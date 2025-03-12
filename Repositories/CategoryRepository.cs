using migrationsKursovaiya.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace migrationsKursovaiya.Repositories
{
    class CategoryRepository
    {
        MyDbContext context = new MyDbContext();

        public CategoryRepository()
        {

        }

        public void ShowAllCategories()
        {
            using var context = new MyDbContext();
            var categories = context.Categories?.Include(c => c.Products).ToList();

            foreach (var category in categories!)
            {
                Console.Write("\n" + category.Name);
                Console.Write($"\t\tProducts: ");
                foreach (var product in category.Products)
                {
                    Console.Write(product.Name + " ");
                }
            }
        }

        public void AddCategory(string name, List<string> products)
        {
            using var context = new MyDbContext();
            var category = new Category { Name = name };

            foreach (var product in products)
            {
                var productToAdd = context.Products?.FirstOrDefault(p => p.Name == product) ?? new Product { Name = product };
                category.Products.Add(productToAdd);
            }

            context.Categories?.Add(category);
            context.SaveChanges();
            Console.WriteLine($"Категория {name} добавлена!");
        }

        public void UpdateCategoryProducts(string categoryName, List<string> newProducts)
        {
            using var context = new MyDbContext();
            var category = context.Categories?.Include(c => c.Products).FirstOrDefault(c => c.Name == categoryName);
            if (category == null)
            {
                Console.WriteLine($"Категория с именем {categoryName} не найдена.");
                return;
            }

            category.Products.Clear();
            foreach (var productName in newProducts)
            {
                var product = context.Products?.FirstOrDefault(p => p.Name == productName) ?? new Product { Name = productName };
                category.Products.Add(product);
            }

            context.SaveChanges();
            Console.WriteLine($"Продукты в категории {categoryName} обновлены.");
        }

        public void DeleteCategory(string categoryName)
        {
            using var context = new MyDbContext();
            var category = context.Categories?.Include(c => c.Products).FirstOrDefault(c => c.Name == categoryName);
            if (category == null)
            {
                Console.WriteLine($"Категория {categoryName} не найдена.");
                return;
            }

            context.Categories?.Remove(category);
            context.SaveChanges();
            Console.WriteLine($"Категория {categoryName} удалена!");
        }

        public void GetById(int id)
        {
            using var context = new MyDbContext();
            var category = context.Categories?.Include(c => c.Products).FirstOrDefault(c => c.Id == id);
            if (category == null)
            {
                Console.WriteLine($"Категория с Id {id} не найдена.");
                return;
            }

            Console.WriteLine($"Категория: {category.Name} найдена по Id {id}");
        }
    }
}
