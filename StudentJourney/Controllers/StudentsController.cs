using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ContosoJourney.Data;
using ContosoJourney.Models;
using StudentJourney.Repository;
using StudentJourney.Interfaces;
using StudentJourney.Services;
using StudentJourney.ViewModels;

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

            
            var viewModelList = students.Select(student => new StudentViewModel
            {
                StudentID = student.StudentID,
                FirstName = student.FirstName,
                LastName = student.LastName,
                EnrollmentDate = student.EnrollmentDate,
                Pesel = student.Pesel 
            });

            return View(viewModelList);
        }


        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _studentService.GetStudentDetails(id);
            if (student == null)
            {
                return NotFound();
            }

            // Mapowanie Student na StudentViewModel
            var studentViewModel = new StudentViewModel
            {
                StudentID = student.StudentID,
                FirstName = student.FirstName,
                LastName = student.LastName,
                EnrollmentDate = student.EnrollmentDate,
                Pesel = student.Pesel // Dodaj więcej właściwości, jeśli potrzebujesz
            };

            return View(studentViewModel);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StudentID,FirstName,LastName,EnrollmentDate,Pesel")] StudentViewModel studentViewModel)
        {
            if (ModelState.IsValid)
            {
                // Mapowanie StudentViewModel na Student
                var student = new Student
                {
                    StudentID = studentViewModel.StudentID,
                    FirstName = studentViewModel.FirstName,
                    LastName = studentViewModel.LastName,
                    EnrollmentDate = studentViewModel.EnrollmentDate,
                    Pesel = studentViewModel.Pesel // Dodaj więcej właściwości, jeśli potrzebujesz
                };

                await _studentService.CreateStudent(student);
                return RedirectToAction(nameof(Index));
            }
            return View(studentViewModel);
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

            // Mapowanie Student na StudentViewModel
            var studentViewModel = new StudentViewModel
            {
                StudentID = student.StudentID,
                FirstName = student.FirstName,
                LastName = student.LastName,
                EnrollmentDate = student.EnrollmentDate,
                Pesel = student.Pesel // Dodaj więcej właściwości, jeśli potrzebujesz
            };

            return View(studentViewModel);
        }


        // POST: Students/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StudentID,FirstName,LastName,EnrollmentDate,Pesel")] StudentViewModel studentViewModel)
        {
            if (id != studentViewModel.StudentID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // Mapowanie StudentViewModel na Student
                var student = new Student
                {
                    StudentID = studentViewModel.StudentID,
                    FirstName = studentViewModel.FirstName,
                    LastName = studentViewModel.LastName,
                    EnrollmentDate = studentViewModel.EnrollmentDate,
                    Pesel = studentViewModel.Pesel // Dodaj więcej właściwości, jeśli potrzebujesz
                };

                await _studentService.UpdateStudent(student);
                return RedirectToAction(nameof(Index));
            }
            return View(studentViewModel);
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
