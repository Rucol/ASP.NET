using ContosoJourney.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentJourney.Services
{
    public interface IEnrollmentService
    {
        Task<List<Enrollment>> GetAllEnrollments();
        Task<Enrollment> GetEnrollmentDetails(int? id);
        Task<Enrollment> CreateEnrollment(Enrollment enrollment);
        Task<Enrollment> UpdateEnrollment(Enrollment enrollment);
        Task<Enrollment> DeleteEnrollment(int id);
    }
}
