using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace c_.Entities
{
    public class Usuario
    {
        public int Id { get; set; }
        public string? Nome { get; set; }

        public string? Email {get ; set;}

        public string Senha {get; set;}
        public string? CPF { get; set; }
        public string? Telefone { get; set; }
        public string? Cargo { get; set; }
        public int? Niviel { get; set; }
        public string? Rg {get; set;}
        public string? Endereco {get; set;}
       [NotMapped]
        public IFormFile? PhotoFile { get; set; }

        public string? PhotoFileName { get; set; }

        


   }
}
