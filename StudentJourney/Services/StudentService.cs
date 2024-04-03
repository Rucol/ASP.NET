using ContosoJourney.Models;
using StudentJourney.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentJourney.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;

        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<List<Student>> GetAllStudents()
        {
            return await _studentRepository.GetAll();
        }

        public async Task<Student> GetStudentDetails(int? id)
        {
            return await _studentRepository.GetDetails(id);
        }

        public async Task<Student> CreateStudent(Student student)
        {
            return await _studentRepository.Create(student);
        }

        public async Task<Student> UpdateStudent(Student student)
        {
            return await _studentRepository.PostEdit(student);
        }

        public async Task<Student> DeleteStudent(int id)
        {
            var studentToDelete = await _studentRepository.FindStudentAsync(id);
            if (studentToDelete != null)
            {
                await _studentRepository.DeleteStudent(studentToDelete);
            }
            return null;
        }
    }
}
