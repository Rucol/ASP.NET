using StudentJourney.Models;
using StudentJourney.Controllers;
using ContosoJourney.Models;
using Microsoft.EntityFrameworkCore.Query;
using System.Collections;

namespace StudentJourney.Interfaces
{
    public interface IEnrollmentRepository
    {
        Task<List<Enrollment>> GetAllAsync();
        Task<Enrollment> FirstOrDefault(int? id);
        Task<List<Student>> StudentsIds();
        Task<List<int>> JourneyCost();
        Task<List<Journey>> Journeys();
        Task<Enrollment> EditJourney(int? id);
        Task<IEnumerable<int>> ViewDataStudentId();
        Task<Enrollment> EditEnrollment(Enrollment enrollment);
        Task<Enrollment?> DeleteById(int? id);
        Task<Enrollment> DeleteEnrollement(int? id);
        Task<Enrollment>RemoveEnrollement(int? id);
        Task<Enrollment> GetEnrollmentIfExists(int id);
    }
}
