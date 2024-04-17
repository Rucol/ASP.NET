using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContosoJourney.Models
{
    public class Student
    {
        
        public int StudentID { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? EnrollmentDate { get; set; }
        public string? Pesel {  get; set; }

        public ICollection<Enrollment>? Enrollments { get; set; }
    }
    public class StudentValidator : AbstractValidator<Student>
    {
        public StudentValidator()
        {
            RuleFor(student => student.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .MaximumLength(50).WithMessage("First name must not exceed 50 characters.");

            RuleFor(student => student.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MaximumLength(50).WithMessage("Last name must not exceed 50 characters.");

            RuleFor(student => student.EnrollmentDate)
                .NotEmpty().WithMessage("Enrollment date is required.")
                .Must(BeAValidDate).WithMessage("Enrollment date must be a valid date.");

            RuleFor(student => student.Pesel)
                .NotEmpty().WithMessage("PESEL is required.")
                .Length(11).WithMessage("PESEL must be 11 characters long.")
                .Matches("^[0-9]*$").WithMessage("PESEL must contain only digits.");
        }

        private bool BeAValidDate(DateTime? date)
        { 
            return date <= DateTime.Now;
        }
    }
}