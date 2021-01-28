using AutoMapper;
using GhibliWebAPI.Context;
using GhibliWebAPI.Models;
using GhibliWebAPI.Models.Dtos;
using GhibliWebAPI.Repository;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;


namespace GhibliWebAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CORSPolicy")]
    public class FilmsController : ControllerBase
    {


        private readonly HttpClient _client;
        private readonly ghDbContext _context;
        private const string BaseUrl = "https://ghibliapi.herokuapp.com/films";
        private readonly IMemoryCache _memoryCache;

        public FilmsController(HttpClient client, ghDbContext context, IMemoryCache memoryCache)
        {

            _client = client;
            _context = context;
            _memoryCache = memoryCache;
        }

        

        [HttpGet]

        public async Task<IActionResult> GetAll()
        {
            var responseHttp = await _client.GetAsync(BaseUrl);
            var cacheKey = $"Get_On_Film-{BaseUrl}";

            if (_memoryCache.TryGetValue(cacheKey, out string cachedValue))
            {
                return Ok(cachedValue);
            }

            try
            {
                if (!responseHttp.IsSuccessStatusCode)
                {
                    throw new Exception("cannot read data!");
                }

                var content = await responseHttp.Content.ReadAsStringAsync();
                var ff = JsonConvert.DeserializeObject<ICollection<Film>>(content);

                var film = new Film();

                foreach (var x in ff)
                {

                    if (!_context.Films.Any(f => f.ID == x.ID))
                    {
                        await _context.AddAsync(x);
                        await _context.SaveChangesAsync();
                    }

                 
                }
                _memoryCache.Set(cacheKey, ff);
                return Ok(ff);

            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }

        }




        //GET: HomeController/Details/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByID(string id)
        {
            var responseHttp = await _client.GetAsync($"{BaseUrl}/{id}");

            if (!responseHttp.IsSuccessStatusCode)
            {
                throw new Exception("cannot read data!");
            }
            var content = await responseHttp.Content.ReadAsStringAsync();
            var f = JsonConvert.DeserializeObject<Film>(content);

            return Ok(f);
        }


       
    }
}