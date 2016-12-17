using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TK.Data.Infrastructure;
using TK.Model.Models;

namespace TK.Data.Repositories
{
    public interface ISettleBodyRepository : IRepository<SettleBody>
    {

    }
    public class SettleBodyRepository : RepositoryBase<SettleBody>, ISettleBodyRepository
    {
        public SettleBodyRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
   
}
