// Author: Nalin Jayasuriya
// Sep/21/2025 - Jacksonville FL

using NalinLogging;
using NalinTransactionCurrencyAPI;
using NalinTransactionPersistence;

namespace TransactionStoreAndRetrievalConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("From Nalin - TransactionStoreAndRetrievalConsole - started");

            // setup

            var logger = new NalinLogger();

            var currencyProvider = new TreasuryCurrencyDataProvider();
            var currencyOperations = new CurrencyOperations(currencyProvider);

            string currentDirectory = Directory.GetCurrentDirectory() + "..\\..\\..\\..\\..\\Data";
            var persistenceProvider = new FileDataPersistence(currentDirectory);
            var transactionPersistenceProvider = new TransactionPersistence(persistenceProvider, logger);


            var running = true;
            while (running)
            {
                Console.WriteLine("Enter 1 for transaction-input, 2 for transactions-view, 3 to view all currencies, Q to exit: Then hit ENTER.");
                var selection = Console.ReadLine();
                switch (selection)
                {
                    case "Q":
                        running = false;
                        break;
                    case "1":
                        var entry = new TransactionEntry(transactionPersistenceProvider, logger);
                        entry.Run();
                        break;
                    case "2":
                        var view = new TransactionsView(currencyOperations, transactionPersistenceProvider, logger);
                        view.Run();
                        break;
                    case "3":
                        var currencies = new CurrenciesView(currencyOperations, logger);
                        currencies.Run();
                        break;
                }

                Console.WriteLine("\r\n");
            }

            Console.WriteLine("From Nalin - TransactionStoreAndRetrievalConsole - completed");

        }
    }
}
