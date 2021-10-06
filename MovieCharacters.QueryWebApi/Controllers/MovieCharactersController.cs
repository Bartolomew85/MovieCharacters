using Azure.Data.Tables;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MovieCharacters.QueryWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieCharacters.QueryWebApi.Controllers
{
    /// <summary>
    /// The controller definition for the MovieCharacters endpoint
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class MovieCharactersController : ControllerBase
    {
        /// <summary>
        /// The <see cref="ILogger"/>
        /// </summary>
        private readonly ILogger<MovieCharactersController> _logger;

        /// <summary>
        /// The <see cref="TableClient"/> for operations on Azure Table Storage
        /// </summary>
        private readonly TableClient _tableClient;

        /// <summary>
        /// Constructor, initializes a new instance of <see cref="MovieCharactersController"/>
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> used for logging</param>
        /// <param name="configuration">The <see cref="IConfiguration"/> to read configuration from</param>
        public MovieCharactersController(ILogger<MovieCharactersController> logger, IConfiguration configuration)
        {
            _logger = logger;

            string tablestorageUrl = configuration["TablestorageUrl"];
            string tablestorageAccountName = configuration["TablestorageAccountName"];
            string tablestorageAccountKey = configuration["TablestorageAccountKey"];
            string tablestorageTableName = configuration["TablestorageTableName"];

            _tableClient = new TableClient(
                new Uri(tablestorageUrl),
                tablestorageTableName,
                new TableSharedKeyCredential(tablestorageAccountName, tablestorageAccountKey)
            );
        }

        /// <summary>
        /// Get the movies that are available for querying
        /// </summary>
        /// <returns>An awaitable <see cref="Task"/> with the List of movie names</returns>
        [Route("GetMovies")]
        [HttpGet]
        public async Task<IEnumerable<string>> GetMovies()
        {
            var entities = _tableClient.Query<MovieCharacter>();

            var result = entities.Select(x => x.Movie).Distinct();

            return result;
        }

        /// <summary>
        /// Get characters by movie name
        /// </summary>
        /// <param name="movie">The name of the name, e.g. 'Harry Potter'</param>
        /// <returns>An awaitable <see cref="Task"/> with the List of <see cref="MovieCharacter"/></returns>
        [Route("GetCharactersFromMovie")]
        [HttpGet]
        public async Task<IEnumerable<MovieCharacter>> GetCharactersFromMovie(string movie)
        {
            var result = _tableClient.Query<MovieCharacter>($"Movie eq '{movie}'");

            return result;
        }

        /// <summary>
        /// Get characters by hair colour
        /// </summary>
        /// <param name="hairColour">The colour of the hair, e.g. 'brown'</param>
        /// <returns>An awaitable <see cref="Task"/> with the List of <see cref="MovieCharacter"/></returns>
        [Route("GetCharactersByHairColour")]
        [HttpGet]
        public async Task<IEnumerable<MovieCharacter>> GetCharactersByHairColour(string hairColour)
        {
            var result = _tableClient.Query<MovieCharacter>($"HairColour eq '{hairColour}'");

            return result;
        }
    }
}
