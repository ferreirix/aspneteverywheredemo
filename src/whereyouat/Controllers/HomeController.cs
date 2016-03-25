using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNet.Mvc;
using System.Text;
using System.Collections;
using Microsoft.Extensions.OptionsModel;

namespace whereyouat.Controllers
{
    public class HomeController : Controller
    {
        private Settings _settings;

        public HomeController(IOptions<Settings> options)
        {
            _settings = options.Value;
        }

        public IActionResult Index() { return View(_settings); }
    }
}
