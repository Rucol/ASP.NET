using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
}