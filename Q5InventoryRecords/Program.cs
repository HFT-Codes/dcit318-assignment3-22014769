using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

// Marker interface for inventory entities
public interface IInventoryEntity
{
    int Id { get; }
}

// Immutable record for inventory items
public record InventoryItem(int Id, string Name, int Quantity, DateTime DateTimeAddDate) : IInventoryEntity;

// Generic logger - saves and loads inventory items
public class InventoryLogger<T> where T : IInventoryEntity
{
    private readonly List<T> _log = new();
    private readonly string _filePath;

    public InventoryLogger(string filePath)
    {
        _filePath = filePath;
    }

    // Add item to log
    public void Add(T item)
    {
        _log.Add(item);
    }

    // Get all items
    public List<T> GetAll()
    {
        return _log;
    }

    // Serialize and save to file
    public void SaveToFile()
    {
        try
        {
            using StreamWriter writer = new(_filePath);
            writer.WriteLine(JsonSerializer.Serialize(_log));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Save error: {ex.Message}");
        }
    }

    // Load and deserialize from file
    public void LoadFromFile()
    {
        try
        {
            if (!File.Exists(_filePath))
            {
                Console.WriteLine($"File not found at {_filePath}.");
                return;
            }

            using StreamReader reader = new(_filePath);
            string json = reader.ReadToEnd();

            if (string.IsNullOrWhiteSpace(json))
            {
                _log.Clear();
                return;
            }

            var items = JsonSerializer.Deserialize<List<T>>(json);
            _log.Clear();
            if (items != null)
            {
                _log.AddRange(items);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Load error: {ex.Message}");
        }
    }
}

// Inventory app - manages inventory lifecycle
public class InventoryApp
{
    private readonly InventoryLogger<InventoryItem> _logger;

    public InventoryApp(string filePath)
    {
        _logger = new InventoryLogger<InventoryItem>(filePath);
    }

    // Add sample inventory items
    public void SeedSampleData()
    {
        _logger.Add(new InventoryItem(1, "Keyboard", 20, DateTime.Now));
        _logger.Add(new InventoryItem(2, "Mouse", 30, DateTime.Now));
        _logger.Add(new InventoryItem(3, "Monitor", 15, DateTime.Now));
        _logger.Add(new InventoryItem(4, "USB Cable", 40, DateTime.Now));
    }

    // Save inventory to disk
    public void SaveData()
    {
        _logger.SaveToFile();
    }

    // Load inventory from disk
    public void LoadData()
    {
        _logger.LoadFromFile();
    }

    // Display all items
    public void PrintAllItems()
    {
        Console.WriteLine("Recovered inventory items:");
        foreach (var item in _logger.GetAll())
        {
            Console.WriteLine($"ID: {item.Id}, Name: {item.Name}, Quantity: {item.Quantity}, Added: {item.DateTimeAddDate}");
        }
    }
}

public class Program
{
    public static void Main()
    {
        Console.WriteLine("QUESTION 5: Inventory Record System\n");

        string filePath = Path.Combine(AppContext.BaseDirectory, "inventory_data.json");

        InventoryApp app = new(filePath);
        app.SeedSampleData();
        app.SaveData();

        InventoryApp newSession = new(filePath);
        newSession.LoadData();
        newSession.PrintAllItems();
    }
}

