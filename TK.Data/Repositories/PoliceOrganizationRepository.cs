using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TK.Data.Infrastructure;
using TK.Model.Models;

namespace TK.Data.Repositories
{
    public interface IPoliceOrganizationRepository : IRepository<PoliceOrganization>
    {

    }
    public class PoliceOrganizationRepository : RepositoryBase<PoliceOrganization>, IPoliceOrganizationRepository
    {
        public PoliceOrganizationRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
   
}
