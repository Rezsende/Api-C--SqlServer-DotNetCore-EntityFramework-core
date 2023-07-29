using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace c_.Entities
{
    public class ContatoUser
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Celular {get; set;}
        public string CelularAdicional {get; set;}
        public string Telefone { get; set; }
        public string Instagran { get; set; }
        public string Facebook { get; set; }
        public string Twiter { get; set; }
   
         [JsonIgnore]
        public User User { get; set; }
        public int UserId { get; set; }
    }
}