// Author: Nalin Jayasuriya
// Sep/21/2025 - Jacksonville FL

namespace TransactionStoreAndRetrievalConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("From Nalin - TransactionStoreAndRetrievalConsole - started");

            var running = true;
            while (running)
            {
                Console.WriteLine("Enter 1 for transaction-input, 2 for transactions-view, 3 for currencies, Q to exit");
                var selection = Console.ReadKey();
                switch (selection.Key.ToString())
                {
                    case "Q":
                        running = false;
                        break;
                    case "1":
                        var entry = new TransactionEntry();
                        entry.Run();
                        break;
                    case "2":
                        var view = new TransactionView();
                        view.Run();
                        break;
                    case "D3":
                        var currencies = new CurrenciesView();
                        currencies.Run();
                        break;
                }

                Console.WriteLine("\r\n");
            }

            Console.WriteLine("From Nalin - TransactionStoreAndRetrievalConsole - completed");

        }
    }
}
