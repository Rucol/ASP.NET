using ContosoJourney.Models;
using StudentJourney.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentJourney.Services
{
    public class JourneyService : IJourneyService
    {
        private readonly IJourneysRepository _journeysRepository;

        public JourneyService(IJourneysRepository journeysRepository)
        {
            _journeysRepository = journeysRepository;
        }

        public async Task<List<Journey>> GetAllJourneys()
        {
            return await _journeysRepository.GetAllJourneys();
        }

        public async Task<Journey> GetJourneyDetails(int? id)
        {
            return await _journeysRepository.ShowDetails(id);
        }

        public async Task<Journey> CreateJourney(Journey journey)
        {
            return await _journeysRepository.CreateJourney(journey);
        }

        public async Task<Journey> UpdateJourney(Journey journey)
        {
            return await _journeysRepository.PostEditJourney(journey);
        }

        public async Task<Journey> DeleteJourney(int id)
        {
            var journeyToDelete = await _journeysRepository.GetDelete(id);
            if (journeyToDelete != null)
            {
                await _journeysRepository.Delete(journeyToDelete);
            }
            return null;
        }
    }
}
