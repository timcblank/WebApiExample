using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;
using Services;

namespace Repositories
{  

    public class ProductRepository : IProductRepository
    {
        private readonly ILogger _logger;

        IEnumerable<Product> products = new Product[]
        {
            new Product { Id = 1, Name = "Tomato Soup", Category = "Groceries", Price = 1 },
            new Product { Id = 2, Name = "Yo-yo", Category = "Toys", Price = 3.75M },
            new Product { Id = 3, Name = "Hammer", Category = "Hardware", Price = 16.99M }
        };

        public ProductRepository(ILogger logger)
        {
            _logger = logger;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return products;
        }

    }
}