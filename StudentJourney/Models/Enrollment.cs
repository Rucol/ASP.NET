using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FluentValidation;

namespace ContosoJourney.Models
{


    public class Enrollment
    {
        [Key]
        
        public int JourneyID { get; set; }
        
        public int StudentID { get; set; }
        public int TripID { get; set; }
        public Student? Student { get; set; }
        public Journey? Journey { get; set; }

    }
    public class EnrollmentValidator : AbstractValidator<Enrollment>
    {
        public EnrollmentValidator()
        {
            RuleFor(enrollment => enrollment.JourneyID).NotEmpty().WithMessage("Journey ID is required.");
            RuleFor(enrollment => enrollment.StudentID).NotEmpty().WithMessage("Student ID is required.");
            RuleFor(enrollment => enrollment.TripID).NotEmpty().WithMessage("Trip ID is required.");
        }
    }
}