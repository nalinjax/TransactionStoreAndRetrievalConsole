// Author: Nalin Jayasuriya
// Sep/21/2025 - Jacksonville FL

using NalinLogging;
using NalinTransactionCurrencyAPI;

namespace TransactionStoreAndRetrievalConsole
{
    /// <summary>
    /// Displays supported country currencies
    /// </summary>
    internal class CurrenciesView
    {
        INalinLogger _logger;

        public CurrenciesView(INalinLogger logger)
        {
            _logger = logger;
        }

        internal void Run()
        {
            Console.WriteLine("****Currencies view");

            var provider = new TreasuryCurrencyDataProvider();
            var currencyOperations = new CurrencyOperations(provider);

            var countryCurrencies = currencyOperations.GetAllSupportedCurrencies().Result;

            if (countryCurrencies == null)
            {
                Console.WriteLine("ERROR - Unable to get country currencies");
                return;
            }

            var index = 1;
            foreach (var currency in countryCurrencies)
            {
                Console.WriteLine($"{index++} : {currency.country_currency_desc}");
            }

            Console.WriteLine("Complete.");
        }
    }
}
