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
        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<User> User { get; set; }    
        public DbSet<ListaDebito> ListaDebitos { get; set; }

        public DbSet<ContatoUser> ContatoUsers {get; set;}

     




   
      
    }
}