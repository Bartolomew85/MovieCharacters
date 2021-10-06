using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieCharacters.FetchHarryPotter.Models
{
    public class HarryPotterCharacter
    {
        public string Movie { get { return "Harry Potter"; } }

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
