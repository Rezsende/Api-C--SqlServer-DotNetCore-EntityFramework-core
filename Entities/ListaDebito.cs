using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace c_.Entities
{
  


public class ListaDebito
{
    public int Id { get; set; }
    public string Descricao { get; set; }=string.Empty;
    public string Messagem { get; set; }= string.Empty;
    public double Valor {get; set; } = 0; 
    [JsonIgnore]
    public User User { get; set; }
    public int UserId { get; set; }


}



}