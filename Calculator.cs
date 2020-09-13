using System.Linq;

namespace Interest
{
    public class Calculator
    {
        private readonly IInterestRateSource rateSource;

        public Calculator(IInterestRateSource rateSource)
        {
            this.rateSource = rateSource;
        }

        public decimal GetInterestAmount(CreditCard card)
        {
            var rate = rateSource.GetInterestRate(card.Type);
            return card.Balance * rate;
        }

        public decimal GetInterestAmount(Wallet wallet)
        {
            return wallet.Cards
                .Select(c => GetInterestAmount(c))
                .Sum();
        }
        
        public decimal GetInterestAmount(Person person)
        {
            return person.Wallets
                .Select(w => GetInterestAmount(w))
                .Sum();
        }
    }
}