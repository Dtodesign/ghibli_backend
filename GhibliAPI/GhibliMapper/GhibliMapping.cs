using AutoMapper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GhibliWebAPI.Models.Dtos;
using GhibliWebAPI.Models;

namespace GhibliWebAPI.GhibliMapper
{
    public class GhibliMapping : Profile
    {
        public GhibliMapping()
        {
            CreateMap<Film, FilmDto>().ReverseMap();
            
        }

    }
}
