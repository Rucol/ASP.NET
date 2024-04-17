using FluentValidation;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContosoJourney.Models
{
    public class Journey
    {
        [Key]
        public int JourneyID { get; set; }
        public string? TripName { get; set; }
        public int Cost { get; set; }
        public int JourneyDuration { get; set; }
        public DateTime JourneyDate { get; set; }


        public ICollection<Enrollment>? Enrollments { get; set; }
        
    }
    public class JourneyValidator : AbstractValidator<Journey>
    {
        public JourneyValidator()
        {
            RuleFor(journey => journey.TripName)
                .MaximumLength(100).WithMessage("Trip name must not exceed 100 characters.");

            RuleFor(journey => journey.Cost)
                .GreaterThan(0).WithMessage("Cost must be greater than 0.");

            RuleFor(journey => journey.JourneyDuration)
                .GreaterThan(0).WithMessage("Journey duration must be greater than 0.");

            RuleFor(journey => journey.JourneyDate)
                .NotEmpty().WithMessage("Journey date is required.")
                .Must(BeAValidDate).WithMessage("Journey date must be a valid date.");
        }

        private bool BeAValidDate(DateTime date)
        {
            return date > DateTime.Now;
        }
    }


}