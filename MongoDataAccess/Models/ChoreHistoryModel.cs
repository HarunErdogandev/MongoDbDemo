using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDataAccess.Models
{
    public class ChoreHistoryModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ChoreId { get; set; }
        public string ChoreText { get; set; }
        public int FrequencyInDays { get; set; }

        public DateTime? DataCompleted { get; set; }
        public UserModel? WhoCompleted { get; set; }

        public ChoreHistoryModel(ChoreModel chore)
        {
            ChoreId = chore.Id;
            DataCompleted = chore.LastCompleted ?? DateTime.Now;
            WhoCompleted = chore.AssignedTo;
            ChoreText= chore.ChoreText;
        }

    }
}
