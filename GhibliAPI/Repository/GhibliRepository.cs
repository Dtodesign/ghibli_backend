using GhibliWebAPI.Context;
using GhibliWebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace GhibliWebAPI.Repository.IRepository
{
    public class GhibliRepository : IGhibliRepository
    {
        private readonly ghDbContext _db;


        public GhibliRepository(ghDbContext db)
        {
            _db = db;
        }


        async Task<Film> IGhibliRepository.AddFilmAsync(Film film)
        {
            film.ID = Guid.NewGuid().ToString();
            await _db.AddAsync(film);

            await _db.SaveChangesAsync();
            return film;

        }

        async Task<Film> IGhibliRepository.DeleteFilm(Film film)
        {

            _db.Films.Remove(film);
            await _db.SaveChangesAsync();

            return film;


        }

        async Task<Film> IGhibliRepository.GetFilmById(string id)
        {
            return await _db.Films.FirstOrDefaultAsync(f => f.ID == id);
        }

        async Task<ICollection<Film>> IGhibliRepository.GetFilms()
        {
            return await _db.Films.ToListAsync();

        }

        async Task<Film> IGhibliRepository.UpdateFilm(Film film)
        {
            _db.Films.Update(film);
            await _db.SaveChangesAsync();

            return film;


        }
    }
}
