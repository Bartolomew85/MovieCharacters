using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieCharacters.FetchStarwars.Models
{
    public class StarwarsCharacter
    {
        public string Movie { get { return "Starwars"; } }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "gender")]
        public string Gender { get; set; }

        [JsonProperty(PropertyName = "eyeColour")]
        public string EyeColour { get; set; }

        [JsonProperty(PropertyName = "hairColour")]
        public string HairColour { get; set; }
    }
}
