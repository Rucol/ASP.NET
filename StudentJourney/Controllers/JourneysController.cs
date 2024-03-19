using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ContosoJourney.Data;
using ContosoJourney.Models;

namespace StudentJourney.Controllers
{
    public class JourneysController : Controller
    {
        private readonly JourneyContext _context;

        public JourneysController(JourneyContext context)
        {
            _context = context;
        }

        // GET: Journeys
        // GET: Journeys
        public async Task<IActionResult> Index()
        {
            // Pobierz listę wszystkich podróży z bazy danych
            var journeys = await _context.Journeys.ToListAsync();

            // Jeśli nie ma żadnych podróży, załaduj przykładowe dane
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

                // Dodaj przykładowe podróże do bazy danych
                _context.Journeys.AddRange(sampleJourneys);
                await _context.SaveChangesAsync();

                // Ponownie pobierz listę podróży
                journeys = await _context.Journeys.ToListAsync();
            }

            // Przekaż listę podróży do widoku
            return View(journeys);
        }


        // GET: Journeys/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var journey = await _context.Journeys
                .FirstOrDefaultAsync(m => m.JourneyID == id);
            if (journey == null)
            {
                return NotFound();
            }

            return View(journey);
        }

        // GET: Journeys/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Journeys/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("JourneyID,TripName,Cost")] Journey journey)
        {
            if (ModelState.IsValid)
            {
                _context.Add(journey);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(journey);
        }

        // GET: Journeys/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var journey = await _context.Journeys.FindAsync(id);
            if (journey == null)
            {
                return NotFound();
            }
            return View(journey);
        }

        // POST: Journeys/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("JourneyID,TripName,Cost")] Journey journey)
        {
            if (id != journey.JourneyID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(journey);
                    await _context.SaveChangesAsync();
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
            return View(journey);
        }

        // GET: Journeys/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var journey = await _context.Journeys
                .FirstOrDefaultAsync(m => m.JourneyID == id);
            if (journey == null)
            {
                return NotFound();
            }

            return View(journey);
        }

        // POST: Journeys/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var journey = await _context.Journeys.FindAsync(id);
            if (journey != null)
            {
                _context.Journeys.Remove(journey);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JourneyExists(int id)
        {
            return _context.Journeys.Any(e => e.JourneyID == id);
        }
    }
}
