using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Foodpoll.Models
{
    public class ClosedPoll
    {
        [BsonId]
        public string _id { get; set; }
        public Poll Poll { get; set; }
    }
}
