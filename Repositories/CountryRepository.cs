using migrationsKursovaiya.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using System.Data;
using Dapper;

namespace migrationsKursovaiya.Repositories
{
    class CountryRepository
    {
        MyDbContext context = new MyDbContext();

        public CountryRepository(MyDbContext _context)
        {
            context = _context;
        }

        private readonly string _connection;

        public CountryRepository(string connectionString)
        {
            _connection = connectionString;
        }

        private IDbConnection CreateConnection()
        {
            return new SqlConnection(_connection);
        }

        //Dapper

        public IEnumerable<Country> GetAllCountries()
        {
            using (var connection = new SqlConnection(_connection))
            {
                connection.Open();
                return connection.Query<Country>("SELECT * FROM Countries");
            }
        }

        //public void ShowAllCountry()
        //{
        //    using var context = new MyDbContext();
        //    var countries = context.Countries?.Include(c => c.Regions).ToList();

        //    foreach(var Country in countries!)
        //    {
        //        Console.Write("\n" + Country.Name);
        //        Console.Write($"\t\tRegions: ");
        //        foreach (var Region in Country.Regions)
        //        {
        //            Console.Write(Region.Name + " ");
        //        }
        //    }
        //}

        public void AddCountry(string name, List<string> regions)
        {
            using var connection = CreateConnection();

            connection.Execute("INSERT INTO Countries (Name) VALUES (@Name)", new { Name = name });

            foreach (var region in regions)
            {
                string insertRegionSql = "INSERT INTO Regions (Name, CountryId) VALUES (@Name, @CountryId)";
                connection.Execute(insertRegionSql, new { Name = region});
            }
        }


        //public void AddCountry(string name, List<string> regions)
        //{
        //    using var context = new MyDbContext();
        //    var country = new Country { Name = name };

        //    foreach (var region in regions)
        //    {
        //        var regionToAdd = context.Regions?.FirstOrDefault(r => r.Name == region) ?? new Region { Name = region };
        //        country.Regions.Add(regionToAdd);
        //    }

        //    context.Countries?.Add(country);
        //    context.SaveChanges();
        //    Console.WriteLine($"Страна {name} добавлена!");
        //}

        //public void UpdateCountryRegions(string countryName, List<string> newRegions)
        //{
        //    using var context = new MyDbContext();
        //    var country = context.Countries?.Include(c => c.Regions).FirstOrDefault(p => p.Name == countryName);
        //    if (country == null)
        //    {
        //        Console.WriteLine($"Страна с именем {countryName} не найденa.");
        //        return;
        //    }

        //    country.Regions.Clear();
        //    foreach (var regionName in newRegions)
        //    {
        //        var region = context.Regions?.FirstOrDefault(r => r.Name == regionName) ?? new Region { Name = regionName };
        //        country.Regions.Add(region);
        //    }
        //    context.SaveChanges();
        //    Console.WriteLine($"Регионы страны {countryName} обновлены");
        //}

        public bool UpdateCountryRegions(string countryName, List<string> newRegions)
        {
            using var connection = CreateConnection();
            int? countryId = connection.QueryFirstOrDefault<int?>("SELECT Id FROM Countries WHERE Name = @Name", new { Name = countryName });

            if (countryId == null)
            {
                Console.WriteLine($"Страна {countryName} не найдена.");
                return false;
            }

            connection.Execute("DELETE FROM Regions WHERE CountryId = @CountryId", new { CountryId = countryId });

            foreach (var region in newRegions)
            {
                connection.Execute("INSERT INTO Regions (Name, CountryId) VALUES (@Name, @CountryId)", new { Name = region, CountryId = countryId });
            }
            return true;
        }


        public bool DeleteCountry(string countryName)
        {
            using var connection = CreateConnection();
            int? countryId = connection.QueryFirstOrDefault<int?>("SELECT Id FROM Countries WHERE Name = @Name", new { Name = countryName });

            if (countryId == null)
            {
                return false;
            }
            connection.Execute("DELETE FROM Regions WHERE CountryId = @CountryId", new { CountryId = countryId });

            connection.Execute("DELETE FROM Countries WHERE Id = @Id", new { Id = countryId });
            return true;
        }

        //public void DeleteCountry(string countryName)
        //{
        //    using var context = new MyDbContext();
        //    var country = context.Countries?.Include(c => c.Regions).FirstOrDefault(c => c.Name == countryName);
        //    if (country == null)
        //    {
        //        Console.WriteLine($"Страна {countryName} не найдена.");
        //        return;
        //    }

        //    context.Countries?.Remove(country);
        //    context.SaveChanges();
        //    Console.WriteLine($"Страна {countryName} удаленa!");
        //}

        public void GetById(int id)
        {
            using var context = new MyDbContext();
            var country = context.Countries?.Include(c => c.Regions).FirstOrDefault(c => c.Id == id);
            if (country == null)
            {
                Console.WriteLine($"Страна с Id {id} не найдена.");
                return;
            }
            
            Console.WriteLine($"Страна: {country.Name} найдена по id{id}");
        }

    }
}
