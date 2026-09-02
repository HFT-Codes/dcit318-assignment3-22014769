using System;
using System.Collections.Generic;
using System.Linq;

// Represents a patient record
public class Patient
{
    public int Id { get; }
    public string Name { get; }
    public int Age { get; }
    public string Gender { get; }

    public Patient(int id, string name, int age, string gender)
    {
        Id = id;
        Name = name;
        Age = age;
        Gender = gender;
    }

    public override string ToString()
    {
        return $"ID: {Id}, Name: {Name}, Age: {Age}, Gender: {Gender}";
    }
}

// Represents a prescription record
public class Prescription
{
    public int Id { get; }
    public int PatientId { get; }
    public string MedicationName { get; }
    public DateTime DateTimeIssued { get; }

    public Prescription(int id, int patientId, string medicationName, DateTime dateTimeIssued)
    {
        Id = id;
        PatientId = patientId;
        MedicationName = medicationName;
        DateTimeIssued = dateTimeIssued;
    }

    public override string ToString()
    {
        return $"Prescription ID: {Id}, Patient ID: {PatientId}, Medication: {MedicationName}, Issued: {DateTimeIssued:dd/MM/yyyy}";
    }
}

// Generic repository for storing and retrieving entities
public class Repository<T>
{
    private readonly List<T> items = new();

    public void Add(T item)
    {
        items.Add(item);
    }

    // Retrieve all items
    public List<T> GetAll()
    {
        return items;
    }

    // Find item by predicate
    public T? GetById(Func<T, bool> predicate)
    {
        return items.FirstOrDefault(predicate);
    }

    // Remove item by predicate
    public bool Remove(Func<T, bool> predicate)
    {
        T? item = items.FirstOrDefault(predicate);
        if (item == null) return false;
        return items.Remove(item);
    }
}

// Health system app - manages patients and prescriptions
public class HealthSystemApp
{
    private readonly Repository<Patient> _patientRepo = new();
    private readonly Repository<Prescription> _prescriptionRepo = new();
    private readonly Dictionary<int, List<Prescription>> _prescriptionMap = new();

    // Add sample patients and prescriptions
    public void SeedData()
    {
        _patientRepo.Add(new Patient(1, "Alice Johnson", 30, "Female"));
        _patientRepo.Add(new Patient(2, "Daniel Smith", 45, "Male"));
        _patientRepo.Add(new Patient(3, "Mary Brown", 28, "Female"));

        _prescriptionRepo.Add(new Prescription(101, 1, "Paracetamol", new DateTime(2026, 1, 5)));
        _prescriptionRepo.Add(new Prescription(102, 1, "Amoxicillin", new DateTime(2026, 1, 10)));
        _prescriptionRepo.Add(new Prescription(103, 2, "Ibuprofen", new DateTime(2026, 2, 5)));
        _prescriptionRepo.Add(new Prescription(104, 3, "Vitamin C", new DateTime(2026, 2, 20)));
        _prescriptionRepo.Add(new Prescription(105, 3, "Cough Syrup", new DateTime(2026, 3, 1)));
    }

    // Group prescriptions by patient ID
    public void BuildPrescriptionMap()
    {
        foreach (var prescription in _prescriptionRepo.GetAll())
        {
            if (!_prescriptionMap.ContainsKey(prescription.PatientId))
            {
                _prescriptionMap[prescription.PatientId] = new List<Prescription>();
            }

            _prescriptionMap[prescription.PatientId].Add(prescription);
        }
    }

    // Display all patients
    public void PrintAllPatients()
    {
        Console.WriteLine("Patients:");
        foreach (var patient in _patientRepo.GetAll())
        {
            Console.WriteLine(patient);
        }
    }

    // Display prescriptions for specific patient
    public void PrintPrescriptionsForPatient(int id)
    {
        if (_prescriptionMap.ContainsKey(id))
        {
            Console.WriteLine($"\nPrescriptions for Patient {id}:");
            foreach (var prescription in _prescriptionMap[id])
            {
                Console.WriteLine(prescription);
            }
        }
        else
        {
            Console.WriteLine("No prescriptions found for this patient.");
        }
    }
}

public class Program
{
    public static void Main()
    {
        Console.WriteLine("QUESTION 2: Healthcare Management System\n");

        HealthSystemApp app = new();
        app.SeedData();
        app.BuildPrescriptionMap();
        app.PrintAllPatients();
        app.PrintPrescriptionsForPatient(1);
    }
}

