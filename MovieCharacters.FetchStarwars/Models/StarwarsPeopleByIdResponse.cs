using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieCharacters.FetchStarwars.Models
{
    public class StarwarsPeopleByIdResponse
    {
        [JsonProperty(PropertyName = "result")]
        public StarwarsPeopleByIdResponseResult Result { get; set; }
    }

    public class StarwarsPeopleByIdResponseResult
    {
        [JsonProperty(PropertyName = "properties")]
        public StarwarsPeopleByIdResponseProperties Properties { get; set; }
    }

    public class StarwarsPeopleByIdResponseProperties
    {
        public string Movie { get { return "Starwars"; } }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "gender")]
        public string Gender { get; set; }

        [JsonProperty(PropertyName = "eye_color")]
        public string EyeColour { get; set; }

        [JsonProperty(PropertyName = "hair_color")]
        public string HairColour { get; set; }
    }
}
