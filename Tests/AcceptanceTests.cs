using NUnit.Framework;

namespace Interest
{
    [TestFixture]
    public class AcceptanceTests
    {
        Calculator testObject;

        [SetUp]
        public void Setup()
        {
            testObject = new Calculator(new InterestRateSource());
        }

        [Test]
        public void TestCase1()
        {
            var person = new Person
            {
                Wallets = new[] {
                    new Wallet{
                        Cards=new []{
                            new CreditCard{Type=CreditCardType.Visa, Balance=100},
                            new CreditCard{Type=CreditCardType.MasterCard, Balance=100},
                            new CreditCard{Type=CreditCardType.Discover, Balance=100}
                        }
                    }
                }
            };

            Assert.That(testObject.GetInterestAmount(person), Is.EqualTo(16));
            Assert.That(testObject.GetInterestAmount(person.Wallets[0].Cards[0]), Is.EqualTo(10));
            Assert.That(testObject.GetInterestAmount(person.Wallets[0].Cards[1]), Is.EqualTo(5));
            Assert.That(testObject.GetInterestAmount(person.Wallets[0].Cards[2]), Is.EqualTo(1));
        }

        [Test]
        public void TestCase2()
        {
            var person = new Person
            {
                Wallets = new[] {
                    new Wallet{
                        Cards=new []{
                            new CreditCard{Type=CreditCardType.Visa, Balance=100},
                            new CreditCard{Type=CreditCardType.Discover, Balance=100}
                        }
                    },
                    new Wallet{
                        Cards=new []{
                            new CreditCard{Type=CreditCardType.MasterCard, Balance=100},
                        }
                    }
                }
            };

            Assert.That(testObject.GetInterestAmount(person), Is.EqualTo(16));
            Assert.That(testObject.GetInterestAmount(person.Wallets[0]), Is.EqualTo(11));
            Assert.That(testObject.GetInterestAmount(person.Wallets[1]), Is.EqualTo(5));
        }

        [Test]
        public void TestCase3()
        {
            var person1 = new Person
            {
                Wallets = new[] {
                    new Wallet{
                        Cards=new []{
                            new CreditCard{Type=CreditCardType.MasterCard, Balance=100},
                            new CreditCard{Type=CreditCardType.Visa, Balance=100},
                            new CreditCard{Type=CreditCardType.Discover, Balance=100}
                        }
                    }
                }
            };

            var person2 = new Person
            {
                Wallets = new[] {
                    new Wallet{
                        Cards=new []{
                            new CreditCard{Type=CreditCardType.Visa, Balance=100},
                            new CreditCard{Type=CreditCardType.MasterCard, Balance=100}
                        }
                    }
                }
            };

            Assert.That(testObject.GetInterestAmount(person1), Is.EqualTo(16));
            Assert.That(testObject.GetInterestAmount(person1.Wallets[0]), Is.EqualTo(16));

            Assert.That(testObject.GetInterestAmount(person2), Is.EqualTo(15));
            Assert.That(testObject.GetInterestAmount(person2.Wallets[0]), Is.EqualTo(15));
        }
    }
}