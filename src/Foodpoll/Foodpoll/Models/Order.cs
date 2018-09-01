using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Foodpoll.Models
{
    public class Order
    {
        public string _id{ get; set; }
        public Menu Menu { get; set; }
        public string Username { get; set; }
    }
}
