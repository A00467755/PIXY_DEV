using System;
using System.Collections.Generic;

namespace PIXY.Models
{
    public class Student
    {
        public int ID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public string Anumber { get; set; }

        public ICollection<Enrollment>? Enrollments { get; set; }
    }
}