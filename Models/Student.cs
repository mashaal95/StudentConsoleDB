using System;
using System.Collections.Generic;
using System.Text;

namespace StudentConsoleDB.Models
{

    //The model for the DB is established in this class
    public class Student
    {
        public int Id { get; set; }

        public String FirstName { get; set; }

        public String LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public String Address { get; set; }

        public String PhoneNum { get; set; }

        public int Age { get; set; }

        public String Gender { get; set; }


    }
}
