﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TK.Data.Infrastructure;
using TK.Model.Models;

namespace TK.Data.Repositories
{
    public interface ISituationTagRepository : IRepository<SituationTag>
    {

    }
    public class SituationTagRepository : RepositoryBase<SituationTag>, ISituationTagRepository
    {
        public SituationTagRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
   
}
