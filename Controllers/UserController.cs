using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using c_.Context;
using c_.DTO;
using c_.Entities;
using c_.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace c_.Controllers
{
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly FarmaContext _context;

        public UserController(FarmaContext farmaContext)
        {
            _context = farmaContext;

        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] UserLoginDTO usuario)
        {
            if (!string.IsNullOrEmpty(usuario.Usuarios) && !string.IsNullOrEmpty(usuario.Senha))
            {
                var user = _context.User.FirstOrDefault(u => u.Usuarios == usuario.Usuarios && u.Senha == usuario.Senha);

                if (user != null)
                {
                    var token = TokenService.GenerateToken(user);
                    var resposta = new
                    {
                        Id = user.Id,
                        Nome = user.Nome,
                        Estatus = user.Estatus,
                        Token = token,
                    };
                    return Ok(resposta);
                }
            }

            return BadRequest("Usuário ou senha inválida");
        }

        [HttpGet("ListaTodosUsuario")]
        public IActionResult UsuarioList()
        {
            var user = _context.User.ToList();
            int quantidadeUsuarios = user.Count;

            if (user == null || user.Count == 0)
            {
                return NotFound("Nenhum usuário encontrado na lista.");
            }

            var usuariosPorNivel = _context.User
        .GroupBy(u => u.Nivil) // Agrupar os usuários pelo campo Nivel
        .Select(g => new
        {
            Nivil = g.Key,
            Quantidade = g.Count()
        })
       .ToList();




            var usuariosProjetados = user.Select(u => new
            {
                Id = u.Id,
                Nome = u.Nome,
                Nivil = u.Nivil,
                Estatus = u.Estatus,


            }).ToList();





            var resposta = new
            {
                Message = "Todos os Usuários do Sistema",
                QuantidadeUsuarios = quantidadeUsuarios,
                usuariosPorNivel = usuariosPorNivel,
                Usuarios = usuariosProjetados,

            };


            return Ok(resposta);
        }

        [HttpGet("ListaDeDebitos")]
        public IActionResult ListaDebitos()
        {
            var usuariosComDebitos = _context.User
                .Include(user => user.ListaDebito)
                .Include(user => user.ContatoUsers)

                .ToList();



            var resultado = new
            {
                Usuarios = usuariosComDebitos.Select(usuario => new
                {
                    Id = usuario.Id,
                    Nome = usuario.Nome,
                    Estatus = usuario.Estatus,
                    Debitos = usuario.ListaDebito.ToArray(),
                    Contatos = usuario.ContatoUsers,

                }).ToList()
            };

            return Ok(resultado);
        }

        [HttpPost("CriarUsuarioEContatoCmpleto")]
        public IActionResult CriarUsuarioEContato([FromBody] CriarUsuarioEContatoDto dto)
        {
            // Crie uma nova instância de User com os dados fornecidos na requisição
            var novoUsuario = new User
            {
                Nome = dto.Nome,
                Usuarios = dto.Usuarios,
                Senha = dto.Senha,
                Nivil = dto.Nivel,
                Estatus = dto.Estatus
            };

            // Crie uma nova instância de ContatoUser com os dados fornecidos na requisição
            var novoContato = new ContatoUser
            {
                Email = dto.Email,
                Celular = dto.Celular,
                CelularAdicional = dto.CelularAdicional,
                Telefone = dto.Telefone,
                Instagran = dto.Instagram,
                Facebook = dto.Facebook,
                Twiter = dto.Twitter
            };

            // Estabeleça o relacionamento 1:1 entre User e ContatoUser
            novoUsuario.ContatoUsers = novoContato;
            novoContato.User = novoUsuario;

            // Adicione as instâncias ao contexto do Entity Framework
            _context.User.Add(novoUsuario);

            // Salve as mudanças no banco de dados
            _context.SaveChanges();

            return Ok("Usuário e Contato criados com sucesso!");
        }



    }
}