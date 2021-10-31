using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.InMemory
{
    public class InMemoryProductDAL : IProductDAL
    {
        List<Product> products;
        public InMemoryProductDAL()
        {
            products = new List<Product> {
                new Product{ProductId = 1, CategoryId = 1, ProductName = "Car" , UnitPrice = 13000, UnitsInStock=19},
                new Product{ProductId = 2, CategoryId = 2, ProductName = "Phone", UnitsInStock = 89, UnitPrice=955},
                new Product{ProductId = 3, CategoryId = 2, ProductName = "Mause", UnitsInStock = 12, UnitPrice=35},
                new Product{ProductId = 4, CategoryId = 3, ProductName = "Book", UnitsInStock = 9, UnitPrice=15},
                new Product{ProductId = 5, CategoryId = 4, ProductName = "Coca-Cola", UnitsInStock = 8, UnitPrice=5}
            };
        }
        public void Add(Product product)
        {
            products.Add(product);
        }

        public void Delete(Product product)
        {
            var productToDelete = products.SingleOrDefault(x => x.ProductId == product.ProductId);

            products.Remove(productToDelete);
        }

        public Product Get(Expression<Func<Product, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetAll()
        {
            return products;
        }

        public List<Product> GetAll(Expression<Func<Product, bool>> expression = null)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetAllByCategoryId(int categoryId)
        {
            return products.Where(x => x.CategoryId == categoryId).ToList();
        }

        public List<ProductDetailDTO> GetProdactDetails()
        {
            throw new NotImplementedException();
        }

        public void Update(Product product)
        {
            var productToUpdate = products.SingleOrDefault(x => x.ProductId == product.ProductId);

            productToUpdate.ProductName = product.ProductName;
            productToUpdate.CategoryId = product.CategoryId;
            productToUpdate.UnitPrice = product.UnitPrice;
            productToUpdate.UnitsInStock = product.UnitsInStock;
            
        }
    }
}
