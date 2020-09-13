using System;

namespace Interest
{
    public interface IInterestRateSource
    {
        decimal GetInterestRate(CreditCardType type);
    }
    public class InterestRateSource : IInterestRateSource
    {
        public decimal GetInterestRate(CreditCardType type)
        {
            switch (type)
            {
                case CreditCardType.Discover:
                    return .01m;
                case CreditCardType.MasterCard:
                    return .05m;
                case CreditCardType.Visa:
                    return .1m;
                default:
                    throw new Exception("Unknown credit card type");
            }
        }
    }
}