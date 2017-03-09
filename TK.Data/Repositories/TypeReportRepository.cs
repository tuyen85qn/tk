using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TK.Data.Infrastructure;
using TK.Model.Models;

namespace TK.Data.Repositories
{
    public interface ITypeReportRepository : IRepository<TypeReport>
    {

    }
    public class TypeReportRepository : RepositoryBase<TypeReport>, ITypeReportRepository
    {
        public TypeReportRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
