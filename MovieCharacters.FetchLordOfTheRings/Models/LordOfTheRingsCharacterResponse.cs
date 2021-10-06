using MovieCharacters.FetchHarryPotter.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieCharacters.FetchLordOfTheRings.Models
{
    /// <summary>
    /// Response model for the https://the-one-api.dev/v2/character api
    /// </summary>
    public class LordOfTheRingsCharacterResponse
    {
        /// <summary>
        /// Root property containing the list of characters
        /// </summary>
        [JsonProperty(PropertyName = "docs")]
        public List<LordOfTheRingsCharacter> Characters { get; set; }
    }
}
