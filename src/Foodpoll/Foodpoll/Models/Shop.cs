using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Foodpoll.Models
{
    public class Shop
    {
        [BsonId]
        public string _id { get; set; }
        public string Name { get; set; }
        public string Detail { get; set; }
        public IEnumerable<Menu> Menus { get; set; }
        public Menu DefaultMenu { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
    }
}
