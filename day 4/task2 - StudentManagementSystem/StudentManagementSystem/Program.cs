using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace StudentManagementSystem
{
    public class Student
    { 
        public string? Name { get; set; }
        public int Age { get; set; }
        public readonly int RollNumber;
        public int Grade { get; set; }

        public Student() { }

        public Student(string name, int age, int grade, int rollNumber)
        {
            this.Name = name;
            this.Age = age;
            this.Grade = grade;
            this.RollNumber = rollNumber;
        }
    }

    // Generic class for Student objects
    public class StudentList<T>
    {
        List<Student> students = new List<Student>();
        public void AddStudent(Student student)
        {
            students.Add(student);
        }

        public void RemoveStudent(Student student)
        {
            students.Remove(student);
        }

        public string SerializeStudents()
        {
            return JsonSerializer.Serialize(students);
        }

        public static StudentList<T> Deserialize(string json)
        {
            var deserializeStudents = JsonSerializer.Deserialize<List<Student>>(json);
            StudentList<T> studentList = new StudentList<T>();
            foreach (var student in deserializeStudents)
            {
                studentList.AddStudent(new Student(student.Name, student.Age, student.RollNumber, student.Grade));
            }
            return studentList;
        }


        public void DisplayStudents()
        {
            foreach (var student in students)
            {
                Console.WriteLine($"The information concerning the Student: {student.Name} is as follows");
                Console.WriteLine($"The name of the student is: {student.Name}");
                Console.WriteLine($"The Age of the student is: {student.Age}");
                Console.WriteLine($"The student's overall grade is: {student.Grade}");
                Console.WriteLine($"The student is seated on roll number {student.RollNumber}");

                Console.WriteLine("");
            }

        }

        // LINQ for search functionality
        public IEnumerable<Student> SearchStudentByName(string name)
        {
            return students.Where(s => s.Name == name);
        }

        public IEnumerable<Student> SearchStudentByID(int id)
        {
            return students.Where(s => s.RollNumber == id);
        }

    }

    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Student Management System");
            Console.WriteLine("");

            StudentList<Student> studentList = new StudentList<Student>();
            Student student1 = new Student("Mark", 15, 12, 1);
            Student student2 = new Student("Angel", 16, 13, 2);
            Student student3 = new Student("Dan", 14, 11, 3);
            Student student4 = new Student("Ben", 13, 10, 4);

            // Add students
            studentList.AddStudent(student1);
            studentList.AddStudent(student2);
            studentList.AddStudent(student3);
            studentList.AddStudent(student4);

            // Remove student
            studentList.RemoveStudent(student4);

            // Display students
            studentList.DisplayStudents();

            // Searching student by name
            var studentsByName = studentList.SearchStudentByName("Mark");
            Console.WriteLine("Student(s) found by name:");

            if (!studentsByName.Any())
            {
                Console.WriteLine("The specified student's name is not found in the database");
            }
            else
            {
                foreach (var student in studentsByName)
                {

                    Console.WriteLine($"Name: {student.Name}, Age: {student.Age}, Grade: {student.Grade}, RollNumber: {student.RollNumber}");
                }

            }
               


            // Searching student by ID - Example when student ID is not found
            var studentsByID = studentList.SearchStudentByID(37);
            Console.WriteLine("\nStudent(s) found by ID:");
            if (!studentsByID.Any())
            {
                Console.WriteLine("The specified student's ID is not found in the database");
            }
            else
            {
                foreach (var student in studentsByID)
                {
                    Console.WriteLine($"Name: {student.Name}, Age: {student.Age}, Grade: {student.Grade}, RollNumber: {student.RollNumber}");
                }
            }


            Console.WriteLine("");

            // Serialization
            string serializedStudents = studentList.SerializeStudents();
            Console.WriteLine("Serialized students:");
            Console.WriteLine(serializedStudents);

            Console.WriteLine("");

            // Deserialization
            StudentList<Student> deserializedStudentList = StudentList<Student>.Deserialize(serializedStudents);
            Console.WriteLine("Deserialized students:");
            deserializedStudentList.DisplayStudents();
        }
    }
}
