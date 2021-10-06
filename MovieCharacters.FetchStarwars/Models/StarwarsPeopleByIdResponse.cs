using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieCharacters.FetchStarwars.Models
{
    /// <summary>
    /// Response model for the https://www.swapi.tech/api/people/ api
    /// </summary>
    public class StarwarsPeopleByIdResponse
    {
        /// <summary>
        /// The root property containing the result
        /// </summary>
        [JsonProperty(PropertyName = "result")]
        public StarwarsPeopleByIdResponseResult Result { get; set; }
    }

    /// <summary>
    /// Response model for the https://www.swapi.tech/api/people/ api
    /// </summary>
    public class StarwarsPeopleByIdResponseResult
    {
        /// <summary>
        /// The root property containing the actual data for the character
        /// </summary>
        [JsonProperty(PropertyName = "properties")]
        public StarwarsPeopleByIdResponseProperties Properties { get; set; }
    }

    /// <summary>
    /// Response model for the https://www.swapi.tech/api/people/ api
    /// </summary>
    public class StarwarsPeopleByIdResponseProperties
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
        [JsonProperty(PropertyName = "eye_color")]
        public string EyeColour { get; set; }

        /// <summary>
        /// The colour of the hair of the character
        /// </summary>
        [JsonProperty(PropertyName = "hair_color")]
        public string HairColour { get; set; }
    }
}
