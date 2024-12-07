using System;
using System.Collections.Generic;
using System.IO;
using StudentManagement;

namespace StudentManagement
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Student> students = new List<Student>(); // List to store student objects
            string filePath = "students.csv"; // Path to the CSV file

            // Load student data from the CSV file
            try
            {
                students = Student.LoadFromCsv(filePath);
            }
            catch (IOException ex)
            {
                Console.WriteLine($"An error occurred while reading the file: {ex.Message}");
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"An error occurred while parsing the file: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }

            bool running = true;

            while (running)
            {
                // Display the main menu
                DisplayMenu();
                string userChoice = Console.ReadLine()?.Trim();

                switch (userChoice)
                {
                    case "1":
                        // View Students
                        ViewStudents(students);
                        break;
                    case "2":
                        // Add Student
                        AddStudent(students);
                        break;
                    case "3":
                        // Edit Student
                        EditStudent(students);
                        break;
                    case "4":
                        // Delete Student
                        DeleteStudent(students, filePath);
                        break;
                    case "5":
                        // Save and Exit
                        SaveStudents(students, filePath);
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please select a valid number.");
                        break;
                }
            }
        }

        // Method to display the menu
        static void DisplayMenu()
        {
            Console.Clear();
            Console.WriteLine("=== Student Management System ===");
            Console.WriteLine("1. View Students");
            Console.WriteLine("2. Add Student");
            Console.WriteLine("3. Edit Student");
            Console.WriteLine("4. Delete Student");
            Console.WriteLine("5. Save and Exit");
            Console.WriteLine("Select an option (1-5):");
        }

        // Method to view all students
        static void ViewStudents(List<Student> students)
        {
            Console.Clear();
            Console.WriteLine("=== List of Students ===");
            foreach (var student in students)
            {
                string classDetails = student.courses.Count == 0
                                      ? "No courses assigned"
                                      : string.Join(", ", student.courses.ConvertAll(c => c.ToString()));

                Console.WriteLine($"ID: {student.ID}, Name: {student.fName} {student.lName}, DOB: {student.DOB:yyyy-MM-dd}, Major: {student.major}, Classes: {classDetails}, Enrolled: {student.isEnrolled}, Average Grade: {Math.Round(student.CalculateAverage(), 2)}");
            }

            Console.WriteLine("\nPress Enter to return to the menu.");
            Console.ReadLine();
        }

        // Method to add a new student
        static void AddStudent(List<Student> students)
        {
            Console.Clear();
            Student newStudent = new Student(); // Create a new student
            newStudent.CreateStudent(); // Collect details for the student
            students.Add(newStudent); // Add the student to the list
            Console.WriteLine("Student added successfully!");

            Console.WriteLine("\nPress Enter to return to the menu.");
            Console.ReadLine();
        }

        // Method to edit an existing student
        static void EditStudent(List<Student> students)
        {
            Console.Clear();
            Console.WriteLine("Enter the ID of the student you want to edit:");
            int id = int.Parse(Console.ReadLine() ?? "0");
            var studentToEdit = students.Find(s => s.ID == id);

            if (studentToEdit != null)
            {
                Console.WriteLine("Editing student...");
                studentToEdit.CreateStudent(); // Recollect details for editing
                Console.WriteLine("Student details updated successfully!");
            }
            else
            {
                Console.WriteLine("Student with that ID not found.");
            }

            Console.WriteLine("\nPress Enter to return to the menu.");
            Console.ReadLine();
        }

        // Method to delete a student
        static void DeleteStudent(List<Student> students, string filePath)
        {
            Console.Clear();
            Console.WriteLine("Enter the ID of the student you want to delete:");
            int id = int.Parse(Console.ReadLine() ?? "0");
            var studentToDelete = students.Find(s => s.ID == id);

            if (studentToDelete != null)
            {
                students.Remove(studentToDelete);
                Console.WriteLine("Student deleted successfully!");

                // After deleting the student, save the updated list to the CSV
                SaveStudents(students, filePath);
            }
            else
            {
                Console.WriteLine("Student with that ID not found.");
            }

            Console.WriteLine("\nPress Enter to return to the menu.");
            Console.ReadLine();
        }

        // Method to save student data to the CSV file
        static void SaveStudents(List<Student> students, string filePath)
        {
            try
            {
                Student.SaveToCsv(filePath, students);
                Console.WriteLine("All students have been saved successfully!");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"An error occurred while saving the file: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }
        }
    }
}