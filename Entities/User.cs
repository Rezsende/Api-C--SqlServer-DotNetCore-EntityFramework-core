using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace c_.Entities
{
    public class User
    {
        public int Id {get; set;}
        public string Nome{get; set;}= string.Empty;
        public string Usuarios {get; set;}= string.Empty;
        public string Senha {get; set;}= string.Empty;
        public int Nivil {get; set;}
        public string Estatus {get; set;}= string.Empty;
        public List<ListaDebito> ListaDebito { get; set;}
        public ContatoUser ContatoUsers { get; set;}
        public Endereco Endereco { get; set;}
      
        
        
    }
}