using MovieCharacters.FetchHarryPotter.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieCharacters.FetchLordOfTheRings.Models
{
    public class LordOfTheRingsCharacterResponse
    {
        [JsonProperty(PropertyName = "docs")]
        public List<LordOfTheRingsCharacter> Characters { get; set; }
    }
}
