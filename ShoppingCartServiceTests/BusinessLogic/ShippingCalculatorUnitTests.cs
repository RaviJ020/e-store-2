using ShoppingCartService.BusinessLogic;
using ShoppingCartService.DataAccess.Entities;
using ShoppingCartService.Models;
using ShoppingCartServiceTests.Builders;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;

namespace ShoppingCartServiceTests.BusinessLogic
{
    public class ShippingCalculatorUnitTests
    {
        [Theory]
        [MemberData(nameof(TestData.Data), MemberType = typeof(TestData))]
        public void TestCalculateShippingCost(TestCase testCase)
        {
            // Assign
            var office = new AddressBuilder()
                .WithCity("city 1")
                .WithCountry("country 1")
                .Build();

            var cart = new CartBuilder()
                .WithCustomerType(testCase.CustomerType)
                .WithShippingMethod(testCase.ShippingMethod)
                .WithShippingAddress(testCase.ShippingAddress)
                .WithItems(testCase.Items)
                .Build();

            var calculator = new ShippingCalculator(office);

            // Act
            var result = calculator.CalculateShippingCost(cart);

            // Assert
            Assert.Equal(result, testCase.ExpectedCost);
        }

        public class TestData
        {
            public static IEnumerable<object[]> Data
            {
                get
                {
                    yield return new object[] { 
                        new TestCase {
                            Name = "SameCityStandardShippingNoItems",
                            ShippingAddress = new AddressBuilder().WithCity("city 1").WithCountry("country 1").Build(),
                            ShippingMethod = ShippingMethod.Standard,
                            CustomerType = CustomerType.Standard,
                            Items = new List<Item>(),
                            ExpectedCost = 0 
                        } 
                    };
                    yield return new object[] {
                        new TestCase {
                            Name = "SameCityStandardShippingOneItemsQuantity1",
                            ShippingAddress = new AddressBuilder().WithCity("city 1").WithCountry("country 1").Build(),
                            ShippingMethod = ShippingMethod.Standard,
                            CustomerType = CustomerType.Standard,
                            Items = new List<Item> { new ItemBuilder().WithQuantity(1).Build() },
                            ExpectedCost = 1 * ShippingCalculator.SameCityRate
                        }
                    };
                    yield return new object[] {
                        new TestCase {
                            Name = "SameCityStandardShippingOneItemsQuantity5",
                            ShippingAddress = new AddressBuilder().WithCity("city 1").WithCountry("country 1").Build(),
                            ShippingMethod = ShippingMethod.Standard,
                            CustomerType = CustomerType.Standard,
                            Items = new List<Item> { new ItemBuilder().WithQuantity(5).Build() },
                            ExpectedCost = 5 * ShippingCalculator.SameCityRate
                        }
                    };
                    yield return new object[] {
                        new TestCase {
                            Name = "SameCityStandardShippingTwoItems",
                            ShippingAddress = new AddressBuilder().WithCity("city 1").WithCountry("country 1").Build(),
                            ShippingMethod = ShippingMethod.Standard,
                            CustomerType = CustomerType.Standard,
                            Items = new List<Item> 
                            { 
                                new ItemBuilder().WithQuantity(5).Build(), 
                                new ItemBuilder().WithQuantity(3).Build()
                            },
                            ExpectedCost = (5 + 3) * ShippingCalculator.SameCityRate
                        }
                    };
                    yield return new object[] {
                        new TestCase {
                            Name = "SameCountryStandardShippingNoItems",
                            ShippingAddress = new AddressBuilder().WithCity("city 2").WithCountry("country 1").Build(),
                            ShippingMethod = ShippingMethod.Standard,
                            CustomerType = CustomerType.Standard,
                            Items = new List<Item>(),
                            ExpectedCost = 0
                        }
                    };
                    yield return new object[] {
                        new TestCase {
                            Name = "SameCountryStandardShippingOneItemsQuantity1",
                            ShippingAddress = new AddressBuilder().WithCity("city 2").WithCountry("country 1").Build(),
                            ShippingMethod = ShippingMethod.Standard,
                            CustomerType = CustomerType.Standard,
                            Items = new List<Item> { new ItemBuilder().WithQuantity(1).Build() },
                            ExpectedCost = 1 * ShippingCalculator.SameCountryRate
                        }
                    };
                    yield return new object[] {
                        new TestCase {
                            Name = "SameCountryStandardShippingOneItemsQuantity5",
                            ShippingAddress = new AddressBuilder().WithCity("city 2").WithCountry("country 1").Build(),
                            ShippingMethod = ShippingMethod.Standard,
                            CustomerType = CustomerType.Standard,
                            Items = new List<Item> { new ItemBuilder().WithQuantity(5).Build() },
                            ExpectedCost = 5 * ShippingCalculator.SameCountryRate
                        }
                    };
                    yield return new object[] {
                        new TestCase {
                            Name = "SameCountryStandardShippingTwoItems",
                            ShippingAddress = new AddressBuilder().WithCity("city 2").WithCountry("country 1").Build(),
                            ShippingMethod = ShippingMethod.Standard,
                            CustomerType = CustomerType.Standard,
                            Items = new List<Item>
                            {
                                new ItemBuilder().WithQuantity(5).Build(),
                                new ItemBuilder().WithQuantity(3).Build()
                            },
                            ExpectedCost = (5 + 3) * ShippingCalculator.SameCountryRate
                        }
                    };
                    yield return new object[] {
                        new TestCase {
                            Name = "InternationalShippingStandardShippingNoItems",
                            ShippingAddress = new AddressBuilder().WithCity("city 2").WithCountry("country 1").Build(),
                            ShippingMethod = ShippingMethod.Standard,
                            CustomerType = CustomerType.Standard,
                            Items = new List<Item>(),
                            ExpectedCost = 0
                        }
                    };
                    yield return new object[] {
                        new TestCase {
                            Name = "InternationalShippingStandardShippingOneItemsQuantity1",
                            ShippingAddress = new AddressBuilder().WithCity("city 2").WithCountry("country 2").Build(),
                            ShippingMethod = ShippingMethod.Standard,
                            CustomerType = CustomerType.Standard,
                            Items = new List<Item> { new ItemBuilder().WithQuantity(1).Build() },
                            ExpectedCost = 1 * ShippingCalculator.InternationalShippingRate
                        }
                    };
                    yield return new object[] {
                        new TestCase {
                            Name = "InternationalShippingStandardShippingOneItemsQuantity5",
                            ShippingAddress = new AddressBuilder().WithCity("city 2").WithCountry("country 2").Build(),
                            ShippingMethod = ShippingMethod.Standard,
                            CustomerType = CustomerType.Standard,
                            Items = new List<Item> { new ItemBuilder().WithQuantity(5).Build() },
                            ExpectedCost = 5 * ShippingCalculator.InternationalShippingRate
                        }
                    };
                    yield return new object[] {
                        new TestCase {
                            Name = "InternationalShippingStandardShippingTwoItems",
                            ShippingAddress = new AddressBuilder().WithCity("city 2").WithCountry("country 2").Build(),
                            ShippingMethod = ShippingMethod.Standard,
                            CustomerType = CustomerType.Standard,
                            Items = new List<Item>
                            {
                                new ItemBuilder().WithQuantity(5).Build(),
                                new ItemBuilder().WithQuantity(3).Build()
                            },
                            ExpectedCost = (5 + 3) * ShippingCalculator.InternationalShippingRate
                        }
                    };
                    yield return new object[] {
                        new TestCase {
                            Name = "SameCityExpeditedShippingOneItemsQuantity1",
                            ShippingAddress = new AddressBuilder().WithCity("city 1").WithCountry("country 1").Build(),
                            ShippingMethod = ShippingMethod.Expedited,
                            CustomerType = CustomerType.Standard,
                            Items = new List<Item> { new ItemBuilder().WithQuantity(1).Build() },
                            ExpectedCost = 1 * ShippingCalculator.SameCityRate * 1.2
                        }
                    };
                    yield return new object[] {
                        new TestCase {
                            Name = "SameCountryExpeditedShippingOneItemsQuantity1",
                            ShippingAddress = new AddressBuilder().WithCity("city 2").WithCountry("country 1").Build(),
                            ShippingMethod = ShippingMethod.Expedited,
                            CustomerType = CustomerType.Standard,
                            Items = new List<Item> { new ItemBuilder().WithQuantity(1).Build() },
                            ExpectedCost = 1 * ShippingCalculator.SameCountryRate * 1.2
                        }
                    };
                    yield return new object[] {
                        new TestCase {
                            Name = "InternationalShippingExpeditedShippingOneItemsQuantity1",
                            ShippingAddress = new AddressBuilder().WithCity("city 2").WithCountry("country 2").Build(),
                            ShippingMethod = ShippingMethod.Expedited,
                            CustomerType = CustomerType.Standard,
                            Items = new List<Item> { new ItemBuilder().WithQuantity(1).Build() },
                            ExpectedCost = 1 * ShippingCalculator.InternationalShippingRate * 1.2
                        }
                    };
                    yield return new object[] {
                        new TestCase {
                            Name = "SameCityPriorityShippingOneItemsQuantity1",
                            ShippingAddress = new AddressBuilder().WithCity("city 1").WithCountry("country 1").Build(),
                            ShippingMethod = ShippingMethod.Priority,
                            CustomerType = CustomerType.Standard,
                            Items = new List<Item> { new ItemBuilder().WithQuantity(1).Build() },
                            ExpectedCost = 1 * ShippingCalculator.SameCityRate * 2
                        }
                    };
                    yield return new object[] {
                        new TestCase {
                            Name = "SameCountryPriorityShippingOneItemsQuantity1",
                            ShippingAddress = new AddressBuilder().WithCity("city 2").WithCountry("country 1").Build(),
                            ShippingMethod = ShippingMethod.Priority,
                            CustomerType = CustomerType.Standard,
                            Items = new List<Item> { new ItemBuilder().WithQuantity(1).Build() },
                            ExpectedCost = 1 * ShippingCalculator.SameCountryRate * 2
                        }
                    };
                    yield return new object[] {
                        new TestCase {
                            Name = "InternationalShippingPriorityShippingOneItemsQuantity1",
                            ShippingAddress = new AddressBuilder().WithCity("city 2").WithCountry("country 2").Build(),
                            ShippingMethod = ShippingMethod.Priority,
                            CustomerType = CustomerType.Standard,
                            Items = new List<Item> { new ItemBuilder().WithQuantity(1).Build() },
                            ExpectedCost = 1 * ShippingCalculator.InternationalShippingRate * 2
                        }
                    };
                    yield return new object[] {
                        new TestCase {
                            Name = "SameCityExpressShippingOneItemsQuantity1",
                            ShippingAddress = new AddressBuilder().WithCity("city 1").WithCountry("country 1").Build(),
                            ShippingMethod = ShippingMethod.Express,
                            CustomerType = CustomerType.Standard,
                            Items = new List<Item> { new ItemBuilder().WithQuantity(1).Build() },
                            ExpectedCost = 1 * ShippingCalculator.SameCityRate * 2.5
                        }
                    };
                    yield return new object[] {
                        new TestCase {
                            Name = "SameCountryExpressShippingOneItemsQuantity1",
                            ShippingAddress = new AddressBuilder().WithCity("city 2").WithCountry("country 1").Build(),
                            ShippingMethod = ShippingMethod.Express,
                            CustomerType = CustomerType.Standard,
                            Items = new List<Item> { new ItemBuilder().WithQuantity(1).Build() },
                            ExpectedCost = 1 * ShippingCalculator.SameCountryRate * 2.5
                        }
                    };
                    yield return new object[] {
                        new TestCase {
                            Name = "InternationalShippingExpressShippingOneItemsQuantity1",
                            ShippingAddress = new AddressBuilder().WithCity("city 2").WithCountry("country 2").Build(),
                            ShippingMethod = ShippingMethod.Express,
                            CustomerType = CustomerType.Standard,
                            Items = new List<Item> { new ItemBuilder().WithQuantity(1).Build() },
                            ExpectedCost = 1 * ShippingCalculator.InternationalShippingRate * 2.5
                        }
                    };
                    yield return new object[] {
                        new TestCase {
                            Name = "InternationalShippingPriorityShippingPremiumCustomerOneItemsQuantity1",
                            ShippingAddress = new AddressBuilder().WithCity("city 2").WithCountry("country 2").Build(),
                            ShippingMethod = ShippingMethod.Priority,
                            CustomerType = CustomerType.Premium,
                            Items = new List<Item> { new ItemBuilder().WithQuantity(1).Build() },
                            ExpectedCost = 1 * ShippingCalculator.InternationalShippingRate
                        }
                    };
                    yield return new object[] {
                        new TestCase {
                            Name = "InternationalShippingExpeditedShippingPremiumCustomerOneItemsQuantity1",
                            ShippingAddress = new AddressBuilder().WithCity("city 2").WithCountry("country 2").Build(),
                            ShippingMethod = ShippingMethod.Expedited,
                            CustomerType = CustomerType.Premium,
                            Items = new List<Item> { new ItemBuilder().WithQuantity(1).Build() },
                            ExpectedCost = 1 * ShippingCalculator.InternationalShippingRate
                        }
                    };
                    yield return new object[] {
                        new TestCase {
                            Name = "InternationalShippingExpressShippingVIPCustomerOneItemsQuantity1",
                            ShippingAddress = new AddressBuilder().WithCity("city 2").WithCountry("country 2").Build(),
                            ShippingMethod = ShippingMethod.Express,
                            CustomerType = CustomerType.Premium,
                            Items = new List<Item> { new ItemBuilder().WithQuantity(1).Build() },
                            ExpectedCost = 1 * ShippingCalculator.InternationalShippingRate * 2.5
                        }
                    };
                }
            }
        }

        public class TestCase
        {
            public string Name { get; set; }
            public Address ShippingAddress { get; set; }
            public ShippingMethod ShippingMethod { get; set; }
            public CustomerType CustomerType { get; set; }
            public List<Item> Items { get; set; }
            public double ExpectedCost { get; set; }
        }
    }
}