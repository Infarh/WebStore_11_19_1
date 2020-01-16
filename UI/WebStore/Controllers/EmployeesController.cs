using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.Entities.Identity;
using WebStore.Domain.ViewModels;
using WebStore.Interfaces.Services;

namespace WebStore.Controllers
{
    [Authorize]
    public class EmployeesController : Controller
    {
        private readonly IEmployeesData _EmployeesData;

        public EmployeesController(IEmployeesData EmployeesData) => _EmployeesData = EmployeesData;

        public IActionResult Index() => View(_EmployeesData.GetAll());

        public IActionResult Details(int? Id)
        {
            if (Id is null)
                return BadRequest();

            var employee = _EmployeesData.GetById((int)Id);
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

        public IActionResult Create() => View(new EmployeeView());

        [HttpPost]
        public IActionResult Create(EmployeeView NewEmployee)
        {
            if (!ModelState.IsValid)
                return View(NewEmployee);

            _EmployeesData.Add(NewEmployee);
            _EmployeesData.SaveChanges();

            return RedirectToAction("Details", new { NewEmployee.Id });
        }

        [Authorize(Roles = Role.Administrator)]
        public IActionResult Edit(int? Id)
        {
            if (Id is null) return View(new EmployeeView()); // Для создания нового сотрудника

            if (Id < 0)
                return BadRequest();

            var employee = _EmployeesData.GetById((int)Id);
            if (employee is null)
                return NotFound();

            return View(employee);
        }

        [HttpPost]
        [Authorize(Roles = Role.Administrator)]
        public IActionResult Edit(EmployeeView Employee)
        {
            if (Employee is null)
                throw new ArgumentOutOfRangeException(nameof(Employee));

            if (Employee.Age < 18) 
                ModelState.AddModelError(nameof(EmployeeView.Age), "Возраст не может быть меньше 18 лет");

            if (Employee.FirstName == "123" && Employee.SecondName == "qwe") 
                ModelState.AddModelError("", "Странное сочетание имени и фамилии");

            if (!ModelState.IsValid)
                return View(Employee);

            var id = Employee.Id;
            if (id == 0)
                _EmployeesData.Add(Employee);
            else
                _EmployeesData.Edit(id, Employee);

            _EmployeesData.SaveChanges();

            return RedirectToAction("Index");
        }

        [Authorize(Roles = Role.Administrator)]
        public IActionResult Delete(int Id)
        {
            var employee = _EmployeesData.GetById(Id);
            if (employee is null)
                return NotFound();
            return View(employee);
        }

        [HttpPost]
        [Authorize(Roles = Role.Administrator)]
        public IActionResult DeleteConfirmed(int Id)
        {
            _EmployeesData.Delete(Id);
            return RedirectToAction("Index");
        }
    }
}