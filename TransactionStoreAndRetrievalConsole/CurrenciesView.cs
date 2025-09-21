// Author: Nalin Jayasuriya
// Sep/21/2025 - Jacksonville FL

using NalinTransactionCurrencyAPI;

namespace TransactionStoreAndRetrievalConsole
{
    /// <summary>
    /// Displays supported country currencies
    /// </summary>
    internal class CurrenciesView
    {
        internal void Run()
        {
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
