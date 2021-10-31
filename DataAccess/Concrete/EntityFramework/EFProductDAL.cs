﻿using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EFProductDAL : EFEntityRepositoryBase<Product, NorthwindContext>, IProductDAL
    {
        public List<ProductDetailDTO> GetProdactDetails()
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                var result = from p in context.Products
                             join c in context.Categories
                             on p.CategoryId equals c.CategoryId
                             select new ProductDetailDTO
                             {
                                 ProductName = p.ProductName,
                                 CategoryName = c.CategoryName,
                                 ProductId = p.ProductId,
                                 UnitsInStock = p.UnitsInStock
                             };
                return result.ToList();
            }
        }
    }
}
