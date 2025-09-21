// Author: Nalin Jayasuriya
// Sep/21/2025 - Jacksonville FL

using NalinLogging;

namespace TransactionStoreAndRetrievalConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("From Nalin - TransactionStoreAndRetrievalConsole - started");

            var logger = new NalinLogger();

            var running = true;
            while (running)
            {
                Console.WriteLine("Enter 1 for transaction-input, 2 for transactions-view, 3 for currencies, Q to exit: Then hit ENTER.");
                var selection = Console.ReadLine();
                switch (selection)
                {
                    case "Q":
                        running = false;
                        break;
                    case "1":
                        var entry = new TransactionEntry(logger);
                        entry.Run();
                        break;
                    case "2":
                        var view = new TransactionView(logger);
                        view.Run();
                        break;
                    case "3":
                        var currencies = new CurrenciesView(logger);
                        currencies.Run();
                        break;
                }

                Console.WriteLine("\r\n");
            }

            Console.WriteLine("From Nalin - TransactionStoreAndRetrievalConsole - completed");

        }
    }
}
