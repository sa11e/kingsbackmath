using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace KingsBackMath.Controllers
{
    public class ChallangeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}