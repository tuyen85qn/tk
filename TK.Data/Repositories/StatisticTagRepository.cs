﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TK.Data.Infrastructure;
using TK.Model.Models;

namespace TK.Data.Repositories
{
    public interface IStatisticTagRepository : IRepository<StatisticTag>
    {

    }
    public class StatisticTagRepository : RepositoryBase<StatisticTag>, IStatisticTagRepository
    {
        public StatisticTagRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
   
}
