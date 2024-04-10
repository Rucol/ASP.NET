using ContosoJourney.Models;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace StudentJourney.ViewModels
{
    public class EnrollmentViewModel
    {
        public int JourneyID { get; set; }

        [Required]
        public int StudentID { get; set; }

        [Required]
        public int TripID { get; set; }

        public string StudentName { get; set; }
        public string TripName { get; set; }
        public int Cost { get; set; }
        public DateTime JourneyDate { get; set; }
        public int JourneyDuration { get; set; }

        public static EnrollmentViewModel FromEnrollment(Enrollment enrollment, IEnumerable<Student> students, IEnumerable<Journey> journeys)
        {
            var student = students.FirstOrDefault(s => s.StudentID == enrollment.StudentID);
            var journey = journeys.FirstOrDefault(j => j.JourneyID == enrollment.TripID);

            return new EnrollmentViewModel
            {
                JourneyID = enrollment.JourneyID,
                StudentID = enrollment.StudentID,
                TripID = enrollment.TripID,
                StudentName = student?.LastName ?? "",
                TripName = journey?.TripName ?? "",
                Cost = journey?.Cost ?? 0,
                JourneyDate = journey?.JourneyDate ?? DateTime.MinValue,
                JourneyDuration = journey?.JourneyDuration ?? 0
            };
        }
    }
}
