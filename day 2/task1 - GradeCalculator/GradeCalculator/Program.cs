// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Generic;

class GradeCalculator
{
    static void Main()
    {
        Console.WriteLine("-----STUDENT GRADE CALCULATOR-----");

        Console.WriteLine("Enter your name: ");
        string name = Console.ReadLine();

        Console.WriteLine("Enter the number of subjects you have taken");
        int subjects = int.Parse(Console.ReadLine());

        Dictionary<string, int> GradedSubjects = new Dictionary<string, int>();


        for (int i = 0; i < subjects; i++)
        {
            Console.WriteLine($"Enter the name of subject {i + 1}:");
            string subject = Console.ReadLine();

            Console.WriteLine($"Enter the grade obtained for subject {subject}");
            int grade = int.Parse(Console.ReadLine());

            if (grade < 0 || grade > 100)
            {
                Console.WriteLine("Grade should be between 0 and 100");
                i--;
                continue;
            }

            GradedSubjects.Add(subject, grade);

        }

        double AvgGrade = CalcAvgGrade(GradedSubjects);

        Console.WriteLine($"Student name: {name}");
        foreach (var subGrade in GradedSubjects)
        {
            Console.WriteLine($"{subGrade.Key}: {subGrade.Value}");

        }

        Console.WriteLine($"Average Grade: {AvgGrade:F2}");

    }

    static double CalcAvgGrade(Dictionary<string, int> grades)
    {
        if (grades.Count == 0)
        {
            return 0;
        }

        int total = 0;
        foreach(var grade in grades.Values)
        {
            total+=grade;
        }

        return (double)total / grades.Count;
    }

}



