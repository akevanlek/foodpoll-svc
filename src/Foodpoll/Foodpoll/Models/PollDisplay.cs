using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Foodpoll.Models
{
    public class PollDisplay
    {
        public string _id { get; set; }
        public Shop Shop { get; set; }     
        public IEnumerable<Order> Orders { get; set; }
        public Menu DefaultMenu { get; set; }
        public Menu MyDefaultMenu { get; set; }
        public Menu MyOrder { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
