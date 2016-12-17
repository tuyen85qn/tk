using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TK.Data.Infrastructure;
using TK.Model.Models;

namespace TK.Data.Repositories
{
    public interface ISituationRepository: IRepository<Situation>
    {

    }
    public class SituationRepository : RepositoryBase<Situation>, ISituationRepository
    {
        public SituationRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
