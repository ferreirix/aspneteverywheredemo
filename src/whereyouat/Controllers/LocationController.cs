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

        [HttpPut("/locations")]
        public async Task AddLocation([FromForm]Location location)
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

            return Json(data.Select(x => new { latitude = x.latitude, longitude = x.longitude, cloud_name = x.cloud_name }));
        }

        [HttpGet("/cloudcounts")]
        public async Task<JsonResult> GetCloudCounts()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(_settings.LocationConnectionString);

            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            CloudTable table = tableClient.GetTableReference("locations");

            TableQuery<LocationEntity> query = new TableQuery<LocationEntity>();

            var data = await table.ExecuteQuerySegmentedAsync(query, new TableContinuationToken());

            var result =
                data.GroupBy(x => new { x.cloud_name, x.container })
                    .Select(g => new { g.Key.cloud_name, g.Key.container, Count = g.Count() })
                    .OrderBy(o => new { o.cloud_name, o.container, o.Count });
            //var result = data.Select(x => new { could = x.cloud_name, hostname = x.host, timestampe = x.Timestamp, container = x.container, pkey = x.PartitionKey, rkey = x.RowKey });
            return Json(result);
        }


        #region Testing APIs
        [HttpGet("/test")]
        public string GetTestResponse()
        {
            return "We're fine. We're all fine here now, thank you... How are you?";
        }
        #endregion
        
        #region LoadTesting APIs
        [HttpGet("/addrandomlocation")]
        public async Task AddRandomLocation()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(_settings.LocationConnectionString);

            var tableClient = storageAccount.CreateCloudTableClient();

            CloudTable table = tableClient.GetTableReference("locations");

            await table.CreateIfNotExistsAsync();

            var locEntity = new LocationEntity(_settings.Cloud_Name ?? "LoadTest");
            Location location = new Location();
            Random randomLat = new Random();
            Random randomLong = new Random();


            float rndLat = randomLat.Next(100) - 70;
            float rndLong = randomLong.Next(0, 300) - 150;

            location.latitude = rndLat;
            location.longitude = rndLong;
            locEntity.longitude = location.longitude.ToString();
            locEntity.latitude = location.latitude.ToString();
            locEntity.host = _settings.Docker_Host;
            locEntity.container = _settings.HostName;

            TableOperation insertOperation = TableOperation.Insert(locEntity);

            await table.ExecuteAsync(insertOperation);
        }

        [HttpGet("/locationsbycloud")]
        public async Task<JsonResult> GetLocationsByCloud(string cloudname)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(_settings.LocationConnectionString);

            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            CloudTable table = tableClient.GetTableReference("locations");

            TableQuery<LocationEntity> query = new TableQuery<LocationEntity>();

            var data = await table.ExecuteQuerySegmentedAsync(query, new TableContinuationToken());

            return Json(data.Where(w => w.cloud_name == cloudname)
                .Select(x => new { latitude = x.latitude, longitude = x.longitude, cloud_name = x.cloud_name }));
        }

        [HttpGet("/alldata")]
        public async Task<JsonResult> GetAllData()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(_settings.LocationConnectionString);

            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            CloudTable table = tableClient.GetTableReference("locations");

            TableQuery<LocationEntity> query = new TableQuery<LocationEntity>();

            var data = await table.ExecuteQuerySegmentedAsync(query, new TableContinuationToken());

            var result = data;
            //var result = data.Select(x => new { could = x.cloud_name, hostname = x.host, timestampe = x.Timestamp, container = x.container, pkey = x.PartitionKey, rkey = x.RowKey });
            return Json(result);
        }

        #endregion

    }
}