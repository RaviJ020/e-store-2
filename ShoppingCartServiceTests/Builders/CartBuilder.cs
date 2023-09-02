using ShoppingCartService.DataAccess.Entities;
using ShoppingCartService.Models;
using System.Collections.Generic;

namespace ShoppingCartServiceTests.Builders
{
    public class CartBuilder
    {
        private string _id = "1";
        private string _customerId = "cust-1";
        private CustomerType _customerType = CustomerType.Standard;
        private ShippingMethod _shippingMethod = ShippingMethod.Standard;
        private Address _shippingAddress = new AddressBuilder().Build();
        private List<Item> _items = new List<Item>();

        public CartBuilder WithId(string id)
        {
            _id = id;
            return this;
        }

        public CartBuilder WithCustomerId(string customerId)
        {
            _customerId = customerId;
            return this;
        }

        public CartBuilder WithCustomerType(CustomerType customerType)
        {
            _customerType = customerType;
            return this;
        }

        public CartBuilder WithShippingMethod(ShippingMethod shippingMethod)
        {
            _shippingMethod = shippingMethod;
            return this;
        }

        public CartBuilder WithShippingAddress(Address shippingAddress)
        {
            _shippingAddress = shippingAddress;
            return this;
        }

        public CartBuilder WithItem(Item item)
        {
            _items.Add(item);
            return this;
        }

        public CartBuilder WithItems(List<Item> items)
        {
            _items = items;
            return this;
        }

        public Cart Build()
        {
            return new Cart
            {
                Id = _id,
                CustomerId = _customerId,
                CustomerType = _customerType,
                ShippingMethod = _shippingMethod,
                ShippingAddress = _shippingAddress,
                Items = _items
            };
        }
    }
}
