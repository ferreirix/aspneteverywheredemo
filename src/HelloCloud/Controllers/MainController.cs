using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;

namespace HelloCloud.Controllers
{
    [Route("/")]
    public class ValuesController : Controller
    {
        // GET: api/values
        [HttpGet]
        public string Get()
        {
            var cloudName = Environment.GetEnvironmentVariable("CLOUD_NAME");
            if(string.IsNullOrEmpty(cloudName))
            {
                cloudName = "an unknown cloud";
            }
            return $"Hello from {cloudName}";
        }
    }
}
