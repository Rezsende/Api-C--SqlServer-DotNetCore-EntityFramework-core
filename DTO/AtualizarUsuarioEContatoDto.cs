using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace c_.DTO
{
    public class AtualizarUsuarioEContatoDto
    {
        public string Nome { get; set; }
        public string Usuarios { get; set; }
        public string Senha { get; set; }
        public int Nivel { get; set; }
        public string Estatus { get; set; }

        public string Email { get; set; }
        public string Celular { get; set; }
        public string CelularAdicional { get; set; }
        public string Telefone { get; set; }
        public string Instagram { get; set; }
        public string Facebook { get; set; }
        public string Twitter { get; set; }

        public int Cep { get; set; }
        public string Bairro { get; set; }
        public string Rua { get; set; }
        public int Numero { get; set; }
        public string Complemento { get; set; }
        public string Moradia { get; set; }
    }
}