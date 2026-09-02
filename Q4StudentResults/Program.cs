using System;
using System.Collections.Generic;
using System.IO;

// Represents a student record
public class Student
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public int Score { get; set; }

    // Calculate grade based on score
    public string GetGrade()
    {
        if (Score >= 80 && Score <= 100) return "A";
        if (Score >= 70 && Score <= 79) return "B";
        if (Score >= 60 && Score <= 69) return "C";
        if (Score >= 50 && Score <= 59) return "D";
        return "F";
    }
}

// Thrown when score format is invalid
public class InvalidScoreFormatException : Exception
{
    public InvalidScoreFormatException(string message) : base(message) { }
}

// Thrown when required field is missing
public class MissingFieldException : Exception
{
    public MissingFieldException(string message) : base(message) { }
}

// Processor for reading and writing student results
public class StudentResultProcessor
{
    // Read students from file with validation
    public List<Student> ReadStudentsFromFile(string inputFilePath)
    {
        List<Student> students = new();

        using StreamReader reader = new(inputFilePath);
        string? line;

        while ((line = reader.ReadLine()) != null)
        {
            if (string.IsNullOrWhiteSpace(line)) continue;

            string[] parts = line.Split(',');

            if (parts.Length != 3)
            {
                throw new MissingFieldException($"Incomplete record: '{line}'");
            }

            string idText = parts[0].Trim();
            string name = parts[1].Trim();
            string scoreText = parts[2].Trim();

            if (string.IsNullOrWhiteSpace(idText) || string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(scoreText))
            {
                throw new MissingFieldException($"Missing field in record: '{line}'");
            }

            if (!int.TryParse(idText, out int id))
            {
                throw new InvalidScoreFormatException($"Invalid student ID in record: '{line}'");
            }

            if (!int.TryParse(scoreText, out int score))
            {
                throw new InvalidScoreFormatException($"Invalid score format in record: '{line}'");
            }

            students.Add(new Student { Id = id, FullName = name, Score = score });
        }

        return students;
    }

    // Write formatted report to file
    public void WriteReportToFile(List<Student> students, string outputFilePath)
    {
        using StreamWriter writer = new(outputFilePath);

        foreach (var student in students)
        {
            writer.WriteLine($"{student.FullName} (ID: {student.Id}): Score = {student.Score}, Grade = {student.GetGrade()}");
        }
    }
}

public class Program
{
    public static void Main()
    {
        Console.WriteLine("QUESTION 4: Student Result Processing\n");

        string inputFile = Path.Combine(AppContext.BaseDirectory, "students.txt");
        string outputFile = Path.Combine(AppContext.BaseDirectory, "student_report.txt");

        File.WriteAllLines(inputFile,
        [
            "101,Alice Smith,84",
            "102,Bob Brown,72",
            "103,Carol White,58",
            "104,David Green,95",
            "105,Emma Stone,abc"
        ]);

        StudentResultProcessor processor = new();

        try
        {
            List<Student> students = processor.ReadStudentsFromFile(inputFile);
            processor.WriteReportToFile(students, outputFile);

            Console.WriteLine("Student report saved successfully.");
            Console.WriteLine(File.ReadAllText(outputFile));
        }
        catch (FileNotFoundException ex)
        {
            Console.WriteLine($"File not found: {ex.Message}");
        }
        catch (InvalidScoreFormatException ex)
        {
            Console.WriteLine($"Invalid score format: {ex.Message}");
        }
        catch (MissingFieldException ex)
        {
            Console.WriteLine($"Missing field: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}

