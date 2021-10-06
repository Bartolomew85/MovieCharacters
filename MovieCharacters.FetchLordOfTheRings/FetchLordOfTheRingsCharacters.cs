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
    public static class FetchLordOfTheRingsCharacters
    {
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
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", apikey);

                    var charactersResult = await client.GetStringAsync(url);
                    var charactersRersponse = JsonConvert.DeserializeObject<LordOfTheRingsCharacterResponse>(charactersResult);

                    var characters = charactersRersponse.Characters;

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
