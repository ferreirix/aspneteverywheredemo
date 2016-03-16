using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;

namespace HelloCloud.Controllers
{
    [Route("/")]
    public class MainController : Controller
    {
        public MainController()
        {
        }
        // GET: api/values
        [HttpGet]
        public string Get()
        {
            var cloudName = Environment.GetEnvironmentVariable("CLOUD_NAME");

            if(string.IsNullOrEmpty(cloudName))
            {
                cloudName = "an unknown cloud";
            }

            //Environment.MachineName will not work until RC2 of corefx.
            //HOSTNAME is set inside any Docker image.
            var hostName = Environment.GetEnvironmentVariable("HOSTNAME");

            if(string.IsNullOrEmpty(hostName))
            {
                hostName = "an unknown host";
            }

            return $"Hello from {cloudName} on {hostName}";
        }

        private static string GetHostName()
        {
            var hostName = Environment.GetEnvironmentVariable("HOSTNAME");
            if(string.IsNullOrEmpty(hostName))
            {
                hostName = Environment.GetEnvironmentVariable("COMPUTERNAME");
            }

            return hostName;
        }
    }
}
