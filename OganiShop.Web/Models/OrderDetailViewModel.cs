using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OganiShop.Web.Models
{
    public class OrderDetailViewModel
    {
        public int OrderID { set; get; }

        public int ProductID { set; get; }

        public int Quantity { set; get; }

        public decimal CurrentPrice { set; get; }
    }
}