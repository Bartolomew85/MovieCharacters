using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;
using System.Collections.Generic;
using MovieCharacters.FetchStarwars.Models;
using Azure.Data.Tables;

namespace MovieCharacters.FetchStarwars
{
    /// <summary>
    /// Azure Function App to fetch Starwars characters from the https://www.swapi.tech/api/people/ api
    /// </summary>
    public static class FetchStarwarsCharacters
    {
        /// <summary>
        /// Process method for Azure Function App
        /// </summary>
        /// <param name="req">The <see cref="HttpRequest"/> with request data</param>
        /// <param name="log">The <see cref="ILogger>"/> to log with</param>
        /// <returns></returns>
        [FunctionName("FetchStarwarsCharacters")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            string url = Environment.GetEnvironmentVariable("ApiUrl");

            string tablestorageUrl = Environment.GetEnvironmentVariable("TablestorageUrl");
            string tablestorageAccountName = Environment.GetEnvironmentVariable("tablestorageAccountName");
            string tablestorageAccountKey = Environment.GetEnvironmentVariable("TablestorageAccountKey");
            string tablestorageTableName = Environment.GetEnvironmentVariable("TablestorageTableName");

            try
            {
                using (var client = new HttpClient())
                {
                    // get characters from API
                    var peopleResult = await client.GetStringAsync(url);
                    var peopleResponse = JsonConvert.DeserializeObject<StarwarsPeopleResponse>(peopleResult);

                    var characters = new List<StarwarsCharacter>();

                    var detailedCharacters = await GetDetailedCharacters(url, client, peopleResponse.People);
                    characters.AddRange(detailedCharacters);

                    // fetch next page if necessary
                    while (!string.IsNullOrEmpty(peopleResponse.Next))
                    {
                        var peopleNextResult = await client.GetStringAsync(peopleResponse.Next);
                        peopleResponse = JsonConvert.DeserializeObject<StarwarsPeopleResponse>(peopleNextResult);

                        detailedCharacters = await GetDetailedCharacters(url, client, peopleResponse.People);
                        characters.AddRange(detailedCharacters);
                    }

                    // upsert characters to Azure Table Storage
                    var tableClient = new TableClient(
                        new Uri(tablestorageUrl),
                        tablestorageTableName,
                        new TableSharedKeyCredential(tablestorageAccountName, tablestorageAccountKey)
                    );

                    foreach (var character in characters)
                    {
                        var entity = new TableEntity(character.Movie, character.Name)
                        {
                            { "Movie", character.Movie },
                            { "Name", character.Name },
                            { "Gender",  character.Gender },
                            { "HairColour",  character.HairColour },
                            { "EyeColour",  character.EyeColour }
                        };

                        await tableClient.UpsertEntityAsync(entity);
                    }
                }
            }
            catch (Exception ex)
            {
                log.LogError(ex.ToString());
                return new BadRequestResult();
            }

            return new OkResult();
        }

        /// <summary>
        /// Get detailed information for the people in the movie
        /// </summary>
        /// <param name="url">The api url to fetch additional data</param>
        /// <param name="client">The <see cref="HttpClient"/> for querying</param>
        /// <param name="people">The <see cref="List{}"/> of <see cref="StarwarsPeopleResponsePerson"/> to fetch the detailed information for</param>
        /// <returns></returns>
        private static async Task<List<StarwarsCharacter>> GetDetailedCharacters(string url, HttpClient client, List<StarwarsPeopleResponsePerson> people)
        {
            List<StarwarsCharacter> characters = new List<StarwarsCharacter>();

            foreach (var person in people)
            {
                var peopleByIdResult = await client.GetStringAsync(url + person.Uid);
                var peopleByIdResponse = JsonConvert.DeserializeObject<StarwarsPeopleByIdResponse>(peopleByIdResult);
                
                var character = new StarwarsCharacter()
                {
                    Name = peopleByIdResponse.Result.Properties.Name,
                    Gender = peopleByIdResponse.Result.Properties.Gender,
                    EyeColour = peopleByIdResponse.Result.Properties.EyeColour,
                    HairColour = peopleByIdResponse.Result.Properties.HairColour
                };

                characters.Add(character);
            }

            return characters;
        }
    }
}
