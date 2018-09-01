using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Foodpoll.Models
{
    public class Poll
    {
        [BsonId]
        public string _id { get; set; }
        public string ShopId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? CloseDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public IEnumerable<Order> Orders { get; set; }

    }
}
