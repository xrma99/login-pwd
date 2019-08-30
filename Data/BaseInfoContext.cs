using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace NewMylogin.Models
{
    public class BaseInfoContext : DbContext
    {
        public BaseInfoContext (DbContextOptions<BaseInfoContext> options)
            : base(options)
        {
        }

        public DbSet<NewMylogin.Models.BasicInfo> BasicInfo { get; set; }
    }
}
