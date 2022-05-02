using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OganiShop.Web.Models
{
    public class HomeViewModel
    {
        public IEnumerable<SlideViewModel> Slides { set; get; }
        public IEnumerable<ProductViewModel> LastestProducts { set; get; }
        public IEnumerable<ProductViewModel> TopSaleProducts { set; get; }
        public IEnumerable<ProductViewModel> GetAll { set; get; }

        public string Title { set; get; }
        public string MetaKeyword { set; get; }
        public string MetaDescription { set; get; }
        public IEnumerable<ProductCategoryViewModel> ProductCategorys { set; get; }

    }
}