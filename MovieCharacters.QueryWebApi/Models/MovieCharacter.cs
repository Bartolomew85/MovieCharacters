using Azure;
using Azure.Data.Tables;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieCharacters.QueryWebApi.Models
{
    public class MovieCharacter: ITableEntity
    {
        public string Movie { get; set; }

        public string Name { get; set; }

        public string Gender { get; set; }

        public string EyeColour { get; set; }

        public string HairColour { get; set; }
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }
    }
}
