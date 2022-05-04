using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautyCosmetic.Common.ViewModels
{
    public class BestSellerStatisticViewModel
    {
        private string name;
        private string quantity;
        public String Name { get { return name; }
            set {
                name = value;
            }
        }
        public int Quantity { get; set; }
    }
}
