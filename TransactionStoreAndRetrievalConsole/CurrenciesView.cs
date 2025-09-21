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
        ICurrencyOperations _currencyOperations;

        INalinLogger _logger;

        public CurrenciesView(ICurrencyOperations currencyOperations, INalinLogger logger)
        {
            _currencyOperations = currencyOperations;
            _logger = logger;
        }

        internal void Run()
        {
            Console.WriteLine("****Currencies view");

            var countryCurrencies = _currencyOperations.GetAllSupportedCurrencies().Result;

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
