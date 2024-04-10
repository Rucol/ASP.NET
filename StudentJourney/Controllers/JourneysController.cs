using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ContosoJourney.Data;
using ContosoJourney.Models;
using StudentJourney.Interfaces;
using StudentJourney.Services;
using Microsoft.EntityFrameworkCore;
using StudentJourney.ViewModels;

namespace StudentJourney.Controllers
{
    public class JourneysController : Controller
    {
        private readonly JourneyContext _context;
        private readonly IJourneysRepository _journeysRepository;
        private readonly IJourneyService _journeyService;

        public JourneysController(JourneyContext context, IJourneysRepository journeysRepository, IJourneyService journeyService)
        {
            _context = context;
            _journeysRepository = journeysRepository;
            _journeyService = journeyService;
        }

        // GET: Journeys
        public async Task<IActionResult> Index()
        {
            var journeys = await _journeyService.GetAllJourneys();
            if (journeys.Count == 0)
            {
                var sampleJourneys = new Journey[]
                {
                    new Journey{JourneyID=1050,TripName="Paris",Cost=1050},
                    new Journey{JourneyID=4022,TripName="Tokyo",Cost=4022},
                    new Journey{JourneyID=4041,TripName="Rome",Cost=4041},
                    new Journey{JourneyID=1045,TripName="Sydney",Cost=1045},
                    new Journey{JourneyID=3141,TripName="Rio de Janeiro",Cost=2050},
                    new Journey{JourneyID=2021,TripName="Istanbul",Cost=2000},
                    new Journey{JourneyID=2042,TripName="New York City",Cost=2060}
                };

                foreach (var journey in sampleJourneys)
                {
                    await _journeysRepository.AddExamples(journey);
                }

                journeys = await _journeysRepository.GetAllJourneys();
            }

            // Przekształć listę Journey na listę JourneyViewModel
            var viewModelList = journeys.Select(journey => new JourneyViewModel
            {
                JourneyID = journey.JourneyID,
                TripName = journey.TripName,
                Cost = journey.Cost,
                JourneyDate = journey.JourneyDate, // Dodaj datę podróży
                JourneyDuration = journey.JourneyDuration // Dodaj czas trwania podróży
            });

            return View(viewModelList);
        }

        // GET: Journeys/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var journey = await _journeyService.GetJourneyDetails(id);
            if (journey == null)
            {
                return NotFound();
            }

            var viewModel = new JourneyViewModel
            {
                JourneyID = journey.JourneyID,
                TripName = journey.TripName,
                Cost = journey.Cost,
                JourneyDate = journey.JourneyDate,
                JourneyDuration = journey.JourneyDuration
            };

            return View(viewModel);
        }

        // GET: Journeys/Create
        public IActionResult Create()
        {
            var viewModel = new JourneyViewModel(); // Utwórz instancję ViewModelu
            return View(viewModel);
        }

        // POST: Journeys/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("JourneyID,TripName,Cost,JourneyDate,JourneyDuration")] JourneyViewModel journeyViewModel)
        {
            if (ModelState.IsValid)
            {
                var journey = new Journey
                {
                    JourneyID = journeyViewModel.JourneyID,
                    TripName = journeyViewModel.TripName,
                    Cost = journeyViewModel.Cost,
                    JourneyDate = journeyViewModel.JourneyDate,
                    JourneyDuration = journeyViewModel.JourneyDuration
                };

                await _journeyService.CreateJourney(journey);
                return RedirectToAction(nameof(Index));
            }
            return View(journeyViewModel);
        }
        // GET: Journeys/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var journey = await _journeysRepository.EditJourney(id);
            if (journey == null)
            {
                return NotFound();
            }

            var viewModel = new JourneyViewModel
            {
                JourneyID = journey.JourneyID,
                TripName = journey.TripName,
                Cost = journey.Cost,
                JourneyDate = journey.JourneyDate,
                JourneyDuration = journey.JourneyDuration
            };

            return View(viewModel);
        }

        // POST: Journeys/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("JourneyID,TripName,Cost,JourneyDate,JourneyDuration")] JourneyViewModel journeyViewModel)
        {
            if (id != journeyViewModel.JourneyID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var journey = new Journey
                {
                    JourneyID = journeyViewModel.JourneyID,
                    TripName = journeyViewModel.TripName,
                    Cost = journeyViewModel.Cost,
                    JourneyDate = journeyViewModel.JourneyDate,
                    JourneyDuration = journeyViewModel.JourneyDuration
                };

                try
                {
                    await _journeyService.UpdateJourney(journey);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JourneyExists(journey.JourneyID))
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
            return View(journeyViewModel);
        }


        // GET: Journeys/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var journey = await _journeysRepository.GetDelete(id);
            if (journey == null)
            {
                return NotFound();
            }

            var viewModel = new JourneyViewModel
            {
                JourneyID = journey.JourneyID,
                TripName = journey.TripName,
                Cost = journey.Cost,
                JourneyDate = journey.JourneyDate,
                JourneyDuration = journey.JourneyDuration
            };

            return View(viewModel);
        }

        // POST: Journeys/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _journeyService.DeleteJourney(id);
            return RedirectToAction(nameof(Index));
        }

        private bool JourneyExists(int id)
        {
            return _context.Journeys.Any(e => e.JourneyID == id);
        }
    }
}
