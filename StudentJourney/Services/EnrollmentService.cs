using ContosoJourney.Models;
using StudentJourney.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace StudentJourney.Services
{
    public class EnrollmentService : IEnrollmentService
    {
        private readonly IEnrollmentRepository _enrollmentRepository;

        public EnrollmentService(IEnrollmentRepository enrollmentRepository)
        {
            _enrollmentRepository = enrollmentRepository;
        }

        public async Task<List<Enrollment>> GetAllEnrollments()
        {
            return await _enrollmentRepository.GetAllAsync();
        }

        public async Task<Enrollment> GetEnrollmentDetails(int? id)
        {
            return await _enrollmentRepository.FirstOrDefault(id);
        }

        public async Task<Enrollment> CreateEnrollment(Enrollment enrollment)
        {
            return await _enrollmentRepository.EditEnrollment(enrollment);
        }

        public async Task<Enrollment> UpdateEnrollment(Enrollment enrollment)
        {
            return await _enrollmentRepository.EditEnrollment(enrollment);
        }

        public async Task<Enrollment> DeleteEnrollment(int id)
        {
            return await _enrollmentRepository.RemoveEnrollement(id);
        }
    }
}
