using ContosoJourney.Data;
using ContosoJourney.Models;
using System;
using System.Linq;

namespace ContosoUniversity.Data
{
    public static class DbInitializer
    {
        public static void Initialize(JourneyContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Students.Any())
            {
                return;   // DB has been seeded
            }

            var students = new Student[]
            {
            new Student{FirstMidName="Carson",LastName="Alexander",EnrollmentDate=DateTime.Parse("2005-09-01")},
            new Student{FirstMidName="Meredith",LastName="Alonso",EnrollmentDate=DateTime.Parse("2002-09-01")},
            new Student{FirstMidName="Arturo",LastName="Anand",EnrollmentDate=DateTime.Parse("2003-09-01")},
            new Student{FirstMidName="Gytis",LastName="Barzdukas",EnrollmentDate=DateTime.Parse("2002-09-01")},
            new Student{FirstMidName="Yan",LastName="Li",EnrollmentDate=DateTime.Parse("2002-09-01")},
            new Student{FirstMidName="Peggy",LastName="Justice",EnrollmentDate=DateTime.Parse("2001-09-01")},
            new Student{FirstMidName="Laura",LastName="Norman",EnrollmentDate=DateTime.Parse("2003-09-01")},
            new Student{FirstMidName="Nino",LastName="Olivetto",EnrollmentDate=DateTime.Parse("2005-09-01")}
            };
            foreach (Student s in students)
            {
                context.Students.Add(s);
            }
            context.SaveChanges();

            var Journeys = new Journey[]
            {
            new Journey{JourneyID=1050,TripName="Paris",Cost=1050},
            new Journey{JourneyID=4022,TripName="Tokyo",Cost=4022},
            new Journey{JourneyID=4041,TripName="Rome",Cost=4041},
            new Journey{JourneyID=1045,TripName="Sydney",Cost=1045},
            new Journey{JourneyID=3141,TripName="Rio de Janeiro",Cost=2050},
            new Journey{JourneyID=2021,TripName="Istanbul",Cost=2000},
            new Journey{JourneyID=2042,TripName="New York City",Cost=2060}
            };
            foreach (Journey c in Journeys)
            {
                context.Journeys.Add(c);
            }
            context.SaveChanges();

            var enrollments = new Enrollment[]
            {
            new Enrollment{StudentID=1,JourneyID=1050},
            new Enrollment{StudentID=1,JourneyID=4022},
            new Enrollment{StudentID=1,JourneyID=4041},
            new Enrollment{StudentID=2,JourneyID=1045},
            new Enrollment{StudentID=2,JourneyID=3141},
            new Enrollment{StudentID=2,JourneyID=2021},
            new Enrollment{StudentID=3,JourneyID=1050},
            new Enrollment{StudentID=4,JourneyID=1050},
            new Enrollment{StudentID=4,JourneyID=4022},
            new Enrollment{StudentID=5,JourneyID=4041},
            new Enrollment{StudentID=6,JourneyID=1045},
            new Enrollment{StudentID=7,JourneyID=3141},
            };
            foreach (Enrollment e in enrollments)
            {
                context.Enrollments.Add(e);
            }
            context.SaveChanges();
        }
    }
}