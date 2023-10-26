using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZstdSharp;

namespace MongoDbDemo
{
    public class DbContext
    {
        public static string ConStr = "mongodb://localhost:27017";
        public static string DbName = "simple_db";

        public ObjectId _id{ get; set; }
        public static IMongoDatabase GetDb()
        {
            var client = new MongoClient(ConStr);
            var db = client.GetDatabase(DbName);
            return db;
        }

        private IMongoCollection<T> ConnectToMongo<T>(in string collection) {


            var client = new MongoClient(ConStr);
            var db = client.GetDatabase(DbName);
            var result=db.GetCollection<T>(collection);

            return result;        
        }
       
        public async Task Add(PersonModel model,string collectionName)
        {
            var addEntity = ConnectToMongo<PersonModel>("people");
            await addEntity.InsertOneAsync(model);
        }

        
    }
}
