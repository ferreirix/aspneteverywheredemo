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
            StringBuilder envVars = new StringBuilder();
            foreach (DictionaryEntry de in Environment.GetEnvironmentVariables())
                envVars.Append(string.Format("<strong>{0}</strong>:{1}<br \\>", de.Key, de.Value));

            ViewData["ENV_VARS"] = envVars.ToString();

            ViewData["HostName"] = (Environment.GetEnvironmentVariables()["HOSTNAME"] != null) ?
                Environment.GetEnvironmentVariables()["HOSTNAME"] :
                Environment.GetEnvironmentVariables()["COMPUTERNAME"];

            ViewData["OS"] = (Environment.GetEnvironmentVariables()["OS"] != null) ?
                Environment.GetEnvironmentVariables()["OS"] :
                Environment.GetEnvironmentVariables()["DNX_RUNTIME_ID"];

            ViewData["PROCESSOR_ARCHITEW6432"] = Environment.GetEnvironmentVariable("PROCESSOR_ARCHITEW6432");
            ViewData["HOSTING_ENVIRONMENT"] = Environment.GetEnvironmentVariable("Hosting:Environment");

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
