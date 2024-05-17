using ContosoJourney.Models;
using System.ComponentModel.DataAnnotations;

namespace StudentJourney.ViewModels
{
    
    public class JourneyViewModel
    {
        public int JourneyID { get; set; }

        [Required(ErrorMessage = "Nazwa wycieczki jest wymagana")]
        [StringLength(50, ErrorMessage = "Nazwa wycieczki nie może być dłuższa niż 50 znaków")]
        public string TripName { get; set; }

        [Required(ErrorMessage = "Koszt wycieczki jest wymagany")]
        [Range(0, int.MaxValue, ErrorMessage = "Koszt wycieczki musi być liczbą dodatnią")]
        public int Cost { get; set; }
        public DateTime JourneyDate { get; set; }
        public int JourneyDuration { get; set; }
        public Journey ToJourney()
        {
            return new Journey
            {
                TripName = this.TripName,
                Cost = this.Cost,
                JourneyDate = this.JourneyDate,
                JourneyDuration = this.JourneyDuration
            };
        }

    }
}
