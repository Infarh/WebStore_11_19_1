using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebStore.ViewModels;

namespace WebStore.Controllers
{
    public class EmployeesTestController : Controller
    {
        // GET: EmployeesTest
        public ActionResult Index()
        {
            return View(new EmployeeView[10]);
        }

        // GET: EmployeesTest/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: EmployeesTest/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EmployeesTest/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EmployeesTest/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: EmployeesTest/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EmployeesTest/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: EmployeesTest/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}