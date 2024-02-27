using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.EFs
{
    public partial class CPtsDbContext : DbContext
    {
        public CPtsDbContext() { }

        public CPtsDbContext(DbContextOptions<CPtsDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder) { }
    }
}
