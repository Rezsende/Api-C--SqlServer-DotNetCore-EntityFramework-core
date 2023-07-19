using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace c_.Entities
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome {get; set;}
        public string CPF { get; set; }
        public string Telefone { get; set; }
        public string  Cargo { get; set; }
        public int Niviel {get; set;}
        public bool   Ativo {get; set;}

    }
}