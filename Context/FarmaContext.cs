using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using c_.Entities;
using Microsoft.EntityFrameworkCore;

namespace c_.Context
{
    public class FarmaContext : DbContext
    {
        public FarmaContext(DbContextOptions<FarmaContext> options) : base(options)
        {

        }
        public DbSet<Usuario> usuarios{get; set;}
    }
}