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
            loc_id = Guid.NewGuid().ToString();
            this.cloud_name = cloud_name;

            this.RowKey = this.loc_id;
            this.PartitionKey = this.cloud_name;
        }

        public string loc_id {get;set;}
        public string longitude { get; set; }
        public string latitude { get; set; }
        public string cloud_name { get; set; }
        public string host {get;set;}
        public string container {get;set;}
    }
}
