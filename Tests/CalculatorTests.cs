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
        public void GetInterestAmount_ForCard_ReturnsAmount()
        {
            rateSource.Setup(s => s.GetInterestRate(It.IsAny<CreditCardType>()))
                .Returns(.2m);
            var card = new CreditCard
            {
                Type = CreditCardType.MasterCard,
                Balance = 300.10m
            };

            var result = testObject.GetInterestAmount(card);

            Assert.That(result, Is.EqualTo(60.02m));
            rateSource.Verify(s => s.GetInterestRate(CreditCardType.MasterCard));
        }
    }
}