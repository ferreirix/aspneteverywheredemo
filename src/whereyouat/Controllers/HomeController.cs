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
    public class HomeController : Controller
    {
        public HomeController(){}
        public IActionResult Index() { return View(); }
    }
}
