using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;

namespace MongoDbDemo
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            const string ConnectionString = "mongodb://localhost:27017";
            string databaseName = "simple_db";
            string collectionName = "people";



            var db = DbContext.GetDb();
            var collection = db.GetCollection<PersonModel>(collectionName);

            var person = new PersonModel { FirstName = "Tim", LastName = "Corey" };

            await collection.InsertOneAsync(person);


            var results = await collection.FindAsync(_ => true);

            foreach (var result in results.ToList())
            {
                Console.WriteLine($"{result.Id}: {result.FirstName}  {result.LastName}");
            }

        }
    }
}