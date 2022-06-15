using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OganiShop.Data.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        private OganiShopDbContext dbContext;

        public OganiShopDbContext Init()
        {
            return dbContext ?? (dbContext = new OganiShopDbContext());
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}
