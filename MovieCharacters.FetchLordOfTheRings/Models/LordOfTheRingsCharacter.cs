using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieCharacters.FetchHarryPotter.Models
{
    public class LordOfTheRingsCharacter
    {
        public string Movie { get { return "Lord of the Rings"; } }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "gender")]
        public string Gender { get; set; }

        [JsonProperty(PropertyName = "hair")]
        public string HairColour { get; set; }
    }
}
