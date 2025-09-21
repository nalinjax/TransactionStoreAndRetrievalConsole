// Author: Nalin Jayasuriya
// Sep/21/2025 - Jacksonville FL

using NalinLogging;
using NalinTransactionCurrencyAPI;
using NalinTransactionPersistence;

namespace TransactionStoreAndRetrievalConsole
{
    /// <summary>
    /// Transactions view that includes currency conversion
    /// </summary>
    internal class TransactionsView
    {
        ICurrencyOperations _currencyOperations;
        ITransactionPersistence _transactionPersistence;
        INalinLogger _logger;

        const int BackMonths = 6;

        public TransactionsView(ICurrencyOperations currencyOperations, ITransactionPersistence transactionPersistence, INalinLogger logger)
        {
            _transactionPersistence = transactionPersistence;
            _currencyOperations = currencyOperations;
            _logger = logger;
        }


        internal void Run()
        {
            Console.WriteLine("****Transactions view");

            Console.WriteLine("Enter country-currency (e.g. Canada-Dollar):");
            var countryCurrency = Console.ReadLine();

            // validation

            if (string.IsNullOrEmpty(countryCurrency))
            {
                Console.WriteLine("Error: Currency is required");
                return;
            }

            if (countryCurrency.Length > 50)
            {
                Console.WriteLine("Error: Currency length is invalid");
                return;
            }

            var countryCurrencies = _currencyOperations.GetAllSupportedCurrencies().Result;
            var knownCurrency = countryCurrencies.FirstOrDefault(c => c.country_currency_desc == countryCurrency);
            if (knownCurrency == null)
            {
                Console.WriteLine("Error: Currency is unsupported");
                return;
            }

            DisplayTransactionsInCurrency(countryCurrency);

            Console.WriteLine("****Transactions view completed.");
        }

        private void DisplayTransactionsInCurrency(string countryCurrency)
        {
            var transactions = _transactionPersistence.RetrieveAll();
            if (transactions == null)
            {
                Console.WriteLine("Error: Unable to retrieve transactions");
                return;
            }

            int recordNumber = 1;
            foreach (var transaction in transactions)
            {
                var id = transaction.ID;
                var description = transaction.Description;
                var usdAmount = transaction.Amount;
                var date = transaction.Date;
                var convertedRate = "";
                var convertedAmount = "?";
                var message = "";
                var rate = _currencyOperations.GetCurrencyRateForDate(countryCurrency, transaction.Date, BackMonths).Result;
                if (rate == null)
                {
                    message = "Unable to get rate for date";
                }
                else
                {
                    convertedRate =  rate.ToString();                   
                    var amountForRate = Math.Round(Decimal.Parse(convertedRate) * usdAmount, 2);
                    convertedAmount = amountForRate.ToString();
                }

                Console.WriteLine($"Record {recordNumber}:");
                Console.WriteLine($"ID: {id}");
                Console.WriteLine($"Date: {date.ToString("MM/dd/yyyy")}");
                Console.WriteLine($"Description: {description}");
                Console.WriteLine($"USD Amount: {usdAmount.ToString("0.00")}");
                if (message == null) 
                { 
                    Console.WriteLine(message);
                }
                else
                {
                    Console.WriteLine($"Rate: {convertedRate}:");
                    Console.WriteLine($"Converted Amount: {convertedAmount}:");
                }

                Console.WriteLine();

            } // for
        }
    }
}
