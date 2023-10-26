using MongoDataAccess.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDataAccess.DataAccess
{
    public class ChoreDataAccess
    {

        public const string ConnectionString = "mongodb://localhost:27017";
        public const string DatabaseName = "choredb";
        public const string ChoreCollection = "chore_cart";
        public const string UserCollection = "users";
        public const string ChoreHistoryCollection = "chore_history";

        private IMongoCollection<T> ConnectToMongo<T>(in string collection)
        {


            var client = new MongoClient(ConnectionString);
            var db = client.GetDatabase(DatabaseName);
            var result = db.GetCollection<T>(collection);

            return result;
        }

        public async Task<List<UserModel>> GetAllUsers()
        {
            var usersCollection = ConnectToMongo<UserModel>(UserCollection);
            var results = await usersCollection.FindAsync(_ => true);
            return results.ToList();
        }
        public async Task<List<ChoreModel>> GetAllChores()
        {
            var choresCollection = ConnectToMongo<ChoreModel>(ChoreCollection);
            var results = await choresCollection.FindAsync(_ => true);
            return results.ToList();
        }

        public async Task<List<ChoreModel>> GetAllChoresForUser(UserModel user)
        {
            var choresCollection = ConnectToMongo<ChoreModel>(ChoreCollection);
            var results = await choresCollection.FindAsync(c => c.AssignedTo.Id==user.Id);
            return results.ToList();
        }
        public Task CreateUser(UserModel user)
        {
            var usersCollection=ConnectToMongo<UserModel>(UserCollection);
            return usersCollection.InsertOneAsync(user);
        }
        public Task CreateChore(ChoreModel chore) 
        {
            var choresCollection = ConnectToMongo<ChoreModel>(ChoreCollection);
            return choresCollection.InsertOneAsync(chore);

        }

        public Task UpdateChore(ChoreModel chore)
        {
            var choresCollection = ConnectToMongo<ChoreModel>(ChoreCollection);
            var filter = Builders<ChoreModel>.Filter.Eq("Id", chore.Id);
            return choresCollection.ReplaceOneAsync(filter, chore,new ReplaceOptions { IsUpsert=true});
        }

        public Task DeleteChore(ChoreModel chore)
        {
            var choresCollection = ConnectToMongo<ChoreModel>(ChoreCollection);
            return choresCollection.DeleteOneAsync(c => c.Id == chore.Id);
        }

        public async Task CompleteChore(ChoreModel chore)
        {
            var choresCollection = ConnectToMongo<ChoreModel>(ChoreCollection);
            var filter =Builders<ChoreModel>.Filter.Eq("Id",chore.Id);
            await choresCollection.ReplaceOneAsync(filter, chore);


            var choreHistoryCollection = ConnectToMongo<ChoreHistoryModel>(ChoreHistoryCollection);
            await choreHistoryCollection.InsertOneAsync(new ChoreHistoryModel(chore));


        }
    }
}
