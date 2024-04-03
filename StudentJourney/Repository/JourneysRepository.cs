using ContosoJourney.Data;
using ContosoJourney.Models;
using Microsoft.EntityFrameworkCore;
using StudentJourney.Interfaces;

namespace StudentJourney.Repository
{
    public class JourneysRepository : IJourneysRepository
    {
        private readonly JourneyContext _context;
        public JourneysRepository(JourneyContext context)
        {
            _context = context;
        }
        public Task<List<Journey>> GetAllJourneys()
        {
            return _context.Journeys.ToListAsync();
        }
        public async Task<Task<Journey>> AddExamples(Journey journey)
        {
            _context.Journeys.AddRange(journey);
            await _context.SaveChangesAsync();
            return Task.FromResult(journey); // Zwracanie obiektu Journey jako zadanie
        }
        public async Task<Journey> ShowDetails(int? id)
        {
            var journey = await _context.Journeys.FirstOrDefaultAsync(m => m.JourneyID == id);
            return journey ?? throw new Exception($"Journey with ID {id} not found."); // Obsługa przypadku, gdy podróż o podanym ID nie została znaleziona
        }
        public async Task<Journey> CreateJourney(Journey journey)
        {
            _context.Add(journey);
            await _context.SaveChangesAsync();
            return journey;
        }
        public async Task<Journey> EditJourney(int? id)
        {
            return await _context.Journeys.FindAsync(id);

        }
        public async Task<Journey> PostEditJourney(Journey journey)
        {
            _context.Update(journey);
            await _context.SaveChangesAsync();
            return journey;
        }
        public async Task<Journey> GetDelete(int? id)
        {
            return await _context.Journeys
                .FirstOrDefaultAsync(m => m.JourneyID == id);
        }
        public async Task Delete(Journey journey)
        {
            _context.Journeys.Remove(journey);
            await _context.SaveChangesAsync();
        }
    }

}
