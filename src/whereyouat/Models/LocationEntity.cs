using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Table;

namespace whereyouat.Models
{
    public class LocationEntity : TableEntity
    {
        public LocationEntity() { }
        public LocationEntity(string cloud_name)
        {
            this.cloud_name = cloud_name;

            this.RowKey = Guid.NewGuid().ToString();
            this.PartitionKey = this.cloud_name;
        }

        public string longitude { get; set; }
        public string latitude { get; set; }
        public string cloud_name { get; set; }
        public string host {get;set;}
        public string container {get;set;}
    }
}
