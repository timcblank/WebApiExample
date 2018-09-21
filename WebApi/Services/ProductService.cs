using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Repositories;
using Models;

namespace Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repo;
        private readonly ILogger _logger;

        public ProductService(ILogger logger, IProductRepository repo)
        {
            _logger = logger;
            _repo = repo;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _repo.GetAllProducts();
        }

        public Product GetProductsByID(int id)
        {
            var product = GetAllProducts().FirstOrDefault((p) => p.Id == id);
            if (product == null)
            {
                return null;
            }

            return product;
        }
    }
}