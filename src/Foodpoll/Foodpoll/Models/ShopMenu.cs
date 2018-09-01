using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Foodpoll.Models
{
    public class ShopMenu
    {
        [BsonId]
        public string _id { get; set; }
        public Shop Shop { get; set; }
        public Menu DefaultMenu { get; set; }
    }
}
