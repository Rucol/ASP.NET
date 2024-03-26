using StudentJourney.Interfaces;
using StudentJourney.Controllers;
using StudentJourney.Models;
using ContosoJourney.Data;
using ContosoJourney.Models;
using Microsoft.EntityFrameworkCore;

namespace StudentJourney.Repository
{
    public class StudentsRepository : IStudentRepository
    {
        private readonly JourneyContext _context;
        public StudentsRepository(JourneyContext context)
        {
            _context = context;

        }
        public async Task<List<Student>> GetAll()
        {
            return await _context.Students.ToListAsync();
        }
        public async Task<Student> GetDetails(int? id)
        {
            return await _context.Students
                .FirstOrDefaultAsync(m => m.StudentID == id);

        }
        public async Task<Student> Create(Student student)
        {
            _context.Add(student);
            await _context.SaveChangesAsync();
            return student;
        }
        public async Task<Student> FindStudentAsync(int? id)
        {
            return await _context.Students.FindAsync(id);
        }
        public async Task<Student> PostEdit(Student student)
        {
            _context.Update(student);
            await _context.SaveChangesAsync();

            return student;
        }
        public async Task<Student> DeleteStudent(Student student)
        {
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return null;
        }
    }
}
