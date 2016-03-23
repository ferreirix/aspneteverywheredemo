using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.OptionsModel;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using whereyouat.Models;

namespace whereyouat.Controllers
{
    public class LocationController : Controller
    {
        private Settings _settings;

        public LocationController(IOptions<Settings> options)
        {
            _settings = options.Value;
        }

        [HttpGet("/test")]
        public string GetTestResponse()
        {
            return "We're fine. We're all fine here now, thank you... How are you?";
        }
        
        [HttpPut("/locations")]
        public async Task AddLocation([FromBody]Location location)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(_settings.LocationConnectionString);

            var tableClient = storageAccount.CreateCloudTableClient();

            CloudTable table = tableClient.GetTableReference("locations");

            await table.CreateIfNotExistsAsync();

            var locEntity = new LocationEntity(_settings.Cloud_Name ?? "Unknown");

            locEntity.longitude = location.longitude.ToString();
            locEntity.latitude = location.latitude.ToString();
            locEntity.host = _settings.Docker_Host;
            locEntity.container = _settings.HostName;

            TableOperation insertOperation = TableOperation.Insert(locEntity);

            await table.ExecuteAsync(insertOperation);
        }

        [HttpGet("/locations")]
        public async Task<JsonResult> GetLocations()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(_settings.LocationConnectionString);

            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            CloudTable table = tableClient.GetTableReference("locations");

            TableQuery<LocationEntity> query = new TableQuery<LocationEntity>();

            var data = await table.ExecuteQuerySegmentedAsync(query, new TableContinuationToken());

            return Json(data.Select(x=> new { latitude= x.latitude, longitude = x.longitude, cloud_name = x.cloud_name }));
        }
    }
}