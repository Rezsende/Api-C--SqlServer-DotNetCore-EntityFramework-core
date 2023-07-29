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
       
        public DbSet<User> Users { get; set; }    
        public DbSet<ListaDebito> ListaDebitos { get; set; }
        public DbSet<ContatoUser> ContatoUsers {get; set;}
        public DbSet<Endereco> Enderecos {get; set;}
        public DbSet<Planos> Planos {get; set;}
        public DbSet<UsersPlanos> UsersPlanos {get; set;}
        public DbSet<InformacaoExtra> InformacaoExtras {get; set;}
       
     




   
      
    }
}