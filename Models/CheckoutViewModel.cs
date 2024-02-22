using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HaluwinShop.Models
{
    public class CheckoutViewModel
    {
        public Customer Customer { get; set; }
        public OrderPro OrderPro { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
    }
}