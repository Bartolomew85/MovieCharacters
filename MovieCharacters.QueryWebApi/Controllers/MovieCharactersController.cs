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
    [ApiController]
    [Route("[controller]")]
    public class MovieCharactersController : ControllerBase
    {
        private readonly ILogger<MovieCharactersController> _logger;
        private readonly TableClient _tableClient;

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

        [Route("GetMovies")]
        [HttpGet]
        public async Task<IEnumerable<string>> GetMovies()
        {
            var entities = _tableClient.Query<MovieCharacter>();

            var result = entities.Select(x => x.Movie).Distinct();

            return result;
        }

        [Route("GetCharactersFromMovie")]
        [HttpGet]
        public async Task<IEnumerable<MovieCharacter>> GetCharactersFromMovie(string movie)
        {
            var result = _tableClient.Query<MovieCharacter>($"Movie eq '{movie}'");

            return result;
        }

        [Route("GetCharactersByHairColour")]
        [HttpGet]
        public async Task<IEnumerable<MovieCharacter>> GetCharactersByHairColour(string hairColour)
        {
            var result = _tableClient.Query<MovieCharacter>($"HairColour eq '{hairColour}'");

            return result;
        }
    }
}
