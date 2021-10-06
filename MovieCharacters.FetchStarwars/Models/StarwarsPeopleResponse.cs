using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieCharacters.FetchStarwars.Models
{
    public class StarwarsPeopleResponse
    {
        [JsonProperty(PropertyName = "results")]
        public List<StarwarsPeopleResponsePerson> People { get; set; }
        [JsonProperty(PropertyName = "next")]
        public string Next { get; set; }
    }

    public class StarwarsPeopleResponsePerson
    {
        [JsonProperty(PropertyName = "uid")]
        public string Uid { get; set; }
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }
    }
}
