using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNet.Mvc;
using System.Text;
using System.Collections;

namespace whereyouat.Controllers
{
    public class AboutController : Controller
    {
        public AboutController()
        {
        }
        public IActionResult Index()
        {
            ViewData["HostName"] = Environment.GetEnvironmentVariable("HOSTNAME") ?? 
                Environment.GetEnvironmentVariable("COMPUTERNAME");

            ViewData["OS"] = Environment.GetEnvironmentVariable("OS") ??
                Environment.GetEnvironmentVariable("DNX_RUNTIME_ID");

            ViewData["CLOUD_NAME"] = Environment.GetEnvironmentVariable("CLOUD_NAME");

            ViewData["PROCESSORARCH"] = Environment.GetEnvironmentVariable("PROCESSOR_ARCHITEW6432");
            ViewData["HOSTING_ENVIRONMENT"] = Environment.GetEnvironmentVariable("Hosting:Environment");

            StringBuilder envVars = new StringBuilder();
            foreach (DictionaryEntry de in Environment.GetEnvironmentVariables())
                envVars.Append(string.Format("<strong>{0}</strong>:{1}<br \\>", de.Key, de.Value));

            ViewData["ENV_VARS"] = envVars.ToString();

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
