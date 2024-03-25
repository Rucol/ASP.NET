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
                 new Student{FirstName="Carson",LastName="Alexander",EnrollmentDate=DateTime.Parse("2005-09-01"), Pesel = "85091912345"},
                 new Student{FirstName="Meredith",LastName="Alonso",EnrollmentDate=DateTime.Parse("2002-09-01"), Pesel = "02100123456"},
                 new Student{FirstName="Arturo",LastName="Anand",EnrollmentDate=DateTime.Parse("2003-09-01"), Pesel = "03120134567"},
                 new Student{FirstName="Gytis",LastName="Barzdukas",EnrollmentDate=DateTime.Parse("2002-09-01"), Pesel = "02050145678"},
                 new Student{FirstName="Yan",LastName="Li",EnrollmentDate=DateTime.Parse("2002-09-01"), Pesel = "02100156789"},
                 new Student{FirstName="Peggy",LastName="Justice",EnrollmentDate=DateTime.Parse("2001-09-01"), Pesel = "01030167890"},
                 new Student{FirstName="Laura",LastName="Norman",EnrollmentDate=DateTime.Parse("2003-09-01"), Pesel = "03060178901"},
                 new Student{FirstName="Nino",LastName="Olivetto",EnrollmentDate=DateTime.Parse("2005-09-01"), Pesel = "05120189012"}

            };
            foreach (Student s in students)
            {
                context.Students.Add(s);
            }
            context.SaveChanges();

            var Journeys = new Journey[]
            {
            new Journey{JourneyID=1050, TripName="Paris", Cost=1050, JourneyDuration=2, JourneyDate=DateTime.Parse("2024-05-14")},
            new Journey{JourneyID=4022, TripName="Tokyo", Cost=4022, JourneyDuration=3, JourneyDate=DateTime.Parse("2024-07-20")},
            new Journey{JourneyID=4041, TripName="Rome", Cost=4041, JourneyDuration=4, JourneyDate=DateTime.Parse("2024-08-10")},
            new Journey{JourneyID=1045, TripName="Sydney", Cost=1045, JourneyDuration=5, JourneyDate=DateTime.Parse("2024-09-05")},
            new Journey{JourneyID=3141, TripName="Rio de Janeiro", Cost=2050, JourneyDuration=6, JourneyDate=DateTime.Parse("2024-10-15")},
            new Journey{JourneyID=2021, TripName="Istanbul", Cost=2000, JourneyDuration=7, JourneyDate=DateTime.Parse("2024-11-20")},
            new Journey{JourneyID=2042, TripName="New York City", Cost=2060, JourneyDuration=8, JourneyDate=DateTime.Parse("2024-12-25")}

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