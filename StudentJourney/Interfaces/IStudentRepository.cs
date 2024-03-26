using ContosoJourney.Models;

namespace StudentJourney.Interfaces
{
    public interface IStudentRepository
    {
        Task<List<Student>> GetAll();
        Task<Student> GetDetails(int? id);
        Task<Student> Create(Student student);
        Task<Student> FindStudentAsync(int? id); 
        Task<Student> PostEdit(Student student);
        Task<Student> DeleteStudent(Student student);
    }
}
