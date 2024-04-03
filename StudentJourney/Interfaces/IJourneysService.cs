using ContosoJourney.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentJourney.Services
{
    public interface IJourneyService
    {
        Task<List<Journey>> GetAllJourneys();
        Task<Journey> GetJourneyDetails(int? id);
        Task<Journey> CreateJourney(Journey journey);
        Task<Journey> UpdateJourney(Journey journey);
        Task<Journey> DeleteJourney(int id);
    }
}
