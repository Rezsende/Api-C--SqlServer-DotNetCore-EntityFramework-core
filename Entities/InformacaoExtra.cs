using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace c_.Entities
{
    public class InformacaoExtra
    {
        public int Id { get; set; }
        public string Mensagem { get; set; }
        public DateTime Data { get; set; }
        //[JsonIgnore]
        public User User { get; set; }
        public int UserId { get; set; }
    }
}