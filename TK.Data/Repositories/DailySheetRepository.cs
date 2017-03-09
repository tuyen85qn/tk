using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TK.Data.Infrastructure;
using TK.Model.Models;

namespace TK.Data.Repositories
{
    public interface IDailySheetRepository : IRepository<DailySheet>
    {

    }
    public class DailySheetRepository : RepositoryBase<DailySheet>, IDailySheetRepository
    {
        public DailySheetRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
