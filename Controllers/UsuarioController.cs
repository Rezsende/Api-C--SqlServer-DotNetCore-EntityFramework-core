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
        public IActionResult Create(Usuario usuario)
        {
            _context.Add(usuario);
            _context.SaveChanges();
            // return Ok(usuario);
            return CreatedAtAction(nameof(UsuarioPorId), new { id = usuario.Id }, usuario);
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
        [HttpGet]
        public IActionResult UsuarioList()
        {
            var usuarios = _context.usuarios.ToList();

            if (usuarios == null || usuarios.Count == 0)
            {
                return NotFound(); // Retorna um status 404 se não houver usuários encontrados.
            }

            return Ok(usuarios);
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
            usuarioBanco.Ativo = usuario.Ativo;

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