using System;
using Microsoft.AspNetCore.Mvc;

namespace WebStore.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => View();

        public IActionResult Blog() => View();

        public IActionResult BlogSingle() => View();

        public IActionResult ContactUs() => View();

        public IActionResult Error404() => View();

        public IActionResult ThrowException() => throw new ApplicationException("Тестовая ошибка в программе");

        public IActionResult ErrorStatus(string Id)
        {
            switch (Id)
            {
                default: return Content($"Статусный код {Id}");
                case "404":
                    return RedirectToAction(nameof(Error404));
            }
        }
    }
}