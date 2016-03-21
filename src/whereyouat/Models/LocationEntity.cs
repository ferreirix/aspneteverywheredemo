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
        public LocationEntity(string city)
        {
            loc_id = Guid.NewGuid().ToString();
            city_name = city;

            this.RowKey = this.loc_id;
            this.PartitionKey = this.city_name;
        }
        public string loc_id {get;set;}
        public string longitude { get; set; }
        public string latitude { get; set; }
        public string city_name { get; set; }
    }
}
