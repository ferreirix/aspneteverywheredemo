using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace whereyouat.Models
{
    public class Location
    {
        public int loc_Id { get; set; }
        public float longitude { get; set; }
        public float latitude { get; set; }

        public string city_name { get; set; }
    }
}
