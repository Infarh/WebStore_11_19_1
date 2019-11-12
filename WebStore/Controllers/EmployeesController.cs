using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
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

        public IActionResult Details(int? Id)
        {
            if (Id is null)
                return BadRequest();

            var employee = __Employees.FirstOrDefault(e => e.Id == Id);
            if (employee is null)
                return NotFound();

            return View(employee);
        }

        public IActionResult DetailsName(string FirstName, string LastName)
        {
            if (FirstName is null && LastName is null)
                return BadRequest();

            IEnumerable<EmployeeView> employees = __Employees;
            if (!string.IsNullOrWhiteSpace(FirstName))
                employees = employees.Where(e => e.FirstName == FirstName);
            if (!string.IsNullOrWhiteSpace(LastName))
                employees = employees.Where(e => e.SecondName == LastName);

            var employee = employees.FirstOrDefault();

            if (employee is null)
                return NotFound();

            return View(nameof(Details), employee);
        }

        [HttpPost]
        public IActionResult Edit(int id, [FromBody] EmployeeView Employee)
        {
            return RedirectToAction(nameof(Index));
        }
    }
}