using System;
using System.Collections.Generic;
using System.IO;

namespace StudentManagement
{
    public class Student
    {
        // Properties representing student information
        public int ID { get; set; }
        public string fName { get; set; }
        public string lName { get; set; }
        public DateTime DOB { get; set; }
        public string major { get; set; }
        public List<CourseGrade> courses { get; set; } // Renamed 'classes' to 'courses'
        public bool isEnrolled { get; set; }

        // Default constructor with initial values
        public Student()
        {
            ID = 0;
            fName = string.Empty;
            lName = string.Empty;
            DOB = DateTime.MinValue;
            major = string.Empty;
            courses = new List<CourseGrade>(); // Use 'courses' instead of 'classes'
            isEnrolled = false;
        }

        // Constructor to initialize a student with given values
        public Student(int id, string fname, string lname, DateTime dob, string major, List<CourseGrade> courses, bool isEnrolled)
        {
            this.ID = id;
            this.fName = fname;
            this.lName = lname;
            this.DOB = dob;
            this.major = major;
            this.courses = courses ?? new List<CourseGrade>(); // Correctly using 'courses'
            this.isEnrolled = isEnrolled;
        }

        // Method to create a student by collecting input
        public void CreateStudent()
        {
            ID = GetValidID();
            Console.WriteLine("First Name:");
            fName = Console.ReadLine() ?? "Unknown";
            Console.WriteLine("Last Name:");
            lName = Console.ReadLine() ?? "Unknown";
            Console.WriteLine("DOB (yyyy-mm-dd):");
            DOB = GetValidDateOfBirth();
            isEnrolled = GetValidEnrollmentStatus();
            
            if (isEnrolled)
            {
                Console.WriteLine("Major:");
                major = Console.ReadLine() ?? "Undeclared";
                Console.WriteLine("Classes (comma separated, format: ClassName:Grade):");
                string classesInput = Console.ReadLine();
                courses = string.IsNullOrEmpty(classesInput) ? new List<CourseGrade>() : new List<CourseGrade>(ParseClassesFromInput(classesInput));
            }
            else
            {
                major = "N/A";
                courses = new List<CourseGrade> { new CourseGrade { ClassName = "N/A", Grade = 0 } };
            }
        }

        // Helper method for ID validation
        private int GetValidID()
        {
            Console.WriteLine("Enter ID:");
            string input = Console.ReadLine();
            int id;
            while (string.IsNullOrEmpty(input) || !int.TryParse(input, out id))
            {
                Console.WriteLine("Invalid input. Please enter a valid ID:");
                input = Console.ReadLine();
            }
            return id;
        }

        // Method for date validation
        private DateTime GetValidDateOfBirth()
        {
            string? dobInput = Console.ReadLine();
            DateTime tempDOB;
            while (string.IsNullOrEmpty(dobInput) || !DateTime.TryParse(dobInput, out tempDOB))
            {
                Console.WriteLine("Invalid date format. Please enter DOB in yyyy-mm-dd format:");
                dobInput = Console.ReadLine();
            }
            return tempDOB;
        }

        // Method to validate enrollment status
        private bool GetValidEnrollmentStatus()
        {
            Console.WriteLine("Are you enrolled? (true/false):");
            string? enrolledInput = Console.ReadLine();
            bool tempEnrolled;
            while (string.IsNullOrEmpty(enrolledInput) || !bool.TryParse(enrolledInput, out tempEnrolled))
            {
                Console.WriteLine("Invalid input. Please enter true or false:");
                enrolledInput = Console.ReadLine();
            }
            return tempEnrolled;
        }

        // Parse classes from user input
        private List<CourseGrade> ParseClassesFromInput(string classesInput)
        {
            var classList = new List<CourseGrade>();
            var classEntries = classesInput.Split(',');
            foreach (var entry in classEntries)
            {
                var parts = entry.Split(':');
                if (parts.Length == 2 && double.TryParse(parts[1], out double grade))
                {
                    classList.Add(new CourseGrade { ClassName = parts[0].Trim(), Grade = grade });
                }
            }
            return classList;
        }

        // Calculate the average grade of the courses
        public double CalculateAverage()
        {
            if (courses.Count == 0) return 0;
            double total = 0;
            foreach (var course in courses)
            {
                total += course.Grade;
            }
            return total / courses.Count;
        }

        // Convert student information to CSV format
        public string ToCsv()
        {
            return $"{ID},{fName},{lName},{DOB:yyyy-MM-dd},{major},\"{string.Join(";", courses)}\",{isEnrolled}";
        }

        // Method to save student data to a CSV file
        public static void SaveToCsv(string filePath, List<Student> students)
        {
            bool fileExists = File.Exists(filePath); // Check if the CSV file already exists
            using (StreamWriter writer = new StreamWriter(filePath, append: fileExists))
            {
                // Write header row if the file is new
                if (!fileExists)
                {
                    writer.WriteLine("ID,First Name,Last Name,DOB,Major,Classes,Is Enrolled,Average Grade");
                }

                // Write each student's details to the file
                foreach (var student in students)
                {
                    writer.WriteLine($"{student.ToCsv()},{Math.Round(student.CalculateAverage(), 2)}");
                }
            }
        }

        // Method to load student data from a CSV file
        public static List<Student> LoadFromCsv(string filePath)
        {
            var students = new List<Student>();

            if (File.Exists(filePath))
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string? headerLine = reader.ReadLine(); // Read the header line
                    string? line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        students.Add(ParseStudentFromCsv(line));
                    }
                }
            }

            return students;
        }

        // Method to parse a student from a CSV line
        private static Student ParseStudentFromCsv(string csvLine)
        {
            string[] values = csvLine.Split(',');

            if (values.Length < 7)
            {
                throw new FormatException("CSV line does not contain enough values.");
            }

            if (!int.TryParse(values[0], out int id))
            {
                throw new FormatException($"Invalid ID value: {values[0]}");
            }

            string fName = values[1];
            string lName = values[2];

            if (!DateTime.TryParse(values[3], out DateTime dob))
            {
                throw new FormatException($"Invalid DOB value: {values[3]}");
            }

            string major = values[4];
            List<CourseGrade> courses = ParseCourses(values[5]);

            if (!bool.TryParse(values[6], out bool isEnrolled))
            {
                throw new FormatException($"Invalid enrollment value: {values[6]}");
            }

            return new Student(id, fName, lName, dob, major, courses, isEnrolled);
        }

        // Method to parse courses from a CSV field
        private static List<CourseGrade> ParseCourses(string coursesField)
        {
            List<CourseGrade> courses = new List<CourseGrade>();
            if (coursesField != "N/A")
            {
                string[] classEntries = coursesField.Split(';');
                foreach (string entry in classEntries)
                {
                    string[] parts = entry.Split(':');
                    if (parts.Length == 2 && double.TryParse(parts[1], out double grade))
                    {
                        courses.Add(new CourseGrade { ClassName = parts[0].Trim(), Grade = grade });
                    }
                    else
                    {
                        courses.Add(new CourseGrade { ClassName = parts[0].Trim(), Grade = 0 });
                    }
                }
            }
            else
            {
                courses.Add(new CourseGrade { ClassName = "N/A", Grade = 0 });
            }
            return courses;
        }
    }

    // Class to represent a course with a grade
    public class CourseGrade
    {
        public string ClassName { get; set; } = string.Empty;
        public double Grade { get; set; }

        public override string ToString()
        {
            return $"{ClassName}:{Grade}";
        }
    }
}