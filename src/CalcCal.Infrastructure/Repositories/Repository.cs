using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalcCal.Infrastructure.Repositories
{
    public abstract class Repository
    {
        protected readonly DbContext Context;

        protected Repository(DbContext context)
        {
            Context = context;
        }
    }
}
