using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Interfaces;

namespace WebApplication1.DB
{
    public class DBRate : IRate
    {
        private AppDBContext _appDbContext;

        public DBRate(AppDBContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public float GetRate()
        {
            throw new NotImplementedException();
        }
    }
}
