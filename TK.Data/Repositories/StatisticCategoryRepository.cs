using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TK.Data.Infrastructure;
using TK.Model.Models;

namespace TK.Data.Repositories
{
    public interface IStatisticCategoryRepository : IRepository<StatisticCategory>
    {

    }
    public class StatisticCategoryRepository : RepositoryBase<StatisticCategory>, IStatisticCategoryRepository
    {
        public StatisticCategoryRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
   
}
