using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;

namespace FirstBankOfSuncoast
{

    class Transaction
    {
        public string TransactionType { get; set; }
        public int ChangeOfBalance { get; set; }
        public string TransactionHistory()
        {
            var transactionHistory = $"{TransactionType} for {ChangeOfBalance}";
            return transactionHistory;
        }
    }
    class Program
    {
        // PROBLEM
        // 
        // Create a console app that allows a user to manage savings and checking banking transactions.
        // The transactions will be saved in a file, using a CSV format to record the data.
        // Example
        // 
        // For instance, if a user deposits 10 to their savings, then withdraws 8 from their savings,
        // then deposits 25 to their checking, they have three transactions to consider. 
        // Compute the checking and saving balance, using the transaction list, when needed. 
        // In this case, their savings balance is 2 and their checking balance is 25.
        // DATA
        // 
        // USER   
        // CheckingAccount
        // AccountBalance
        // SavingsAccount
        // AccountBalance
        // TRANSACTION
        // MONEY
        // 
        // A
        // 
        // Make a new object with properties:
        // CheckingAccount
        // SavingsAccount 
        // Create a MENU
        // Make option to DEPOSIT
        // 
        // 
        // 
        // 
        //         
        static void Main(string[] args)
        {
            var savingsAccountBalance = 0;
            var checkingAccountBalance = 0;

            // Create a list to to track transfers
            var checkingTransactions = new List<Transaction>();


            if (File.Exists("checkingtransactions.csv"))
            {
                var checkingFileReader = new StreamReader("checkingtransactions.csv");

                var checkingCsvReader = new CsvReader(checkingFileReader, CultureInfo.InvariantCulture);

                // checkingCsvReader.Configuration.HasHeaderRecord = false;

                checkingTransactions = checkingCsvReader.GetRecords<Transaction>().ToList();
            }
            foreach (var check in checkingTransactions)
            {
                if (check.TransactionType == "Deposit")
                {
                    checkingAccountBalance += check.ChangeOfBalance;
                }
                else
                {
                    checkingAccountBalance -= check.ChangeOfBalance;
                }
            }

            var savingsTransactions = new List<Transaction>();


            if (File.Exists("savingstransactions.csv"))
            {
                var savingsFileReader = new StreamReader("savingstransactions.csv");

                var savingsCsvReader = new CsvReader(savingsFileReader, CultureInfo.InvariantCulture);

                // savingsCsvReader.Configuration.HasHeaderRecord = false;

                savingsTransactions = savingsCsvReader.GetRecords<Transaction>().ToList();
            }
            foreach (var save in savingsTransactions)
            {
                if (save.TransactionType == "Deposit")
                {
                    savingsAccountBalance += save.ChangeOfBalance;
                }
                else
                {
                    savingsAccountBalance -= save.ChangeOfBalance;
                }
            }




            Console.WriteLine("Welcome to PRESTIGE WORLDWIDE");
            var hasQuitTheApplication = false;
            while (hasQuitTheApplication == false)
            {
                // Show them a menu of options they can do
                Console.WriteLine("Select an option from the Menu below:");
                Console.WriteLine("-------------------------------------");
                Console.WriteLine("BALANCE - View balances of your accounts");
                Console.WriteLine();
                Console.WriteLine("TRANSACTIONS - View transaction history.");
                Console.WriteLine();
                Console.WriteLine("WITHDRAW - Take out money from your accounts");
                Console.WriteLine();
                Console.WriteLine("DEPOSIT - Place money in your accounts");
                Console.WriteLine();
                Console.WriteLine("QUIT - Quit the program!");
                Console.WriteLine();
                Console.Write("Choice: ");
                var choice = Console.ReadLine().ToUpper();
                if (choice == "QUIT")
                {
                    hasQuitTheApplication = true;
                }

                if (choice == "DEPOSIT")
                {
                    Console.WriteLine("Please select which account to deposit into, CHECKING or SAVINGS");
                    var answer = Console.ReadLine().ToUpper();
                    if (answer == "CHECKING")
                    {
                        Console.WriteLine("How much would you like to deposit");
                        var depositAmount = int.Parse(Console.ReadLine());
                        var newTransaction = new Transaction
                        {
                            TransactionType = "Deposit",
                            ChangeOfBalance = depositAmount
                        };
                        var newCheckingAccountBalance = checkingAccountBalance + newTransaction.ChangeOfBalance;
                        checkingAccountBalance = newCheckingAccountBalance;
                        Console.WriteLine(checkingAccountBalance);
                        checkingTransactions.Add(newTransaction);
                    }
                    if (answer == "SAVINGS")
                    {
                        Console.WriteLine("How much would you like to deposit");
                        var depositAmount = int.Parse(Console.ReadLine());
                        var newTransaction = new Transaction
                        {
                            TransactionType = "Deposit",
                            ChangeOfBalance = depositAmount
                        };
                        var newSavingsAccountBalance = savingsAccountBalance + newTransaction.ChangeOfBalance;
                        savingsAccountBalance = newSavingsAccountBalance;
                        Console.WriteLine(savingsAccountBalance);
                        savingsTransactions.Add(newTransaction);
                    }
                }
                if (choice == "WITHDRAW")
                {
                    Console.WriteLine("Please select which account to withdraw from, CHECKING or SAVINGS");
                    var answer = Console.ReadLine().ToUpper();
                    if (answer == "CHECKING")
                    {
                        Console.WriteLine("How much would you like to withdraw?");
                        var withdrawAmount = int.Parse(Console.ReadLine());
                        var newTransaction = new Transaction
                        {
                            TransactionType = "Withdraw",
                            ChangeOfBalance = withdrawAmount
                        };
                        //If/else statement needed
                        if (withdrawAmount < checkingAccountBalance)
                        {
                            var newCheckingAccountBalance = checkingAccountBalance - newTransaction.ChangeOfBalance;
                            checkingAccountBalance = newCheckingAccountBalance;
                            Console.WriteLine(checkingAccountBalance);
                            checkingTransactions.Add(newTransaction);
                        }
                        else
                        {
                            Console.WriteLine("Insufficient funds to perform transaction.");
                        }
                    }
                    if (answer == "SAVINGS")
                    {
                        Console.WriteLine("How much would you like to withdraw?");
                        var withdrawAmount = int.Parse(Console.ReadLine());
                        var newTransaction = new Transaction
                        {
                            TransactionType = "Withdraw",
                            ChangeOfBalance = withdrawAmount
                        };
                        //If/else statement needed
                        if (withdrawAmount < savingsAccountBalance)
                        {
                            var newSavingsAccountBalance = savingsAccountBalance - newTransaction.ChangeOfBalance;
                            savingsAccountBalance = newSavingsAccountBalance;
                            Console.WriteLine(savingsAccountBalance);
                            savingsTransactions.Add(newTransaction);
                        }
                        else
                        {
                            Console.WriteLine("Insufficient funds to perform transaction.");
                        }

                    }

                }
                if (choice == "BALANCE")
                {
                    Console.WriteLine($"The balance of your checking account is {checkingAccountBalance} and your savings account balance is {savingsAccountBalance}");
                }
                if (choice == "TRANSACTIONS")
                {
                    Console.WriteLine("View transaction history of CHECKING or SAVINGS?");
                    var answer = Console.ReadLine().ToUpper();
                    if (answer == "CHECKING")
                    {
                        foreach (var transaction in checkingTransactions)
                        {
                            Console.WriteLine(transaction.TransactionHistory());
                        }
                    }
                    if (answer == "SAVINGS")
                    {
                        foreach (var transaction in savingsTransactions)
                        {
                            Console.WriteLine(transaction.TransactionHistory());
                        }
                    }
                }


                Console.WriteLine(" GOODBYE ");
            }
            var checkingFileWriter = new StreamWriter("checkingtransactions.csv");
            var checkingCsvWriter = new CsvWriter(checkingFileWriter, CultureInfo.InvariantCulture);
            checkingCsvWriter.WriteRecords(checkingTransactions);
            checkingFileWriter.Close();

            var savingsFileWriter = new StreamWriter("savingstransactions.csv");
            var savingsCsvWriter = new CsvWriter(savingsFileWriter, CultureInfo.InvariantCulture);
            savingsCsvWriter.WriteRecords(savingsTransactions);
            savingsFileWriter.Close();
        }


    }
}