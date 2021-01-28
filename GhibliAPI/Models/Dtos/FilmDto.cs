using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GhibliWebAPI.Models.Dtos
{

    public class FilmDto
    {

        public string ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public string Director { get; set; }

        //public string producer { get; set; }
        public string ReleaseDate { get; set; }

        public string Rate { get; set; }

        //public string rt_score { get; set; }
        //public ICollection<Character> people { get; set; }


        //public string url { get; set; }
    }

}
