using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieCharacters.FetchStarwars.Models
{
    /// <summary>
    /// Response model for the https://www.swapi.tech/api/people api
    /// </summary>
    public class StarwarsPeopleResponse
    {
        /// <summary>
        /// The root property containing the result
        /// </summary>
        [JsonProperty(PropertyName = "results")]
        public List<StarwarsPeopleResponsePerson> People { get; set; }

        /// <summary>
        /// The url of the next page to fetch
        /// </summary>
        [JsonProperty(PropertyName = "next")]
        public string Next { get; set; }
    }

    /// <summary>
    /// Response model for the https://www.swapi.tech/api/people api
    /// </summary>
    public class StarwarsPeopleResponsePerson
    {
        /// <summary>
        /// The unique identifier of the character in the API
        /// </summary>
        [JsonProperty(PropertyName = "uid")]
        public string Uid { get; set; }
        
        /// <summary>
        /// The fullname of the character
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        
        /// <summary>
        /// The url of the full details of the character
        /// </summary>
        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }
    }
}
