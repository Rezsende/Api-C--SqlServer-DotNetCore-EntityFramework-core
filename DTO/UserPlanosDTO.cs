using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using c_.Entities;

namespace c_.DTO
{
    public class UserPlanosDTO
    {
        public string Nome { get; set; }
        public string Estatus { get; set; }

        public Endereco Endereco {get; set;}
    }
}