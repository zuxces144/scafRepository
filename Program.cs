using Microsoft.Data.SqlClient;
using migrationsKursovaiya.Models;
using migrationsKursovaiya.Repositories;
using Newtonsoft.Json;
using Z.Dapper.Plus;
using System.Reflection.Emit;
using System.Reflection;

namespace migrationsKursovaiya
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Server=localhost;Database=Store;Trusted_Connection=True;TrustServerCertificate=True;";
            string filePath = "C:/Users/777/Desktop/someJSON.json";

            var records = ReadJson(filePath);

            var modelType = CreateDynamicModel(records);

            var dataList = MapDataToModel(records, modelType);

            string tableName = "DynamicModel";
            CreateTableInDatabase(tableName, modelType, connectionString);

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                connection.BulkInsert(dataList); 
                Console.WriteLine("Данные успешно вставлены в таблицу.");
            }
        }
    }
}
