using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace c_.DTO
{
    public class ListaDebitoPostDTO
    {
    public int DebitoId { get; set; }
    public string Descricao { get; set; }
    public string Messagem { get; set; }

    public double Valor {get; set; } = 0; 
    public int UserId { get; set; }
    // public string NomeUsuario { get; set; }
    // public string NomeUsuario {get; set;} = string.Empty;

 
    }
}