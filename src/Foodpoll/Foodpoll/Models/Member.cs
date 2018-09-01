﻿using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Foodpoll.Models
{
    public class Member
    {
        [BsonId]
        public string _id { get; set; }
        public string Name { get; set; }
        public DateTime? DeleteDate { get; set; }
        public DateTime CreateDate { get; set; }
        public IEnumerable<ShopMenu> ShopMenus { get; set; }
    }
}
