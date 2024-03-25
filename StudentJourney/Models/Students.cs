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
}