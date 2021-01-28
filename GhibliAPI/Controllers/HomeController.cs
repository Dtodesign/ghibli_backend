using AutoMapper;
using GhibliWebAPI.Context;
using GhibliWebAPI.Models;
using GhibliWebAPI.Models.Dtos;
using GhibliWebAPI.Repository;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;


namespace GhibliWebAPI.Controllers
{

    [Route("api/[controller]")]
    [EnableCors("CORSPolicy")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        // private readonly ILogger<HomeController> _logger;

        private readonly ghDbContext _context;
        //private const string BaseUrl = "https://ghibliapi.herokuapp.com/films";
        private readonly IGhibliRepository _ghRepo;
        private readonly IMapper _mapper;
    

        // ILogger<HomeController> logger,
        public HomeController(IMapper mapper, ghDbContext context, IGhibliRepository ghibliRepository)
        {
            //_logger = logger;
            _context = context;
            _ghRepo = ghibliRepository;
            _mapper = mapper;

        }

        

        [HttpGet]
        public async Task<ActionResult<ICollection<Film>>> GetFilmsFromDb()
        {
            var fList = await _ghRepo.GetFilms();
            var fDto = new List<FilmDto>();
            foreach (var x in fList)
            {
                fDto.Add(_mapper.Map<FilmDto>(x));
            }

            return Ok(fDto);



        }

        //GET: HomeController/Details/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Film>> GetFilmById(string id)
        {
            var obj = await _ghRepo.GetFilmById(id);
            if (obj == null)
            {
                return NotFound();
            }
            var objDto = _mapper.Map<FilmDto>(obj);

            return Ok(objDto);

            //var f = await _context.Films.FirstOrDefaultAsync(f => f.ID == id);
            //if (f == null)
            //{
            //    return NotFound();
            //}
            //return f;
        }

        [HttpPost]
        public async Task<ActionResult<Film>> AddFilmToDb([FromBody] FilmDto filmDto)
        {
            //    _context.Films.Add(film);
            //    await _context.SaveChangesAsync();

            //    return Ok(_context.Films);
            if (filmDto == null)
            {
                return BadRequest(ModelState);
            }
            var obj = _mapper.Map<Film>(filmDto);
            await _ghRepo.AddFilmAsync(obj);

            return Ok(obj);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFilm(string id, [FromBody] FilmDto filmDto)
        {
            if (filmDto == null || id != filmDto.ID)
            {
                return BadRequest(ModelState);
            }

            var filmObj = _mapper.Map<Film>(filmDto);
            await _ghRepo.UpdateFilm(filmObj);

            return Ok(filmObj);
        }


        // DELETE: api/Seminars/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Film>> DeleteFilm(string id)
        {
            if (!FilmExists(id))
            {
                return NotFound();
            }
            var fObj = await _ghRepo.GetFilmById(id);
            await _ghRepo.DeleteFilm(fObj);

            return Ok("Deleted!");
        }

        private bool FilmExists(string id)
        {
            return _context.Films.Any(e => e.ID == id);
        }



    }
}
