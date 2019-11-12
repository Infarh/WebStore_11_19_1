using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.ViewModels;

namespace WebStore.Controllers
{
    public class EmployeesController : Controller
    {
        private static readonly List<EmployeeView> __Employees = new List<EmployeeView>
        {
            new EmployeeView { Id = 1, SecondName = "Иванов", FirstName = "Иван", Patronymic = "Иванович", Age = 35 },
            new EmployeeView { Id = 2, SecondName = "Петров", FirstName = "Пётр", Patronymic = "Петрович", Age = 25 },
            new EmployeeView { Id = 3, SecondName = "Сидоров", FirstName = "Сидор", Patronymic = "Сидорович", Age = 18 },
        };

        public IActionResult Index()
        {
            ViewBag.SomeData = "Hello World!";
            ViewData["Test"] = "TestData";

            return View(__Employees);
        }
    }
}