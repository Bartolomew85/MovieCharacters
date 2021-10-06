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
using MovieCharacters.FetchLordOfTheRings.Models;
using System.Collections.Generic;
using Azure.Data.Tables;

namespace MovieCharacters.FetchLordOfTheRings
{
    /// <summary>
    /// Azure Function App to fetch Lord of the Rings characters from the https://the-one-api.dev/v2/character api
    /// </summary>
    public static class FetchLordOfTheRingsCharacters
    {
        /// <summary>
        /// Process method for Azure Function App
        /// </summary>
        /// <param name="req">The <see cref="HttpRequest"/> with request data</param>
        /// <param name="log">The <see cref="ILogger>"/> to log with</param>
        /// <returns></returns>
        [FunctionName("FetchLordOfTheRingsCharacters")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            string url = Environment.GetEnvironmentVariable("ApiUrl");
            string apikey = Environment.GetEnvironmentVariable("ApiKey");
            
            string tablestorageUrl = Environment.GetEnvironmentVariable("TablestorageUrl");
            string tablestorageAccountName = Environment.GetEnvironmentVariable("tablestorageAccountName");
            string tablestorageAccountKey = Environment.GetEnvironmentVariable("TablestorageAccountKey");
            string tablestorageTableName = Environment.GetEnvironmentVariable("TablestorageTableName");

            try
            {
                using (var client = new HttpClient())
                {
                    // get characters from API
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", apikey);

                    var charactersResult = await client.GetStringAsync(url);
                    var charactersRersponse = JsonConvert.DeserializeObject<LordOfTheRingsCharacterResponse>(charactersResult);

                    var characters = charactersRersponse.Characters;

                    var tableClient = new TableClient(
                        new Uri(tablestorageUrl),
                        tablestorageTableName,
                        new TableSharedKeyCredential(tablestorageAccountName, tablestorageAccountKey)
                    );

                    // upsert characters to Azure Table Storage
                    foreach (var character in characters)
                    {
                        var entity = new TableEntity(character.Movie, character.Name)
                        {
                            { "Movie", character.Movie },
                            { "Name", character.Name },
                            { "Gender",  character.Gender },
                            { "HairColour",  character.HairColour }
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
    }
}
