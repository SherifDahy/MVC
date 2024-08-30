using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC1.Data;
using Models;
using ViewModels;

namespace MVC1.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmployeesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.Employees.Where(x => x.UserId == Convert.ToInt32(Request.Cookies["Id"])).Include(nameof(Employee.Department)).ToList());
        }

        public IActionResult Details(int id)
        {

            var employee =  _context.Employees
                .FirstOrDefault(m => m.id == id);
            return View(employee);
        }

        public IActionResult Create()
        {
            ViewBag.Departments = _context.Departments.ToList();
            return View();
        }

        
        [HttpPost]
        public IActionResult Create(EmployeeVM employee)
        {
            if (ModelState.IsValid)
            {
                Employee Emp = new Employee()
                {
                    firstName = employee.firstName,
                    lastName = employee.lastName,
                    phoneNumber = employee.phoneNumber,
                    DepartmentId = employee.DepartmentId,
                    salary = employee.salary,
                    title = employee.title,
                    Department = _context.Departments.FirstOrDefault(x=>x.id == employee.DepartmentId),
                    UserId = Convert.ToInt32(Request.Cookies["Id"]),
                };
                _context.Employees.Add(Emp);
                _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Departments = _context.Departments.ToList();
            return View();
        }

        public IActionResult Edit(int id)
        {
            ViewBag.Departments = _context.Departments.ToList();
            var employee =  _context.Employees.Find(id);

            if(employee != null)
            {
                EmployeeVM employeeVM = new EmployeeVM()
                {
                    firstName = employee.firstName,
                    lastName = employee.lastName,
                    phoneNumber = employee.phoneNumber,
                    DepartmentId = employee.DepartmentId,
                    salary = employee.salary,
                    title = employee.title,
                    id = employee.id,
                    
                    
                };
                return View(employeeVM);

            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult Edit(int id,EmployeeVM employee)
        {

            if (ModelState.IsValid)
            {
                Employee emp = new Employee()
                {
                    id = employee.id,
                    firstName = employee.firstName,
                    lastName = employee.lastName,
                    phoneNumber = employee.phoneNumber,
                    DepartmentId = employee.DepartmentId,
                    salary = employee.salary,
                    title = employee.title,
                    Department = _context.Departments.FirstOrDefault(),
                    UserId = Convert.ToInt16(Request.Cookies["Id"]),
                };
               _context.Update(emp);
               _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            var employee = _context.Employees
                .FirstOrDefault(m => m.id == id);
            return View(employee);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            Employee emp =  _context.Employees.Find(id);
            _context.Employees.Remove(emp);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        
    }
}
