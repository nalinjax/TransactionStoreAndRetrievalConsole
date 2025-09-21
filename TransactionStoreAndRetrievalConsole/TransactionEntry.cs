// Author: Nalin Jayasuriya
// Sep/21/2025 - Jacksonville FL

using NalinLogging;
using NalinTransactionCommon;
using NalinTransactionPersistence;
using System.Globalization;

namespace TransactionStoreAndRetrievalConsole
{
    /// <summary>
    /// Transaction entry
    /// </summary>
    internal class TransactionEntry
    {
        INalinLogger _logger;

        public TransactionEntry(INalinLogger logger)
        {
            _logger = logger;
        }

        internal void Run()
        {
            Console.WriteLine("****Transaction entry");

            Console.WriteLine("Entry description (max 50 chars):");
            var description = Console.ReadLine();

            if (string.IsNullOrEmpty(description))
            {
                Console.WriteLine("Error: Description is required");
                return;
            }

            if (description.Length > 50)
            {
                Console.WriteLine("Error: Description length must be 50 chars or less");
                return;
            }

            Console.WriteLine("Entry date (mm/dd/yyyy):");
            var dateInput = Console.ReadLine();

            if (string.IsNullOrEmpty(dateInput))
            {
                Console.WriteLine("Error: Date is required");
                return;
            }

            if (!DateTime.TryParseExact(dateInput, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime transactionDate))
            {
                Console.WriteLine("Error: date is not valid in mm/dd/yyyy format");
                return;
            }

            Console.WriteLine("Entry amount (> 0):");
            var amountInput = Console.ReadLine();

            // validate
            if (string.IsNullOrEmpty(amountInput))
            {
                Console.WriteLine("Error: Amount is required");
                return;
            }

            if (!Decimal.TryParse(amountInput, out decimal amount))
            {
                Console.WriteLine("Error: Amount is not a valid decimal number");
                return;
            }

            if (amount <= 0)
            {
                Console.WriteLine("Error: Amount must be > 0");
                return;
            }

            AddTransaction(description, transactionDate, amount);

        }

        /// <summary>
        /// Add transaction
        /// </summary>
        private void AddTransaction(string description, DateTime transactionDate, decimal amount)
        {
            Console.WriteLine("Adding transaction");

            string currentDirectory = Directory.GetCurrentDirectory();

            var persistenceProvider = new FileDataPersistence(currentDirectory);
            var transactionPersistenceProvider = new TransactionPersistence(persistenceProvider, _logger);

            var transactionData = new TransactionData()
            {
                Amount = amount,
                Date = transactionDate,
                Description = description,
                ID = Guid.NewGuid().ToString()
            };

            var isSuccessful = transactionPersistenceProvider.PersistSingle(transactionData);
            if (isSuccessful)
            {
                Console.WriteLine("Adding transaction completed successfully.");
            }
            else
            {
                Console.WriteLine("ERRORL Adding transaction completed failed.");
            }
        }
    }
}
