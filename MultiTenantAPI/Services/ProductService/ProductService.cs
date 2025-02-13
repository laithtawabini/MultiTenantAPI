﻿using multiTenantApp.Models;
using multiTenantApp.Persistence.Contexts;
using multiTenantApp.Services.ProductService.DTOs;

namespace multiTenantApp.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context; // database context

        public ProductService(ApplicationDbContext context)
        {
            _context = context;
        }

        // get a list of all products
        public IEnumerable<Product> GetAllProducts()
        {
            var products = _context.Products.ToList();
            return products;
        }

        // get a single product
        public Product GetProductById(int id)
        {
            var product = _context.Products.Where(x => x.Id == id).FirstOrDefault();
            return product;
        }

        // create a new product
        public Product CreateProduct(CreateProductRequest request)
        {
            var product = new Product();
            product.Name = request.Name;
            product.Description = request.Description;

            _context.Add(product);
            _context.SaveChanges();

            return product;
        }


        // delete a product
        public bool DeleteProduct(int id)
        {
            var product = _context.Products.Where(x => x.Id == id).FirstOrDefault();

            if (product != null)
            {
                _context.Remove(product);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }

}
