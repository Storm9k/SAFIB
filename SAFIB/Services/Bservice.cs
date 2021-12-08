using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SAFIB.Models;

namespace SAFIB.Services
{
    public class Bservice
    {
        private AppDbContext dbcontext;
        public List<Subvision> SubvisionsResult;
        public Bservice (AppDbContext dbContext) : base ()
        {
            dbcontext = dbContext;
            GetSubvisions();
        }

        public List<Subvision> GetSubvisions()
        {
            SubvisionsResult = dbcontext.Subvisions.ToList();
            return SubvisionsResult;
        }
    }
}
