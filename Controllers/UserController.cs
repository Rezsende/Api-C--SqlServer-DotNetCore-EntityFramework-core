using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using c_.Context;
using c_.DTO;
using c_.Entities;
using c_.Services;
using Microsoft.AspNetCore.Authorization;
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
                var user = _context.Users.FirstOrDefault(u => u.Usuarios == usuario.Usuarios && u.Senha == usuario.Senha);

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
        [Authorize]
        [HttpGet("UsuariosPorId/{id}")]
        public IActionResult UsuarioPorId(int id)
        {
            var usuarioExistente = _context.Users
                .Include(u => u.ContatoUsers)
                .Include(u => u.Endereco)
                .FirstOrDefault(u => u.Id == id);

            if (usuarioExistente == null)
            {
                return NotFound();
            }

            var resposta = new
            {
                Message = "Usuário encontrado com sucesso",
                Usuario = usuarioExistente
            };

            return Ok(resposta);
        }
        [Authorize]
        [HttpGet("UsuariosDebitosPorId/{id}")]
        public IActionResult UsuarioDebitoPorId(int id)
        {
            var usuarioExistente = _context.Users
                .Include(user => user.ListaDebito)
                .Include(u => u.ContatoUsers)
                .Include(u => u.Endereco)
                .FirstOrDefault(u => u.Id == id);

            if (usuarioExistente == null)
            {
                return NotFound();
            }

            return Ok(usuarioExistente);
        }
        [Authorize]
        [HttpGet("ListaUsuarios")]
        public IActionResult UsuarioList()
        {
            var user = _context.Users.ToList();
            int quantidadeUsuarios = user.Count;

            if (user == null || user.Count == 0)
            {
                return NotFound("Nenhum usuário encontrado na lista.");
            }

            var usuariosPorNivel = _context.Users
        .GroupBy(u => u.Nivil) // Agrupar os usuários pelo campo Nivel
        .Select(g => new
        {
            Nivil = g.Key,
            Quantidade = g.Count()
        })
       .ToList();


            var ContatoUser = _context.Users
               .Include(user => user.ContatoUsers)
                .ToList();

            var EnderecoUser = _context.Users
                .Include(User => User.Endereco)
                .ToList();

            var usuariosProjetados = user.Select(u => new
            {
                Id = u.Id,
                Nome = u.Nome,
                Nivil = u.Nivil,
                Estatus = u.Estatus,
                ContatoUser = u.ContatoUsers,
                Endereco = u.Endereco
            }).ToList();


            var resposta = new
            {
                Message = "Todos os Usuários do Sistema",
                QuantidadeUsuarios = quantidadeUsuarios,
                usuariosPorNivel = usuariosPorNivel,
                Usuarios = usuariosProjetados,
                // Contatos = Contatos
            };
            return Ok(resposta);
        }
        [Authorize]
        [HttpGet("ListaUsuariosComDebitos")]
        public IActionResult ListaDebitos()
        {
            var usuariosComDebitos = _context.Users
                .Include(user => user.ListaDebito)
                .Include(user => user.ContatoUsers)
                .Include(user => user.Endereco)
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
                    Endereco = usuario.Endereco,

                }).ToList()
            };

            return Ok(resultado);
        }
        [Authorize]
        [HttpPost("CriarUsuarioCompleto")]
        public IActionResult CriarUsuarioEContatoCompleto([FromBody] CriarUsuarioEContatoDto dto)
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

            var novoEndereco = new Endereco
            {
                Cep = dto.Cep,
                Bairro = dto.Bairro,
                Rua = dto.Rua,
                Numero = dto.Numero,
                Complemento = dto.Complemento,
                Moradia = dto.Moradia

            };

            // Estabeleça o relacionamento 1:1 entre User e ContatoUser
            novoUsuario.ContatoUsers = novoContato;
            novoContato.User = novoUsuario;

            // Estabeleça o relacionamento 1:1 entre User e Endereco
            novoUsuario.Endereco = novoEndereco;
            novoEndereco.User = novoUsuario;


            _context.Users.Add(novoUsuario);


            _context.SaveChanges();

            return Ok("Usuário, Contato e Endereço criados com sucesso!");
        }
        [Authorize]
        [HttpPut("AtualizarUsuarioCompleto/{userId}")]
        public IActionResult AtualizarUsuarioEContatoCompleto(int userId, [FromBody] AtualizarUsuarioEContatoDto dto)
        {
            // Consulte o banco de dados para recuperar o usuário, contato e endereço existentes
            var usuarioExistente = _context.Users
                .Include(u => u.ContatoUsers)
                .Include(u => u.Endereco)
                .FirstOrDefault(u => u.Id == userId);

            // Se o usuário não existir, retorne um erro
            if (usuarioExistente == null)
            {
                return NotFound("Usuário não encontrado.");
            }

            // Atualize as informações do usuário com base nos dados fornecidos na requisição
            usuarioExistente.Nome = dto.Nome;
            usuarioExistente.Usuarios = dto.Usuarios;
            usuarioExistente.Senha = dto.Senha;
            usuarioExistente.Nivil = dto.Nivel;
            usuarioExistente.Estatus = dto.Estatus;

            // Verifique se o usuário possui um contato antes de atualizar suas informações
            if (usuarioExistente.ContatoUsers != null)
            {
                usuarioExistente.ContatoUsers.Email = dto.Email;
                usuarioExistente.ContatoUsers.Celular = dto.Celular;
                usuarioExistente.ContatoUsers.CelularAdicional = dto.CelularAdicional;
                usuarioExistente.ContatoUsers.Telefone = dto.Telefone;
                usuarioExistente.ContatoUsers.Instagran = dto.Instagram;
                usuarioExistente.ContatoUsers.Facebook = dto.Facebook;
                usuarioExistente.ContatoUsers.Twiter = dto.Twitter;
            }
            else
            {
                // Caso não exista um ContatoUsers, crie um novo e relacione ao usuário
                usuarioExistente.ContatoUsers = new ContatoUser
                {
                    Email = dto.Email,
                    Celular = dto.Celular,
                    CelularAdicional = dto.CelularAdicional,
                    Telefone = dto.Telefone,
                    Instagran = dto.Instagram,
                    Facebook = dto.Facebook,
                    Twiter = dto.Twitter,
                    UserId = userId // Estabeleça o relacionamento 1:1
                };
            }

            // Verifique se o usuário possui um endereço antes de atualizar suas informações
            if (usuarioExistente.Endereco != null)
            {
                usuarioExistente.Endereco.Cep = dto.Cep;
                usuarioExistente.Endereco.Bairro = dto.Bairro;
                usuarioExistente.Endereco.Rua = dto.Rua;
                usuarioExistente.Endereco.Numero = dto.Numero;
                usuarioExistente.Endereco.Complemento = dto.Complemento;
                usuarioExistente.Endereco.Moradia = dto.Moradia;
            }
            else
            {
                // Caso não exista um Endereco, crie um novo e relacione ao usuário
                usuarioExistente.Endereco = new Endereco
                {
                    Cep = dto.Cep,
                    Bairro = dto.Bairro,
                    Rua = dto.Rua,
                    Numero = dto.Numero,
                    Complemento = dto.Complemento,
                    Moradia = dto.Moradia,
                    UserId = userId // Estabeleça o relacionamento 1:1
                };
            }

            // Salve as mudanças no banco de dados
            _context.SaveChanges();

            return Ok("Usuário, Contato e Endereço atualizados com sucesso!");
        }
        [Authorize]
        [HttpDelete("ExcluirUsuarioEContatoCompleto/{userId}")]
        public IActionResult ExcluirUsuarioEContatoCompleto(int userId)
        {
            // Consulte o banco de dados para recuperar o usuário, contato e endereço existentes
            var usuarioExistente = _context.Users
                .Include(u => u.ContatoUsers)
                .Include(u => u.Endereco)
                .FirstOrDefault(u => u.Id == userId);

            // Se o usuário não existir, retorne um erro
            if (usuarioExistente == null)
            {
                return NotFound("Usuário não encontrado.");
            }

            // Remova o usuário do contexto
            _context.Users.Remove(usuarioExistente);

            // Salve as mudanças no banco de dados
            _context.SaveChanges();

            return Ok("Usuário, Contato e Endereço excluídos com sucesso!");
        }

    }
}