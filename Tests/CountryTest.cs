using NUnit.Framework;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using migrationsKursovaiya.Models;
using migrationsKursovaiya.Repositories;

[TestFixture]
public class CountryRepositoryTests
{
    private MyDbContext? _context;

    [SetUp]
    public void SetUp()
    {
        var options = new DbContextOptionsBuilder<MyDbContext>()
            .UseInMemoryDatabase("TestDatabase")
            .Options;

        _context = new MyDbContext(options);
    }

    [Test]
    public void AddCountry_ShouldAddCountryWithRegions()
    {
        var countryName = "United States";
        var regions = new List<string> { "IDK1", "IDK2" };

        var repository = new CountryRepository(_context!);

        repository.AddCountry(countryName, regions);

        var addedCountry = _context!.Countries!.Include(c => c.Regions).FirstOrDefault(c => c.Name == countryName);

        Assert.That(addedCountry!.Regions.Count, Is.EqualTo(2));
        Assert.That(addedCountry.Regions.Select(r => r.Name), Is.EquivalentTo(regions));
    }

    [Test]
    public void DeleteCountry_ShoildDeleteCountryWithRegions()
    {
        var countryName = "USA";
        var country = new Country
        {
            Name = countryName,
            Regions = new List<Region>
        {
            new Region { Name = "IDK1" },
            new Region { Name = "IDK2" }
        }
        };

        _context!.Countries!.Add(country);
        _context.SaveChanges();

        var repository = new CountryRepository(_context);

        repository.DeleteCountry(countryName);

        //не знаю как проверить удаление страны
    }
}