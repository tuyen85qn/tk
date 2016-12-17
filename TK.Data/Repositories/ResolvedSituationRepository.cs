using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TK.Data.Infrastructure;
using TK.Model.Models;

namespace TK.Data.Repositories
{
    public interface IResolvedSituationRepository : IRepository<ResolvedSituation>
    {

    }
    public class ResolvedSituationRepository : RepositoryBase<ResolvedSituation>, IResolvedSituationRepository
    {
        public ResolvedSituationRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
   
}
