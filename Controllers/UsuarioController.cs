using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using c_.Context;
using c_.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace c_.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {   
        private readonly FarmaContext _context;

        public UsuarioController(FarmaContext farmaContext)
        {
            _context = farmaContext;
        }

        [HttpPost]
        public IActionResult Create([FromForm] Usuario usuario)
        {
            if (usuario.PhotoFile != null && usuario.PhotoFile.Length > 0)
            {
                var filePath = Path.Combine("Storage", usuario.PhotoFile.FileName);
                using Stream fileStream = new FileStream(filePath, FileMode.Create);
                usuario.PhotoFile.CopyTo(fileStream);
                usuario.PhotoFileName = usuario.PhotoFile.FileName; 
            }
            else
            {
                usuario.PhotoFileName = "";
            }

            _context.Add(usuario);
            _context.SaveChanges();
            return Ok(usuario);
        }


        [HttpGet("{id}")]
        public IActionResult UsuarioPorId(int id)
        {
            var usuarioId = _context.usuarios.Find(id);
            if (usuarioId == null)
                return NotFound();
            return Ok(usuarioId);
        }

        [HttpGet("obeterpornome")]
        public IActionResult UsuarioPorNome(string nome)
        {
            var usuario = _context.usuarios.Where(x => x.Nome.Contains(nome));

            return Ok(usuario);
        }

       [HttpGet("Todos")]
       public IActionResult UsuarioList()
        {
            var usuarios = _context.usuarios.ToList();
            int quantidadeUsuarios = usuarios.Count;

            if (usuarios == null || usuarios.Count == 0)
            {
                return NotFound("Nenhum usuário encontrado na lista.");
            }

            var usuariosPorNivel = _context.usuarios
        .GroupBy(u => u.Niviel) // Agrupar os usuários pelo campo Nivel
        .Select(g => new
        {
            Nivel = g.Key,
            Quantidade = g.Count()
        })
        .ToList();

            var resposta = new
            {
                Message = "Todos os Usuários do Sistema",
                QuantidadeUsuarios = quantidadeUsuarios,
                usuariosPorNivel = usuariosPorNivel,
                Usuarios = usuarios
            };


            return Ok(resposta);
        }


        [HttpPut("{id}")]
        public IActionResult AtualizarUsuario(int id, Usuario usuario)
        {
            var usuarioBanco = _context.usuarios.Find(id);
            if (usuarioBanco == null)
                return NotFound();
            usuarioBanco.Nome = usuario.Nome;
            usuarioBanco.CPF = usuario.CPF;
            usuarioBanco.Telefone = usuario.Telefone;
            usuarioBanco.Cargo = usuario.Cargo;
            usuarioBanco.Niviel = usuario.Niviel;


            _context.usuarios.Update(usuarioBanco);
            _context.SaveChanges();
            return Ok(usuarioBanco);
        }
        
        
        [HttpDelete("{id}")]
        public IActionResult DeletarUsuario(int id)
        {
            var usuarioBanco = _context.usuarios.Find(id);
            if (usuarioBanco == null)
                return NotFound();
            _context.usuarios.Remove(usuarioBanco);
            _context.SaveChanges();
            return NoContent();
        }


    }
}