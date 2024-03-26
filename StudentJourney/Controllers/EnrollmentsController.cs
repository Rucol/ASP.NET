using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ContosoJourney.Data;
using ContosoJourney.Models;
using StudentJourney.Interfaces;

namespace StudentJourney.Controllers
{
    public class EnrollmentsController : Controller
    {
        private readonly JourneyContext _context;
        private readonly IEnrollmentRepository _enrollmentRepo;

        public EnrollmentsController(JourneyContext context, IEnrollmentRepository enrollmentRepo)
        {
            _enrollmentRepo = enrollmentRepo;
            _context = context;
        }

        // GET: Enrollments
        public async Task<IActionResult> Index()
        {
            var journeyContext = _enrollmentRepo.GetAllAsync();
            return View(await journeyContext);
        }

        // GET: Enrollments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enrollment = await _enrollmentRepo.FirstOrDefault(id);
            if (enrollment == null)
            {
                return NotFound();
            }
            

            return View(enrollment);
        }

        // GET: Enrollments/Create
        // GET: Enrollments/Create
        public async Task<IActionResult> Create()
        {
            // Pobranie listy identyfikatorów studentów
            var studentIds = await _enrollmentRepo.StudentsIds();
            var journeyCost = await _enrollmentRepo.JourneyCost();

            // Pobranie listy wszystkich wycieczek
            var trips = await _enrollmentRepo.Journeys();

            // Przekazanie list do ViewBag, aby można było je użyć w widoku
            ViewBag.StudentID = new SelectList(studentIds);
            ViewBag.TripID = new SelectList(trips, "JourneyID", "TripName");
            ViewBag.Cost = new SelectList(journeyCost, "Cost");
            ViewBag.trips = new SelectList(trips);

            return View();
        }



        // POST: Enrollments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("JourneyID,StudentID,TripID")] Enrollment enrollment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(enrollment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StudentID"] = new SelectList(_context.Students, "ID", "ID", enrollment.StudentID);
            ViewData["TripID"] = new SelectList(_context.Journeys, "JourneyID", "TripName", enrollment.TripID);
            return View(enrollment);
        }

        // GET: Enrollments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enrollment = await _enrollmentRepo.EditJourney(id);
            if (enrollment == null)
            {
                return NotFound();
            }
            ViewData["StudentID"] = new SelectList(await _enrollmentRepo.ViewDataStudentId());
            return View(enrollment);
        }

        // POST: Enrollments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("JourneyID,StudentID,TripID")] Enrollment enrollment)
        {
            if (id != enrollment.JourneyID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _enrollmentRepo.EditEnrollment(enrollment); // Oczekiwanie na zakończenie operacji asynchronicznej
                }
                catch (DbUpdateConcurrencyException)
                {
                    var enrollmentExists = await EnrollmentExists(enrollment.JourneyID); // Oczekiwanie na zakończenie operacji asynchronicznej
                    if (!enrollmentExists)
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
            ViewData["StudentID"] = new SelectList(await _enrollmentRepo.ViewDataStudentId());
            return View(enrollment);
        }


        // GET: Enrollments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enrollment = await _enrollmentRepo.DeleteById(id);
            if (enrollment == null)
            {
                return NotFound();
            }

            return View(enrollment);
        }

        // POST: Enrollments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var enrollment = await _enrollmentRepo.DeleteEnrollement(id);
            if (enrollment != null)
            {
                _enrollmentRepo.RemoveEnrollement(id);
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> EnrollmentExists(int id)
        {
            var enrollment = await _enrollmentRepo.GetEnrollmentIfExists(id);
            return enrollment != null;
        }

    }
}
