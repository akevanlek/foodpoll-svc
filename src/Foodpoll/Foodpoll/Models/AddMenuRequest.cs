using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Foodpoll.Models
{
    public class AddMenuRequest
    {
        public Menu Menu { get; set; }
        public string ShopId { get; set; }
    }
}
