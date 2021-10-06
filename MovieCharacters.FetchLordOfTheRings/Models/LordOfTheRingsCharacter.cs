using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieCharacters.FetchHarryPotter.Models
{
    /// <summary>
    /// Response model for the https://the-one-api.dev/v2/character api
    /// </summary>
    public class LordOfTheRingsCharacter
    {
        /// <summary>
        /// The movie the character appeared in
        /// </summary>
        public string Movie { get { return "Lord of the Rings"; } }

        /// <summary>
        /// The fullname of the character
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// The gender of the character
        /// </summary>
        [JsonProperty(PropertyName = "gender")]
        public string Gender { get; set; }

        /// <summary>
        /// The colour of the hair of the character
        /// </summary>
        [JsonProperty(PropertyName = "hair")]
        public string HairColour { get; set; }
    }
}
