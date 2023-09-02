using ShoppingCartService.DataAccess.Entities;

namespace ShoppingCartServiceTests.Builders
{
    public class ItemBuilder
    {
        private string _productId = "prod-1";
        private string _productName = "Product 1";
        private double _price = 1.0;
        private uint _quantity = 1;

        public ItemBuilder WithProductId(string productId)
        {
            _productId = productId;
            return this;
        }

        public ItemBuilder WithProductName(string productName)
        {
            _productName = productName;
            return this;
        }

        public ItemBuilder WithPrice(double price)
        {
            _price = price;
            return this;
        }

        public ItemBuilder WithQuantity(uint quantity)
        {
            _quantity = quantity;
            return this;
        }

        public Item Build()
        {
            return new Item
            {
                ProductId = _productId,
                ProductName = _productName,
                Price = _price,
                Quantity = _quantity
            };
        }
    }

}
