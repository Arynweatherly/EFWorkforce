using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EFWorkforce.Data;
using EFWorkforce.Models;

namespace EFWorkforce.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly ApplicationDBContext _context;

        public EmployeesController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: Employees
        public async Task<IActionResult> Index()
        {
            //always change applicationDbContext to something else min 3:15
            var employees = _context.Employee.Include(e => e.Department);
            return View(await employees.ToListAsync());
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //min 12:15 this is instead of SQL reader and instead of joins 
            var employee = await _context.Employee //referring to the employee table in the dB
                .Include(e => e.Computer)
                .Include(e => e.Department)
                .FirstOrDefaultAsync(m => m.Id == id);
            //first or default turns back the first thing that matches. It's the equivalent to where
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }



        // GET: Employees/Create
        public IActionResult Create()
        {
            //what's viewData? it's a variable where you store the data 
            //how do we inject data into our view? in the view model .... return view (employee) we passed in the model employee
            //change name to ComputerList and department List

            var computers = _context.Computer.Where(c => c.DecomissionDate == null && c.Employee == null); 
            ViewData["ComputerList"] = new SelectList(_context.Computer, "Id", "Model");
            ViewData["DepartmentList"] = new SelectList(_context.Department, "Id", "Name");
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Email,DepartmentId,IsSupervisor,ComputerId")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ComputerId"] = new SelectList(_context.Computer, "Id", "Id", employee.ComputerId);
            ViewData["DepartmentId"] = new SelectList(_context.Department, "Id", "Id", employee.DepartmentId);
            return View(employee);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            //view data is just a dictionary 
            ViewData["ComputerList"] = new SelectList(_context.Computer, "Id", "Model", employee.ComputerId);
            ViewData["DepartmentList"] = new SelectList(_context.Department, "Id", "Name", employee.DepartmentId);
            
            return View(employee);
            //in edit we need the context of an employee form 
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,Email,DepartmentId,IsSupervisor,ComputerId")] Employee employee)
        {
            if (id != employee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ComputerId"] = new SelectList(_context.Computer, "Id", "Id", employee.ComputerId);
            ViewData["DepartmentId"] = new SelectList(_context.Department, "Id", "Id", employee.DepartmentId);
            return View(employee);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee
                .Include(e => e.Computer)
                .Include(e => e.Department)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _context.Employee.FindAsync(id);
            _context.Employee.Remove(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employee.Any(e => e.Id == id);
        }
    }
}
