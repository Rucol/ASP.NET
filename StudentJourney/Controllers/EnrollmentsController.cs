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
using StudentJourney.Services;
using StudentJourney.ViewModels;
using FluentValidation.Results;
using AutoMapper;

namespace StudentJourney.Controllers
{
    public class EnrollmentsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly JourneyContext _context;
        private readonly IEnrollmentRepository _enrollmentRepo;
        private readonly IEnrollmentService _enrollmentService;
        private readonly EnrollmentValidator _enrollmentValidator;

        public EnrollmentsController(JourneyContext context, IEnrollmentRepository enrollmentRepo, IEnrollmentService enrollmentService, IMapper mapper)
        {
            _enrollmentRepo = enrollmentRepo;
            _context = context;
            _enrollmentService = enrollmentService;
            _enrollmentValidator = new EnrollmentValidator();
            _mapper = mapper;
        }

        // GET: Enrollments
        public async Task<IActionResult> Index()
        {
            var enrollments = await _enrollmentService.GetAllEnrollments();
            var studentIds = await _enrollmentRepo.StudentsIds();
            var journeys = await _enrollmentRepo.Journeys();

            var enrollmentViewModels = enrollments.Select(e => EnrollmentViewModel.FromEnrollment(e, studentIds, journeys));
            return View(enrollmentViewModels);
        }


        // GET: Enrollments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enrollment = await _enrollmentService.GetEnrollmentDetails(id);
            if (enrollment == null)
            {
                return NotFound();
            }
            

            return View(enrollment);
        }
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("JourneyID,StudentID,TripID")] EnrollmentViewModel enrollmentViewModel)
        {
            var enrollment = _mapper.Map<Enrollment>(enrollmentViewModel);
            ValidationResult validationResult = _enrollmentValidator.Validate(enrollment); // Walidacja obiektu Enrollment

            if (validationResult.IsValid)
            {
                _context.Add(enrollment);
                await _enrollmentService.CreateEnrollment(enrollment);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                // Jeśli walidacja nie powiedzie się, zwróć widok z błędami walidacji
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }

                // Pobierz ponownie listy studentów i wycieczek, aby można było je wyświetlić w widoku
                var studentIds = await _enrollmentRepo.StudentsIds();
                var trips = await _enrollmentRepo.Journeys();

                ViewBag.StudentID = new SelectList(studentIds);
                ViewBag.TripID = new SelectList(trips, "JourneyID", "TripName");
                ViewBag.trips = new SelectList(trips);

                return View(enrollment);
            }
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
                    await _enrollmentService.UpdateEnrollment(enrollment); 
                }
                catch (DbUpdateConcurrencyException)
                {
                    var enrollmentExists = await EnrollmentExists(enrollment.JourneyID);
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
                await _enrollmentService.DeleteEnrollment(id);
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
