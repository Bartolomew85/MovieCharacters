using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieCharacters.FetchHarryPotter.Models
{
    /// <summary>
    /// Response model for the http://hp-api.herokuapp.com/api/characters api
    /// </summary>
    public class HarryPotterCharacter
    {
        /// <summary>
        /// The movie the character appeared in
        /// </summary>
        public string Movie { get { return "Harry Potter"; } }

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
