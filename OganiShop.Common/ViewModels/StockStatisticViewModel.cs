using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautyCosmetic.Common.ViewModels
{
    public class StockStatisticViewModel
    {
        public long ProductID { set; get; }
        public long ProductName { set; get; }
        public decimal Quantity { set; get; }

        public StockStatisticViewModel(long productID, long productName, decimal quantity)
        {
            ProductID = productID;
            ProductName = productName;
            Quantity = quantity;
        }
    }
}
