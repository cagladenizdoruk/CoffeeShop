using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoffeeShop.Models.Model
{
    public class Basket
    {
        private List<Basketline> _basketLines = new List<Basketline>();
        public List<Basketline> BasketLines
        {
            get { return _basketLines; }
        }
        public void AddProduct(Product product, int quantity)
        {
            var line = _basketLines.FirstOrDefault(i => i.Product.ProductId == product.ProductId);
            if (line == null)
            {
                _basketLines.Add(new Basketline() { Product = product, Quantity = quantity });
            }
            else
            {
                line.Quantity += quantity;
            }
        }
        public void DeleteProduct(Product product)
        {
            _basketLines.RemoveAll(i => i.Product.ProductId == product.ProductId);
        }
        public double Total()
        {
            return _basketLines.Sum(i => i.Product.Price * i.Quantity);
        }
        public void Clear()
        {
            _basketLines.Clear();
        }
    }
    public class Basketline
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
