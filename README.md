# MovieCharacters
The Movie Characters application consists of four components:
- FetchHarryPotter: fetch Harry Potter character data and store them in a common format in Azure Table Storage
- FetchLordOfTheRings: fetch Lord of the Rings character data and store them in a common format in Azure Table Storage
- FetchStarwars: fetch Starwars character data and store them in a common format in Azure Table Storage
- QueryWebApi: query the Azure Table Storage for common character data. 

Azure Table Storage is a NoSQL database solution which has low maintenance and cost, but is very powerful in querying and storing data.

The following data is extracted for each character: Name, Gender, Eye Colour and Hair Colour. This data is stored in Azure Table Storage along with the name of the movie it appeared in.

# FetchHarryPotter
Azure Function App that is currently implemented as HTTP trigger for convenience, but should eventually be a timer trigger and run periodically. The Function App queries the http://hp-api.herokuapp.com/api/characters api, converts the data into a common format and inserts the data in Azure Table Storage.

# FetchLordOfTheRings
Azure Function App that is currently implemented as HTTP trigger for convenience, but should eventually be a timer trigger and run periodically. The Function App queries the https://the-one-api.dev/v2/character api, converts the data into a common format and inserts the data in Azure Table Storage.

# FetchStarwars
Azure Function App that is currently implemented as HTTP trigger for convenience, but should eventually be a timer trigger and run periodically. The Function App queries the https://www.swapi.tech/api/people/ api, converts the data into a common format and inserts the data in Azure Table Storage.

# QueryWebApi
.Net Core Web Api that can query the Azure Table Storage. For example, search for characters with brown hair colour over all movies.
Swagger endpoint is available at '/swagger' (https://localhost:44386/swagger/index.html)

# Debugging info
To run the Azure Function Apps you should provide a local.settings.json file with at least the following information:
```
    "ApiUrl": "{url of the movie character data api}",
	"ApiKey": "{apikey for the data api, only applicable for Lord of the Rings api}",
    "TablestorageUrl": "https://{name of the storage account}.table.core.windows.net/{table name}",
    "TablestorageAccountName": "{name of the storage account}",
    "TablestorageAccountKey": "{access key of the storage account}",
    "TablestorageTableName": "{table name}"
```

To run the Web Api project you should provide a appsettings.Development.json file with at least the following information
```
    "TablestorageUrl": "https://{name of the storage account}.table.core.windows.net/{table name}",
    "TablestorageAccountName": "{name of the storage account}",
    "TablestorageAccountKey": "{access key of the storage account}",
    "TablestorageTableName": "{table name}"
```

