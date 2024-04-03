using ContosoJourney.Models;
using StudentJourney.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentJourney.Services
{
    public interface IStudentService
    {
        Task<List<Student>> GetAllStudents();
        Task<Student> GetStudentDetails(int? id);
        Task<Student> CreateStudent(Student student);
        Task<Student> UpdateStudent(Student student);
        Task<Student> DeleteStudent(int id);
    }
}
