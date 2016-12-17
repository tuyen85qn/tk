using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TK.Data.Infrastructure;
using TK.Model.Models;

namespace TK.Data.Repositories
{
    public interface ISituationCategoryRepository : IRepository<SituationCategory>
    {

    }
    public class SituationCategoryRepository : RepositoryBase<SituationCategory>, ISituationCategoryRepository
    {
        public SituationCategoryRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
   
}
