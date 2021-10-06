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
using MovieCharacters.FetchHarryPotter.Models;
using Azure.Data.Tables;

namespace MovieCharacters
{
    /// <summary>
    /// Azure Function App to fetch Harry Potter characters from the http://hp-api.herokuapp.com/api/characters api
    /// </summary>
    public static class FetchHarryPotterCharacters
    {
        /// <summary>
        /// Process method for Azure Function App
        /// </summary>
        /// <param name="req">The <see cref="HttpRequest"/> with request data</param>
        /// <param name="log">The <see cref="ILogger>"/> to log with</param>
        /// <returns></returns>
        [FunctionName("FetchHarryPotterCharacters")]
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
                    var charactersResult = await client.GetStringAsync(url);
                    var characters = JsonConvert.DeserializeObject<List<HarryPotterCharacter>>(charactersResult);

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
    }
}
