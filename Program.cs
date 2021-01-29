using ConsoleTables;
using Microsoft.Data.SqlClient;
using StudentConsoleDB.Models;
using System;
using System.IO;
using System.Linq;
using System.Text;

namespace StudentConsoleDB
{
    class Program
    {
        static string connString = "Server=(localdb)\\mssqllocaldb;Database=EFCore;Trusted_Connection=True;";
        static string fileName = @"C:\test.txt";
        static void Main(string[] args)
        {
            Boolean Success = false;
            do
            {
                try
                {
                    Console.WriteLine(" \n Hello User!");

                    Console.WriteLine("Please select from the following options to continue");


                    Console.WriteLine("To Add Students, Press 1");
                    Console.WriteLine("To Read the Student Database, Press 2");
                    Console.WriteLine("To Update Student Details, Press 3");
                    Console.WriteLine("To Delete Student Details, Press 4");
                    Console.WriteLine("To Search for Students using various parameters, Press 5");
                    Console.WriteLine("To Exit the Program, Press 6");


                    Int32 UserInput = Convert.ToInt32(Console.ReadLine());

                    switch (UserInput)
                    {
                        case 1:
                            AddStudent();
                            break;
                        case 2:
                            ReadStudent();
                            break;
                        case 3:
                            UpdateStudent();
                            break;
                        case 4:
                            DeleteStudent();
                            break;
                        case 5:
                            SearchStudent();
                            break;
                        case 6:
                            Console.WriteLine("Goodbye!");
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine("Please choose an option from the above");
                            break;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Please enter a valid value \n");

                }
            } while (Success == false);
        }

        static void Validations()
        {


        }

        static void AddStudent()
        {
            Int32 UserInput;

            var db = new EFContext();

            do
            {
                Student student = new Student();
                Console.WriteLine("Enter the First name of the student");
                student.FirstName = Console.ReadLine().ToLower();

                Console.WriteLine("Enter the Last name of the student");
                student.LastName = Console.ReadLine().ToLower();

                Console.WriteLine("Enter the Date Of Birth of the student eg:(22/12/2021)");

                Boolean BadDate = true;
                do
                {
                    try
                    {
                        student.DateOfBirth = DateTime.Parse(Console.ReadLine());
                        BadDate = false;
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Please type in a valid date in the format dd/mm/yyyy");
                    }
                } while (BadDate == true);

                Console.WriteLine("Enter the Phone Number of the student");
                student.PhoneNum = Console.ReadLine().ToLower();

                Console.WriteLine("Enter the Address of the student");
                student.Address = Console.ReadLine().ToLower();

                Boolean BadAge = true;
                do
                {
                    try
                    {
                        Console.WriteLine("Enter the Age of the student");
                        student.Age = Convert.ToInt32(Console.ReadLine());
                        BadAge = false;
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Please type in a valid Age eg: 34");
                    }

                } while (BadAge == true);

                Boolean BadGender = true;

                do
                {

                    Console.WriteLine("Enter the Gender of the student eg: Male/Female/Unspecified");
                    String EnteredGender = Console.ReadLine().ToLower();
                    switch (EnteredGender)
                    {
                        case "male":
                        case "female":
                        case "unspecified":
                            student.Gender = EnteredGender.ToLower();
                            BadGender = false;
                            break;
                        default:
                            Console.WriteLine("Enter a valid Gender value");
                            break;
                    }
                } while (BadGender == true);

               

                db.Add(student);
                db.SaveChanges();
                

                Console.WriteLine("Do you want to enter the details of another student? Press 1 to enter the details or press any other key to return to the main menu");
                try
                {
                    UserInput = Convert.ToInt32(Console.ReadLine());
                }

                catch (FormatException)
                {
                    break;
                }

            }
            while (UserInput.Equals(1));

            WriteFile(fileName);
        }

        static void ReadStudent()
        {

            var db = new EFContext();

            var q = from Student in db.Students
                    select Student;
            var table = new ConsoleTable("First Name, Last Name, PhoneNum, Address, Date Of Birth, Age, Gender");
            foreach (var z in q) 
            {
                table.AddRow($"{z.FirstName} {"  "} {z.LastName}{"    "} {z.PhoneNum}{"  "}{z.Address}{"  "}{z.DateOfBirth} {z.Age} {z.Gender}");
            }
            table.Write();
            Console.WriteLine("\n");
        }

        static void UpdateStudent()
        {
            ReadStudent();

            Console.WriteLine("Which Student do you want to update the details for?");


            using (var db = new EFContext())
            {
                Console.WriteLine("To Update Student First Name, Press 1");
                Console.WriteLine("To Update Student Last Name, Press 2");
                Console.WriteLine("To Update Student Age Name, Press 3");
                Console.WriteLine("To Update Student Address Name, Press 4");
                Console.WriteLine("To Update Student Gender Name, Press 5");
                Console.WriteLine("To Update Student Phone Number Name, Press 6");
                Console.WriteLine("To Update Student DOB , Press 7");

                Int32 UserInput = Convert.ToInt32(Console.ReadLine());

                switch (UserInput)
                {
                    case 1:
                        Console.WriteLine("Enter Old First Name of the Student");
                        var FirstNameUpdate = db.Students.Single(x => x.FirstName == Console.ReadLine().ToLower());
                        Console.WriteLine("Enter new First Name of the Student");
                        FirstNameUpdate.FirstName = Console.ReadLine().ToLower();
                        db.SaveChanges();
                        ReadStudent();
                        break;

                    case 2:
                        Console.WriteLine("Enter Old Last Name of the Student");
                        var LastNameUpdate = db.Students.Single(x => x.LastName == Console.ReadLine().ToLower());
                        Console.WriteLine("Enter new Last Name of the Student");
                        LastNameUpdate.LastName = Console.ReadLine().ToLower();
                        db.SaveChanges();
                        ReadStudent();
                        break;

                    case 3:
                        Console.WriteLine("Enter Old Age of the Student");
                        var AgeUpdate = db.Students.Single(x => x.Age == Convert.ToInt32(Console.ReadLine()));
                        Console.WriteLine("Enter new Age of the Student");
                        AgeUpdate.Age = Convert.ToInt32(Console.ReadLine());
                        db.SaveChanges();
                        ReadStudent();
                        break;
                    case 4:
                        Console.WriteLine("Enter Old Address of the Student");
                        var AddressUpdate = db.Students.Single(x => x.Address == Console.ReadLine().ToLower());
                        Console.WriteLine("Enter new Address of the Student");
                        AddressUpdate.Address = Console.ReadLine().ToLower();
                        db.SaveChanges();
                        ReadStudent();
                        break;

                    case 5:
                        Console.WriteLine("Enter Old Gender of the Student");
                        var GenderUpdate = db.Students.Single(x => x.Gender == Console.ReadLine().ToLower());
                        Console.WriteLine("Enter new Gender of the Student");
                        GenderUpdate.Gender = Console.ReadLine().ToLower();
                        db.SaveChanges();
                        ReadStudent();
                        break;


                    case 6:
                        Console.WriteLine("Enter Old Phone Number of the Student");
                        var PhoneNumUpdate = db.Students.Single(x => x.PhoneNum == Console.ReadLine());
                        Console.WriteLine("Enter new Phone Number of the Student");
                        PhoneNumUpdate.PhoneNum = Console.ReadLine();
                        db.SaveChanges();
                        ReadStudent();
                        break;

                    case 7:
                        Console.WriteLine("Enter Old DOB of the Student");
                        var DOBUpdate = db.Students.Single(x => x.DateOfBirth == DateTime.Parse(Console.ReadLine()));
                        Console.WriteLine("Enter new DOB of the Student");
                        DOBUpdate.DateOfBirth = DateTime.Parse(Console.ReadLine());
                        db.SaveChanges();
                        ReadStudent();
                        break;

                    default:
                        Console.WriteLine("Enter a valid value");
                        break;
                }
            }

        }

        static void SearchStudent()
        {
            Console.WriteLine("---------------------------------Search-------------------------------------");
            Console.WriteLine("To Search by First Name, Press 1");
            Console.WriteLine("To Search by Gender, Press 2");
            Console.WriteLine("To Search by Age and find students greater than entered Age, Press 3");



            var db = new EFContext();


            Int32 UserInput = Convert.ToInt32(Console.ReadLine());

            switch (UserInput)
            {
                case 1:
                    Console.WriteLine("Enter First Name");
                    var Fquery = from Student in db.Students
                                 where Student.FirstName.Equals(Console.ReadLine().ToLower())
                                 select Student;

                    var table = new ConsoleTable("First Name, Last Name, PhoneNum, Address, Date Of Birth, Age, Gender");
                    foreach (var z in Fquery)
                    {

                        table.AddRow($"{z.FirstName} {"  "} {z.LastName}{"    "} {z.PhoneNum}{"  "}{z.Address}{"  "}{z.DateOfBirth} {z.Age} {z.Gender}");

                    }
                    table.Write();
                    Console.WriteLine("\n");
                    break;
                case 2:
                    Console.WriteLine("Enter Gender Name");
                    var Gquery = from Student in db.Students
                                 where Student.Gender.Equals(Console.ReadLine().ToLower())
                                 select Student;

                    var table1 = new ConsoleTable("First Name, Last Name, PhoneNum, Address, Date Of Birth, Age, Gender");
                    foreach (var z in Gquery)
                    {

                        table1.AddRow($"{z.FirstName} {"  "} {z.LastName}{"    "} {z.PhoneNum}{"  "}{z.Address}{"  "}{z.DateOfBirth} {z.Age} {z.Gender}");

                    }
                    table1.Write();
                    Console.WriteLine("\n");
                    break;
                case 3:
                    Console.WriteLine("Enter Age to find Students older than entered Age");
                    var Aquery = from Student in db.Students
                                 where Student.Age > Convert.ToInt32(Console.ReadLine())
                                 select Student;

                    var table2 = new ConsoleTable("First Name, Last Name, PhoneNum, Address, Date Of Birth, Age, Gender");
                    foreach (var z in Aquery)
                    {

                        table2.AddRow($"{z.FirstName} {"  "} {z.LastName}{"    "} {z.PhoneNum}{"  "}{z.Address}{"  "}{z.DateOfBirth} {z.Age} {z.Gender}");

                    }
                    table2.Write();
                    Console.WriteLine("\n");
                    break;
                default:
                    Console.WriteLine("Please choose an option from the above");
                    break;
            }

        }

        static void DeleteStudent()
        {
            Console.WriteLine("---------------------------------Delete-------------------------------------");
            ReadStudent();

            Console.WriteLine("\n Which Student do you want to delete? Enter their First Name");
            var db = new EFContext();

            var x = (from Student in db.Students
                     where Student.FirstName.Equals(Console.ReadLine().ToLower())
                     select Student).First();

            db.Students.Remove(x);
            db.SaveChanges();

            Console.WriteLine("\n Record has been deleted");

            ReadStudent();

        }

        static void WriteFile(string fileName)
        {
            SqlCommand comm = new SqlCommand();
            comm.Connection = new SqlConnection(connString);
            String sql = @"select * from Students";

            comm.CommandText = sql;
            comm.Connection.Open();

            SqlDataReader sqlReader = comm.ExecuteReader();
            int count = sqlReader.FieldCount;

            // Change the Encoding to what you need here (UTF8, Unicode, etc)
            using (StreamWriter file = new StreamWriter(fileName, false))
            {
                while (sqlReader.Read())
                {
                    for (int i = 0; i < count; i++)
                    { 
                        file.WriteLine(sqlReader.GetValue(i));
                    }
                }
            }

            sqlReader.Close();
            comm.Connection.Close();
        }



    }
}
