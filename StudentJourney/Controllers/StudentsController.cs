using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ContosoJourney.Data;
using ContosoJourney.Models;
using StudentJourney.Repository;
using StudentJourney.Interfaces;
using StudentJourney.Services;

namespace StudentJourney.Controllers
{
    public class StudentsController : Controller
    {
        private readonly JourneyContext _context;
        private readonly IStudentRepository _studentRepository;
        private readonly IStudentService _studentService;

        public StudentsController(JourneyContext context, IStudentRepository studentRepository, IStudentService studentService)
        {
            _context = context;
            _studentRepository = studentRepository;
            _studentService = studentService;
        }

        // GET: Students
        public async Task<IActionResult> Index()
        {
            var students = await _studentService.GetAllStudents();
            return View(students);
        }


        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var student = await _studentService.GetStudentDetails(id);
            return View(student);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,FirstName,LastName,EnrollmentDate")] Student student)
        {
            if (ModelState.IsValid)
            {
                await _studentService.CreateStudent(student);
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _studentRepository.FindStudentAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }


        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StudentID,FirstName,LastName,EnrollmentDate")] Student student)
        {
            if (id != student.StudentID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _studentService.UpdateStudent(student);
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            await _studentService.DeleteStudent(id);
            return RedirectToAction(nameof(Index));
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _studentRepository.FindStudentAsync(id);
            if (student != null)
            {
                await _studentRepository.DeleteStudent(student);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.StudentID == id);
        }
    }
}
