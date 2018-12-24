using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Assignment1.Models;

namespace Assignment1.Controllers
{
    // [Route("api/[controller]")]
    public class StudentsController : Controller
    {
        
        private readonly Assignment1Context _context;
        
        

        public StudentsController(Assignment1Context context)
        {
            _context = context;
        }

        
        // GET: Students
        [Route("api/students", Name="index")]
        public async Task<IActionResult> Index()
        {
            double sum = 0;
            int count = 0;
            var something = _context.Student.ToList();
            foreach (var item in something)
            {
                sum = sum + item.GPA;
                count = count + 1;
            }
            double average = sum / count;
            ViewBag.AverageGPA = average.ToString();

            return View(await _context.Student.ToListAsync());

            
        }

        // GET: Students/Details/5
        [Route("api/students/details/{id?}", Name = "Details")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Student
                .FirstOrDefaultAsync(m => m.StudentID == id);
            if (student == null)
            {
                return NotFound();
            }
            if (student.GPA < 2)
            {
                ViewBag.getGPAFlag = "(Academic Probation)";
            }
            if (student.GPA >= 3.4)
            {
                ViewBag.getGPAFlag = "(Dean's List)";
            }
            return View(student);
            
            
        }

        // GET: Students/Create
        [Route("api/students/create", Name="create")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        [Route("api/students/create", Name = "create")]
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StudentID,LastName,FirstName,GPA,GraduationDate,Active")] Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(student);
            
        }

        // GET: Students/Edit/5
        [Route("api/students/edit/{id?}", Name ="Edit")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Student.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        // POST: Students/Edit/5
        [Route("api/students/edit/{id?}", Name = "Edit")]
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StudentID,LastName,FirstName,GPA,GraduationDate,Active")] Student student)
        {
            if (id != student.StudentID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.StudentID))
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
            return View(student);
        }

        // GET: Students/Delete/5
        [Route("api/students/delete/{id?}", Name = "Delete")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Student
                .FirstOrDefaultAsync(m => m.StudentID == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [Route("api/students/delete/{id?}", Name = "Delete")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _context.Student.FindAsync(id);
            _context.Student.Remove(student);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id)
        {
            return _context.Student.Any(e => e.StudentID == id);
        }
    }
}
