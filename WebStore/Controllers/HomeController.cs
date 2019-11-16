using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Net.Http.Headers;
using WebStore.ViewModels;

namespace WebStore.Controllers
{
    public class HomeController : Controller
    {
        public HomeController() { }

        public IActionResult Index()
        {
            //throw new InvalidOperationException("Отладочная ошибка в приложении");
            return View();
        }

        public IActionResult Blog() => View();
        public IActionResult BlogSingle() => View();
        public IActionResult Cart() => View();
        public IActionResult CheckOut() => View();
        public IActionResult ContactUs() => View();
        public IActionResult Login() => View();
        public IActionResult ProductDetails() => View();
        public IActionResult Shop() => View();

        public IActionResult Error404() => View();

        public IActionResult TestAction()
        {
            //return new ViewResult(); // Ручное создание "представления" - альтернатива вызову View
            //return View();

            //return new JsonResult(new { Customer = "Иванов", Id = 15, Date = DateTime.Now });
            //return Json(new { Customer = "Иванов", Id = 15, Date = DateTime.Now });

            //return Redirect("http://www.yandex.ru");
            //return new RedirectResult("http://www.yandex.ru");

            //return new RedirectToActionResult("Index", "Employees", null);
            return RedirectToAction("Index", "Employees");

            //return Content("Hello World");
            //return new ContentResult { Content = "Hello World", ContentType = "application/text" };

            //return File(Encoding.UTF8.GetBytes("Hello World!"), "application/text", "HelloWorld.txt");
            //return new FileContentResult(Encoding.UTF8.GetBytes("Hello World!"), new MediaTypeHeaderValue("application/text"));
            //return new FileStreamResult(new MemoryStream(Encoding.UTF8.GetBytes("Hello World!")), "application/text");

            //return StatusCode(405);
            //return new StatusCodeResult(500);
            //return NoContent();
            //return new NoContentResult();
        }
    }
}