using ContosoJourney.Models;

namespace StudentJourney.Interfaces
{
    public interface IStudentRepository
    {
        Task<List<Student>> GetAll();
        Task<Student> GetDetails(int? id);
        Task<Student> Create(Student student);
 
    }
}
