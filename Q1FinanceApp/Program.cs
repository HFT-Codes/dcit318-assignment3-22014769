using System;
using System.Collections.Generic;

// Immutable transaction record
public record Transaction(int Id, DateTime Date, decimal Amount, string Category);

// Interface for processing different transaction types
public interface ITransactionProcessor
{
    void Process(Transaction transaction);
}

// Processes bank transfer transactions
public class BankTransferProcessor : ITransactionProcessor
{
    public void Process(Transaction transaction)
    {
        Console.WriteLine($"Bank Transfer processed: {transaction.Amount:C} for {transaction.Category}");
    }
}

// Processes mobile money transactions
public class MobileMoneyProcessor : ITransactionProcessor
{
    public void Process(Transaction transaction)
    {
        Console.WriteLine($"Mobile Money processed: {transaction.Amount:C} for {transaction.Category}");
    }
}

// Processes cryptocurrency wallet transactions
public class CryptoWalletProcessor : ITransactionProcessor
{
    public void Process(Transaction transaction)
    {
        Console.WriteLine($"Crypto Wallet processed: {transaction.Amount:C} for {transaction.Category}");
    }
}

// Base account class
public class Account
{
    public string AccountNumber { get; }
    protected decimal Balance { get; set; }

    public Account(string accountNumber, decimal initialBalance)
    {
        AccountNumber = accountNumber;
        Balance = initialBalance;
    }

    // Apply transaction to balance
    public virtual void ApplyTransaction(Transaction transaction)
    {
        Balance -= transaction.Amount;
        Console.WriteLine($"Account {AccountNumber} new balance: {Balance:C}");
    }
}

// Sealed account with balance validation
public sealed class SavingsAccount : Account
{
    public SavingsAccount(string accountNumber, decimal initialBalance)
        : base(accountNumber, initialBalance)
    {
    }

    // Verify sufficient funds before transaction
    public override void ApplyTransaction(Transaction transaction)
    {
        if (transaction.Amount > Balance)
        {
            Console.WriteLine("Insufficient funds");
            return;
        }

        Balance -= transaction.Amount;
        Console.WriteLine($"Updated balance: {Balance:C}");
    }
}

// Finance app - processes transactions and manages accounts
public class FinanceApp
{
    private readonly List<Transaction> _transactions = new();

    // Execute app workflow
    public void Run()
    {
        SavingsAccount account = new("SA-1001", 1000m);

        Transaction t1 = new(1, DateTime.Today, 150.00m, "Groceries");
        Transaction t2 = new(2, DateTime.Today, 200.00m, "Utilities");
        Transaction t3 = new(3, DateTime.Today, 300.00m, "Entertainment");

        ITransactionProcessor mobileMoney = new MobileMoneyProcessor();
        ITransactionProcessor bankTransfer = new BankTransferProcessor();
        ITransactionProcessor cryptoWallet = new CryptoWalletProcessor();

        mobileMoney.Process(t1);
        bankTransfer.Process(t2);
        cryptoWallet.Process(t3);

        account.ApplyTransaction(t1);
        account.ApplyTransaction(t2);
        account.ApplyTransaction(t3);

        _transactions.Add(t1);
        _transactions.Add(t2);
        _transactions.Add(t3);

        Console.WriteLine("All transactions recorded.");
    }
}

public class Program
{
    public static void Main()
    {
        Console.WriteLine("QUESTION 1: Finance Management System\n");
        FinanceApp app = new();
        app.Run();
    }
}

