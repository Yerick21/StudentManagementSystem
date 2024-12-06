# Student Management System

## Overview

The **Student Management System** is a C# console application designed to manage student records for a school or class. It allows users to add, edit, delete, and view student details, including personal information, major, enrollment status, and courses with grades. Additionally, the program supports saving and loading student data from a CSV file for persistent storage.

## Features

1. **Add New Students**: Users can input information such as student ID, first name, last name, date of birth, major, courses, and enrollment status.
   
2. **Edit Existing Students**: Users can edit a student's details by providing their student ID.

3. **Delete Students**: Users can delete a student from the list and the CSV file by providing their student ID.

4. **View All Students**: Users can view the list of all students, including their courses and average grades.

5. **Save and Load Data**: The program can load student data from a CSV file on startup and save the data back to the file after modifications (adding, editing, deleting students).

6. **CSV Format**: The program uses CSV (Comma-Separated Values) format to save and load student data. This allows for easy integration with other systems or for data transfer.

## Requirements

- **.NET Core**: The program is built using the .NET Core framework. You need to have **.NET Core** installed to run this application.
  
- **CSV File**: The program reads from and writes to a CSV file, where student data is stored. The default file is `students.csv`.

## How to Run the Program

1. **Clone the Repository** (or copy the code to your local machine):
   - Open a terminal or command prompt.
   - Clone the repository or navigate to the project folder where your files are located.

2. **Open a Terminal / Command Prompt**:
   - Open your terminal or command prompt and navigate to the folder containing the `StudentManagement.csproj` file.

3. **Restore Dependencies** (if necessary):
   - Run the following command to restore any dependencies:
    dotnet restore

   4. **Run the Program**:
- To run the program, use the following command:
    dotnet run

    ### CSV Column Descriptions:
- **ID**: The unique ID of the student.
- **First Name**: The student's first name.
- **Last Name**: The student's last name.
- **DOB**: The student's date of birth in `yyyy-mm-dd` format.
- **Major**: The student's major or course of study.
- **Classes**: A semicolon-separated list of courses and grades in the format `ClassName:Grade`.
- **Is Enrolled**: A boolean value indicating whether the student is currently enrolled (`True` or `False`).
- **Average Grade**: The student's average grade calculated from their courses.

## Troubleshooting

- If the program fails to load the CSV file, ensure that the `students.csv` file exists in the same directory as the program, or specify the correct path in the program code.
- If the CSV file has incorrect formatting (e.g., missing or extra commas), the program may throw an error when trying to load or parse the file.

## Future Enhancements

- Adding the ability to sort students by different criteria (e.g., by name, by average grade).
- Implementing a search function to search students by ID, name, or major.
- Implementing better data validation to handle edge cases more gracefully.

---

### **Sample CSV File:**

Create a file named `students.csv` and place it in the same directory as your program. Here's a sample content for the file:

```csv
ID,First Name,Last Name,DOB,Major,Classes,Is Enrolled,Average Grade
1,John,Doe,2000-01-01,Computer Science,"Math:90;History:85",True,87.5
2,Jane,Smith,2001-02-15,Engineering,"Math:75;Physics:80",True,77.5
3,David,Johnson,1999-11-30,Biology,"Biology:80;Chemistry:70",True,75
4,Emma,Wilson,2000-06-15,Mathematics,"Calculus:95;Algebra:90",True,92.5
5,Oliver,Miller,2001-02-20,Physics,"Physics:85;Astronomy:88",True,86.5