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
    public static class FetchStarwarsCharacters
    {
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
