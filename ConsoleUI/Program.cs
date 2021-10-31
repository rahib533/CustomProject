using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using System;
using System.Threading.Tasks;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            //GetCategories();
            //ProductManager productManager = new ProductManager(new EFProductDAL());
            //var result = productManager.GetAll();
            //Console.WriteLine(result.Message);
            //Console.WriteLine(result.Succes);
            //foreach (var item in result.Data)
            //{
            //    Console.WriteLine(item.ProductName + " - " + item.ProductId);
            //}
        }

        private static void GetCategories()
        {
            CategoryManager categoryManager = new CategoryManager(new EFCategoryDAL());
            foreach (var item in categoryManager.GetAll())
            {
                Console.WriteLine(item.CategoryName);
            }
        }
    }
}
