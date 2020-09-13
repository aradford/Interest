using NUnit.Framework;
using Moq;

namespace Interest
{
    [TestFixture]
    public class CalculatorTests
    {
        Calculator testObject;
        Mock<IInterestRateSource> rateSource;

        [SetUp]
        public void Setup()
        {
            rateSource = new Mock<IInterestRateSource>();
            testObject = new Calculator(rateSource.Object);
        }

        [Test]
        public void GetInterestAmount_ForCard()
        {
            SetupInterestRate(CreditCardType.MasterCard, .2m);
            var card = new CreditCard
            {
                Type = CreditCardType.MasterCard,
                Balance = 300.10m
            };

            var result = testObject.GetInterestAmount(card);

            Assert.That(result, Is.EqualTo(60.02m));
        }

        [Test]
        public void GetInterestAmount_ForWallet()
        {
            SetupInterestRate(CreditCardType.MasterCard, .05m);
            SetupInterestRate(CreditCardType.Discover, .1m);

            var wallet = new Wallet
            {
                Cards = new[] {
                    new CreditCard{Type = CreditCardType.MasterCard, Balance = 50},
                    new CreditCard{Type = CreditCardType.Discover, Balance = 75}
                }
            };

            var result = testObject.GetInterestAmount(wallet);

            Assert.That(result, Is.EqualTo(10m));
        }

        [Test]
        public void GetInterestAmount_ForPerson()
        {
            SetupInterestRate(CreditCardType.Visa, .1m);
            SetupInterestRate(CreditCardType.MasterCard, .2m);
            SetupInterestRate(CreditCardType.Discover, .3m);

            var person = new Person
            {
                Wallets = new[]{
                    new Wallet
                    {
                        Cards = new[] {
                            new CreditCard{Type = CreditCardType.MasterCard, Balance = 1},
                            new CreditCard{Type = CreditCardType.Discover, Balance = 2}
                        }
                    },
                    new Wallet
                    {
                        Cards = new[] {
                            new CreditCard{Type = CreditCardType.MasterCard, Balance = 3},
                            new CreditCard{Type = CreditCardType.Visa, Balance = 4}
                        }
                    }
                }
            };

            var result = testObject.GetInterestAmount(person);

            Assert.That(result, Is.EqualTo(1.8m));
        }

        private void SetupInterestRate(CreditCardType type, decimal rate)
        {
            rateSource.Setup(s => s.GetInterestRate(type))
                            .Returns(rate);
        }
    }
}