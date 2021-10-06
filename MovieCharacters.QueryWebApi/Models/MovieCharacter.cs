using Azure;
using Azure.Data.Tables;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieCharacters.QueryWebApi.Models
{
    /// <summary>
    /// The <see cref="ITableEntity"/> with common data for the movie character
    /// </summary>
    public class MovieCharacter: ITableEntity
    {
        /// <summary>
        /// The movie the character appeared in
        /// </summary>
        public string Movie { get; set; }

        /// <summary>
        /// The fullname of the character
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The gender of the character
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        /// The colour of the eyes of the character
        /// </summary>
        public string EyeColour { get; set; }

        /// <summary>
        /// The colour of the hair of the character
        /// </summary>
        public string HairColour { get; set; }
        
        /// <summary>
        /// The PartitionKey for this entity
        /// </summary>
        public string PartitionKey { get; set; }

        /// <summary>
        /// The RowKey for this entity
        /// </summary>
        public string RowKey { get; set; }

        /// <summary>
        /// The Timestamp for this entity
        /// </summary>
        public DateTimeOffset? Timestamp { get; set; }

        /// <summary>
        /// The <see cref="ETag"/> for this entity
        /// </summary>
        public ETag ETag { get; set; }
    }
}
