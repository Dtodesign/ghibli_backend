using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace GhibliWebAPI.Models
{

    public class Film
    {
        [JsonProperty("id")]
        public string ID { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("director")]
        public string Director { get; set; }

        //public string producer { get; set; }

        [JsonProperty("release_date")]
        public string ReleaseDate { get; set; }

        [JsonProperty("rt_score")]
        public string Rate { get; set; }

        internal T ToObject<T>()
        {
            throw new NotImplementedException();
        }
    }

}
