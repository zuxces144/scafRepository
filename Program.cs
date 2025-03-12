using migrationsKursovaiya.Models;
using migrationsKursovaiya.Repositories;

namespace migrationsKursovaiya
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var temp = new CountryRepository();
            temp.ShowAllCountry();
        }
    }
}
