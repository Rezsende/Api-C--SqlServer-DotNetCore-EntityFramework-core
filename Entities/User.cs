using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace c_.Entities
{
    public class User
    {
        public int Id {get; set;}
        public string Nome{get; set;}= string.Empty;
        public string Usuarios {get; set;}= string.Empty;
        public string Senha {get; set;}= string.Empty;
        public int Nivil {get; set;}//1 cliente //2 funcionario//3 administrado
        public string Estatus {get; set;}= string.Empty;
        public List<ListaDebito> ListaDebito { get; set;}
        public ContatoUser ContatoUsers { get; set;}
        public Endereco Endereco { get; set;}

        public List<InformacaoExtra> InformacaoExtras{get; set;}
    
        
       
      
        
        
    }
}

//ui https://assinemob.com.br/450mega?gclid=CjwKCAjwzo2mBhAUEiwAf7wjkmvWUAXSMfNJIm5MiBGt7y3uKbMCvpmOEBabXcY6QCUrjXgUJQg03RoCrf0QAvD_BwE
//https://connectboahora.sgp.net.br/accounts/central/login