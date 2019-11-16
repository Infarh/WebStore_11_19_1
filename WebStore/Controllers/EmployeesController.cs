using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using WebStore.Infrastructure.Interfaces;
using WebStore.ViewModels;

namespace WebStore.Controllers
{
    //[Route("Users")]
    public class EmployeesController : Controller
    {
        private readonly IEmployeesData _EmployeesData;

        public EmployeesController(IEmployeesData EmployeesData) => _EmployeesData = EmployeesData;

        public IActionResult Index()
        {
            ViewBag.SomeData = "Hello World!";
            ViewData["Test"] = "TestData";

            return View(_EmployeesData.GetAll());
        }

        //[Route("{id}")]
        public IActionResult Details(int? Id)
        {
            if (Id is null)
                return BadRequest();

            var employee = _EmployeesData.GetById((int) Id);
            if (employee is null)
                return NotFound();

            return View(employee);
        }

        public IActionResult DetailsName(string FirstName, string LastName)
        {
            if (FirstName is null && LastName is null)
                return BadRequest();

            var employees = _EmployeesData.GetAll();
            if (!string.IsNullOrWhiteSpace(FirstName))
                employees = employees.Where(e => e.FirstName == FirstName);
            if (!string.IsNullOrWhiteSpace(LastName))
                employees = employees.Where(e => e.SecondName == LastName);

            var employee = employees.FirstOrDefault();

            if (employee is null)
                return NotFound();

            return View(nameof(Details), employee);
        }

        public IActionResult Edit(int Id)
        {
            if (Id < 0)
                return BadRequest();

            var employee = _EmployeesData.GetById((int)Id);
            if (employee is null)
                return NotFound();

            return View(employee);
        }

        [HttpPost]
        public IActionResult Edit(EmployeeView Employee)
        {
            if(Employee is null)
                throw new ArgumentOutOfRangeException(nameof(Employee));

            if (!ModelState.IsValid)
                View(Employee);

            var id = Employee.Id;
            _EmployeesData.Edit(id, Employee);
            _EmployeesData.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}