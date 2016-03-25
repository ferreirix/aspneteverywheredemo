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
    public class SuperSecretController : Controller
    {
        public SuperSecretController(){}
        public IActionResult Index() { return View(); }
    }
}
