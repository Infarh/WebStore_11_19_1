using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace WebStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _Configuration;

        public HomeController(IConfiguration Configuration)
        {
            _Configuration = Configuration;
        }

        public IActionResult Index()
        {
            return Content(_Configuration["CustomData"]);
        }
    }
}