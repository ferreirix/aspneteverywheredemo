using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;

namespace whereyouat.Controllers
{
    public class LocationController
    {
        [HttpGet("/test")]
        public string GetTestResponse()
        {
            return "We're fine. We're all fine here now, thank you... How are you?";
        }
    }
}
