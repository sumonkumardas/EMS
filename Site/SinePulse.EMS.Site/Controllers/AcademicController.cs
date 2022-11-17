using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SinePulse.EMS.Site.Controllers
{
    public class AcademicController : Controller
    {
        public IActionResult AddCalanderDescription()
        {
            return View();
        }
    }
}