using MongoDataAccess.DataAccess;
using MongoDataAccess.Models;
using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;

namespace MongoDbDemo
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
           ChoreDataAccess db= new ChoreDataAccess();

            await db.CreateUser(new UserModel { FirstName = "Timm", LastName = "Corey" });

            var users = await db.GetAllUsers();

            var chore = new ChoreModel 
            { 
                AssignedTo = users.First(), 
                ChoreText = "Mow the lawn", 
                FrequencyInDays = 7 ,

            }; 
            await db.CreateChore(chore);

        }
    }
}