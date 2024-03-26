using ContosoJourney.Models;

namespace StudentJourney.Interfaces
{
    public interface IJourneysRepository
    {
        Task<List<Journey>> GetAllJourneys();
        Task<Task<Journey>> AddExamples(Journey journey);
        Task<Journey> ShowDetails(int? id);
        Task<Journey> CreateJourney(Journey journey);
        Task<Journey> EditJourney(int? id);
        Task<Journey> PostEditJourney(Journey journey);
        Task<Journey> GetDelete(int? id);
        Task Delete(Journey journey);
    }
}
