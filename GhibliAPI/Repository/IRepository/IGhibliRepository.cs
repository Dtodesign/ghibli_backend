using GhibliWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GhibliWebAPI.Repository
{
    public interface IGhibliRepository
    {
        Task<ICollection<Film>> GetFilms();

        Task<Film> GetFilmById(string id);

        Task<Film> AddFilmAsync(Film film);
        Task<Film> UpdateFilm(Film film);

        Task<Film> DeleteFilm(Film film);
       
    }
}
