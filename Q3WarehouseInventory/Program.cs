using System;
using System.Collections.Generic;
using System.Linq;

// Marker interface for inventory items
public interface IInventoryItem
{
    int Id { get; }
    string Name { get; }
    int Quantity { get; set; }
}

// Represents electronic product in inventory
public class ElectronicItem : IInventoryItem
{
    public int Id { get; }
    public string Name { get; }
    public int Quantity { get; set; }
    public string Brand { get; }
    public int WarrantyMonths { get; }

    public ElectronicItem(int id, string name, int quantity, string brand, int warrantyMonths)
    {
        Id = id;
        Name = name;
        Quantity = quantity;
        Brand = brand;
        WarrantyMonths = warrantyMonths;
    }

    public override string ToString()
    {
        return $"ElectronicItem: {Name} (ID: {Id}), Quantity: {Quantity}, Brand: {Brand}, Warranty: {WarrantyMonths} months";
    }
}

// Represents grocery product in inventory
public class GroceryItem : IInventoryItem
{
    public int Id { get; }
    public string Name { get; }
    public int Quantity { get; set; }
    public DateTime ExpiryDate { get; }

    public GroceryItem(int id, string name, int quantity, DateTime expiryDate)
    {
        Id = id;
        Name = name;
        Quantity = quantity;
        ExpiryDate = expiryDate;
    }

    public override string ToString()
    {
        return $"GroceryItem: {Name} (ID: {Id}), Quantity: {Quantity}, Expiry: {ExpiryDate:dd/MM/yyyy}";
    }
}

// Thrown when adding duplicate item ID
public class DuplicateItemException : Exception
{
    public DuplicateItemException(string message) : base(message) { }
}

// Thrown when item not found
public class ItemNotFoundException : Exception
{
    public ItemNotFoundException(string message) : base(message) { }
}

// Thrown when quantity is invalid (negative)
public class InvalidQuantityException : Exception
{
    public InvalidQuantityException(string message) : base(message) { }
}

// Generic repository for inventory items
public class InventoryRepository<T> where T : IInventoryItem
{
    private readonly Dictionary<int, T> _items = new();

    // Add item, throw exception if duplicate
    public void AddItem(T item)
    {
        if (_items.ContainsKey(item.Id))
        {
            throw new DuplicateItemException($"Duplicate item ID: {item.Id}");
        }

        _items[item.Id] = item;
    }

    // Retrieve item by ID
    public T GetItemById(int id)
    {
        if (!_items.TryGetValue(id, out T? item))
        {
            throw new ItemNotFoundException($"Item with ID {id} was not found.");
        }

        return item;
    }

    // Remove item by ID
    public void RemoveItem(int id)
    {
        if (!_items.Remove(id))
        {
            throw new ItemNotFoundException($"Cannot remove item with ID {id}; not found.");
        }
    }

    // Get all items
    public List<T> GetAllItems()
    {
        return _items.Values.ToList();
    }

    // Update quantity with validation
    public void UpdateQuantity(int id, int newQuantity)
    {
        if (newQuantity < 0)
        {
            throw new InvalidQuantityException("Quantity cannot be negative.");
        }

        if (!_items.TryGetValue(id, out T? item))
        {
            throw new ItemNotFoundException($"Item with ID {id} was not found.");
        }

        item.Quantity = newQuantity;
    }
}

// Warehouse manager - handles electronics and groceries
public class WareHouseManager
{
    private readonly InventoryRepository<ElectronicItem> _electronics = new();
    private readonly InventoryRepository<GroceryItem> _groceries = new();

    // Add sample data
    public void SeedData()
    {
        _electronics.AddItem(new ElectronicItem(1, "Laptop", 12, "Dell", 24));
        _electronics.AddItem(new ElectronicItem(2, "Smartphone", 20, "Samsung", 12));
        _electronics.AddItem(new ElectronicItem(3, "Monitor", 8, "HP", 18));

        _groceries.AddItem(new GroceryItem(101, "Rice", 50, new DateTime(2026, 12, 15)));
        _groceries.AddItem(new GroceryItem(102, "Milk", 35, new DateTime(2026, 09, 20)));
        _groceries.AddItem(new GroceryItem(103, "Flour", 40, new DateTime(2026, 10, 10)));
    }

    // Print all items in repository
    public void PrintAllItems<T>(InventoryRepository<T> repo) where T : IInventoryItem
    {
        Console.WriteLine($"{typeof(T).Name} inventory:");
        foreach (var item in repo.GetAllItems())
        {
            Console.WriteLine(item);
        }
    }

    // Increase stock with error handling
    public void IncreaseStock<T>(InventoryRepository<T> repo, int id, int quantity) where T : IInventoryItem
    {
        try
        {
            T item = repo.GetItemById(id);
            item.Quantity += quantity;
            Console.WriteLine($"Stock increased for {item.Name}. New quantity: {item.Quantity}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    // Remove item with error handling
    public void RemoveItemById<T>(InventoryRepository<T> repo, int id) where T : IInventoryItem
    {
        try
        {
            repo.RemoveItem(id);
            Console.WriteLine($"Item {id} removed successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    // Demonstrate exception handling
    public void DemonstrateErrors()
    {
        try
        {
            _electronics.AddItem(new ElectronicItem(1, "Duplicate Laptop", 5, "Lenovo", 12));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Duplicate item caught: {ex.Message}");
        }

        try
        {
            _groceries.RemoveItem(999);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Missing item caught: {ex.Message}");
        }

        try
        {
            _electronics.UpdateQuantity(2, -10);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Invalid quantity caught: {ex.Message}");
        }
    }
}

public class Program
{
    public static void Main()
    {
        Console.WriteLine("QUESTION 3: Warehouse Inventory Management System\n");

        WareHouseManager manager = new();
        manager.SeedData();
        manager.PrintAllItems(new InventoryRepository<ElectronicItem>());
        manager.PrintAllItems(new InventoryRepository<GroceryItem>());

        // Populate repositories for printing actual items
        InventoryRepository<ElectronicItem> electronics = new();
        InventoryRepository<GroceryItem> groceries = new();

        electronics.AddItem(new ElectronicItem(1, "Laptop", 12, "Dell", 24));
        electronics.AddItem(new ElectronicItem(2, "Smartphone", 20, "Samsung", 12));

        groceries.AddItem(new GroceryItem(101, "Rice", 50, new DateTime(2026, 12, 15)));
        groceries.AddItem(new GroceryItem(102, "Milk", 35, new DateTime(2026, 09, 20)));

        Console.WriteLine("\nGrocery items:");
        foreach (var item in groceries.GetAllItems())
        {
            Console.WriteLine(item);
        }

        Console.WriteLine("\nElectronic items:");
        foreach (var item in electronics.GetAllItems())
        {
            Console.WriteLine(item);
        }

        manager.DemonstrateErrors();
    }
}

