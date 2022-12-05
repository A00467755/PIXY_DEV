using Microsoft.EntityFrameworkCore;
using PIXY.Models;
using System;
using System.Linq;

namespace PIXY.Data
{
    public static class DbInitializer
    {
        public static void Initialize(PIXYContext context)
        {

            context.Database.EnsureCreated();


            if (context.Users.Any())
            {
                return;   // DB has been seeded
            }

            var users = new User[]
            {
            new User{FirstName="Carson",LastName="Alexander",Email="abc@abc.com",PhoneNumber="3232323",UserName="AlexC",Password="dfsdfsd"}
            };

            foreach (User s in users)
            {
                context.Users.Add(s);
            }
            context.SaveChanges();


            if (context.Images.Any())
            {
                return;   // DB has been seeded
            }
            
            var images = new Image[]
            {
            new Image{ UserId =1,CategoryDesc="Natural",ImageType="jpg",FilePathWatermark="\\images\\photo\\001_w.jpg",FilePath="\\images\\photo\\001.jpg",Price=10.1,HaveHardcopy=true},
            new Image{ UserId =1,CategoryDesc="Landscape",ImageType="jpg",FilePathWatermark="\\images\\photo\\002_w.jpg",FilePath="\\images\\photo\\002.jpg",Price=10.1,HaveHardcopy=true},
            new Image{ UserId =2,CategoryDesc="Food",ImageType="jpg",FilePathWatermark="\\images\\photo\\003_w.jpg",FilePath="\\images\\photo\\003.jpg",Price=10.1,HaveHardcopy=true},
            new Image{ UserId =2,CategoryDesc="Landscape",ImageType="jpg",FilePathWatermark="\\images\\photo\\004_w.jpg",FilePath="\\images\\photo\\004.jpg",Price=10.1,HaveHardcopy=true}
            };

            foreach (Image s in images)
            {
                context.Images.Add(s);
            }

            context.SaveChanges();


            if (context.Carts.Any())
            {
                return;   // DB has been seeded
            }

            var carts = new Cart[]
            {
            new Cart{ UserId =1,ImageId=1,IsHardcopy=false,NoOfHardcopy=0}
            };

            foreach (Cart s in carts)
            {
                context.Carts.Add(s);
            }

            context.SaveChanges();

            if (context.Transactions.Any())
            {
                return;   // DB has been seeded
            }

            var transactions = new Transaction[]
            {
                new Transaction{UserId =1,ImageId=2,IsHardcopy=false,NoOfHardcopy=0,PurchaseDataTime=DateTime.Parse("2005-09-01")}
            };

            foreach (Transaction s in transactions)
            {
                context.Transactions.Add(s);
            }

            context.SaveChanges();

            if (context.PurchasedItems.Any())
            {
                return;   // DB has been seeded
            }

            var purchasedItems = new PurchasedItem[]
            {
            new PurchasedItem{ UserId =1,ImageId=4,IsHardcopy=false,NoOfHardcopy=0}
            };

            foreach (PurchasedItem s in purchasedItems)
            {
                context.PurchasedItems.Add(s);
            }

            context.SaveChanges();
            // Look for any students.
            if (context.Students.Any())
            {
                return;   // DB has been seeded
            }

            var students = new Student[]
            {
            new Student{FirstName="Carson",LastName="Alexander",EnrollmentDate=DateTime.Parse("2005-09-01"),Anumber="A00460001"},
            new Student{FirstName="Meredith",LastName="Alonso",EnrollmentDate=DateTime.Parse("2002-09-01"),Anumber="A00460002"},
            new Student{FirstName="Arturo",LastName="Anand",EnrollmentDate=DateTime.Parse("2003-09-01"),Anumber="A00460003"},
            new Student{FirstName="Gytis",LastName="Barzdukas",EnrollmentDate=DateTime.Parse("2002-09-01"),Anumber="A00460004"},
            new Student{FirstName="Yan",LastName="Li",EnrollmentDate=DateTime.Parse("2002-09-01"),Anumber="A00460005"},
            new Student{FirstName="Peggy",LastName="Justice",EnrollmentDate=DateTime.Parse("2001-09-01"),Anumber="A00460006"},
            new Student{FirstName="Laura",LastName="Norman",EnrollmentDate=DateTime.Parse("2003-09-01"),Anumber="A00460007"},
            new Student{FirstName="Nino",LastName="Olivetto",EnrollmentDate=DateTime.Parse("2005-09-01"),Anumber="A00460008"}
            };
            foreach (Student s in students)
            {
                context.Students.Add(s);
            }
            context.SaveChanges();

            var courses = new Course[]
            {
            new Course{CourseID=1050,Title="Chemistry",Credits=3,CourseNumber=5510,Crn=17756},
            new Course{CourseID=4022,Title="Microeconomics",Credits=3,CourseNumber=5520,Crn=17757},
            new Course{CourseID=4041,Title="Macroeconomics",Credits=3,CourseNumber=55310,Crn=17758},
            new Course{CourseID=1045,Title="Calculus",Credits=4,CourseNumber=5540,Crn=17759},
            new Course{CourseID=3141,Title="Trigonometry",Credits=4,CourseNumber=5550,Crn=17760},
            new Course{CourseID=2021,Title="Composition",Credits=3,CourseNumber=5560,Crn=17761},
            new Course{CourseID=2042,Title="Literature",Credits=4,CourseNumber=5570,Crn=17762}
            };
            foreach (Course c in courses)
            {
                context.Courses.Add(c);
            }
            context.SaveChanges();

            var enrollments = new Enrollment[]
            {
            new Enrollment{StudentID=1,CourseID=1050,Grade=Grade.A},
            new Enrollment{StudentID=1,CourseID=4022,Grade=Grade.C},
            new Enrollment{StudentID=1,CourseID=4041,Grade=Grade.B},
            new Enrollment{StudentID=2,CourseID=1045,Grade=Grade.B},
            new Enrollment{StudentID=2,CourseID=3141,Grade=Grade.F},
            new Enrollment{StudentID=2,CourseID=2021,Grade=Grade.F},
            new Enrollment{StudentID=3,CourseID=1050},
            new Enrollment{StudentID=4,CourseID=1050},
            new Enrollment{StudentID=4,CourseID=4022,Grade=Grade.F},
            new Enrollment{StudentID=5,CourseID=4041,Grade=Grade.C},
            new Enrollment{StudentID=6,CourseID=1045},
            new Enrollment{StudentID=7,CourseID=3141,Grade=Grade.A},
            };
            foreach (Enrollment e in enrollments)
            {
                context.Enrollments.Add(e);
            }
            context.SaveChanges();
        }
    }
}