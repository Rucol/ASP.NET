using ContosoJourney.Models;
using StudentJourney.ViewModels;

namespace StudentJourney.Extensions
{
    public static class JourneyExtensions
    {
        public static JourneyViewModel ToJourneyViewModel(this Journey journey)
        {
            if (journey == null)
            {
                throw new ArgumentNullException(nameof(journey));
            }

            return new JourneyViewModel
            {
                JourneyID = journey.JourneyID,
                TripName = journey.TripName,
                Cost = journey.Cost,
                JourneyDate = journey.JourneyDate,
                JourneyDuration = journey.JourneyDuration
            };
        }
    }
}
