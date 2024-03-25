using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContosoJourney.Models
{
    public class Journey
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int JourneyID { get; set; }
        public string? TripName { get; set; }
        public int Cost { get; set; }
        public int JourneyDuration { get; set; }
        public DateTime JourneyDate { get; set; }


        public ICollection<Enrollment>? Enrollments { get; set; }
    }
}