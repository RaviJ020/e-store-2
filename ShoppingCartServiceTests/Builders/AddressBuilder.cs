using ShoppingCartService.Models;
using System;

namespace ShoppingCartServiceTests.Builders
{
    public class AddressBuilder
    {
        private string _country = "country";
        private string _city = "city";
        private string _street = "street";

        public AddressBuilder WithCountry(string country)
        {
            _country = country;
            return this;
        }

        public AddressBuilder WithCity(string city)
        {
            _city = city;
            return this;
        }

        public AddressBuilder WithStreet(string street)
        {
            _street = street;
            return this;
        }

        public Address Build()
        {
            return new Address
            {
                Country = _country,
                City = _city,
                Street = _street,
            };
        }
    }

}
