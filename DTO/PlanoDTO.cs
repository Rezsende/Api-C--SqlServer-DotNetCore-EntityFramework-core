using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace c_.DTO
{
    public class PlanosDTO
    {
        
        public string Plano { get; set; } = string.Empty;
        public double ValorPlano { get; set; } = 0;

         public int Users { get; set; } 
        // Outras propriedades do plano, se necessário
    }

}