using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TK.Data.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        private TKDbContext dbContext;

        public TKDbContext Init()
        {
            return dbContext ?? (dbContext = new TKDbContext());
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}
