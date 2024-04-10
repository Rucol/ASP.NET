using ContosoJourney.Data;
using ContosoJourney.Models;
using Microsoft.EntityFrameworkCore;
using StudentJourney.Interfaces;
using StudentJourney.Controllers;

using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Query;



namespace StudentJourney.Repository;

public class EnrollmentsRepository : IEnrollmentRepository
{
    private readonly JourneyContext _context;
    public EnrollmentsRepository(JourneyContext context) 
    {
        _context = context;
    }
    public async Task<List<Enrollment>> GetAllAsync()
    {
        return await _context.Enrollments
                            .Include(e => e.Student)
                            .ToListAsync();
    }

    public Task<Enrollment> FirstOrDefault(int? id)
    {
        return _context.Enrollments
            .Include(e => e.Student)
            .FirstOrDefaultAsync(m => m.JourneyID == id); 
    }
    public async Task<List<Student>> StudentsIds()
    {
        return await _context.Students.ToListAsync();
    }
    public async Task<List<int>> JourneyCost()
    {
        return _context.Journeys.Select(s => s.Cost).ToList();
    }
    public async Task<List<Journey>> Journeys()
    {
        return await _context.Journeys.ToListAsync();
    }
    public Task<Enrollment> EditJourney(int? id)
    {
        return _context.Enrollments.FindAsync(id).AsTask();
    }
    public async Task<IEnumerable<int>> ViewDataStudentId()
    {
        return await _context.Students.Select(s => s.StudentID).ToListAsync();
    }
    public async Task<Enrollment> EditEnrollment(Enrollment enrollment)
    {
        _context.Update(enrollment);
        await _context.SaveChangesAsync();
        return enrollment;
    }
    public Task<Enrollment?> DeleteById(int? id)
    {
        return _context.Enrollments
                 .Include(e => e.Student)
                 .FirstOrDefaultAsync(m => m.JourneyID == id);
    }
    public async Task<Enrollment> DeleteEnrollement(int? id)
    {
        return await _context.Enrollments.FindAsync(id);
    }
    public async Task<Enrollment> RemoveEnrollement(int? id)
    {
        var enrollmentToRemove = await _context.Enrollments.FindAsync(id);
        if (enrollmentToRemove != null)
        {
            _context.Enrollments.Remove(enrollmentToRemove);
            await _context.SaveChangesAsync(); // Zapisz zmiany do bazy danych
        }
        return enrollmentToRemove;
    }
    public async Task<Enrollment> GetEnrollmentIfExists(int id)
    {
        if (_context.Enrollments.Any(e => e.JourneyID == id))
        {
            return await _context.Enrollments.FindAsync(id);
        }
        else
        {
            return null;
        }
    }



}
