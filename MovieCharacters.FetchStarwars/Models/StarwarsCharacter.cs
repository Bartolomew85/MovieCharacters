using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieCharacters.FetchStarwars.Models
{
    /// <summary>
    /// Response model for the https://www.swapi.tech/api/people/ api
    /// </summary>
    public class StarwarsCharacter
    {
        /// <summary>
        /// The movie the character appeared in
        /// </summary>
        public string Movie { get { return "Starwars"; } }

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
        /// The colour of the eyes of the character
        /// </summary>
        [JsonProperty(PropertyName = "eyeColour")]
        public string EyeColour { get; set; }

        /// <summary>
        /// The colour of the hair of the character
        /// </summary>
        [JsonProperty(PropertyName = "hairColour")]
        public string HairColour { get; set; }
    }
}
