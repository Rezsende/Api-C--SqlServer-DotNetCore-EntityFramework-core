using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using c_.Entities;
using c_.Context;

namespace c_.Repository
{
    public class UsuarioResponse
    {
        public string Message { get; set; }
        public int QuantidadeUsuarios { get; set; }
        public Dictionary<int, int> UsuariosPorNivel { get; set; } 
        public List<Usuario> Usuarios { get; set; }
    }

    public class UsuarioRepository : ControllerBase
    {
        private readonly FarmaContext _context;

        public UsuarioRepository(FarmaContext context)
        {
            _context = context;
        }

        public IActionResult UsuarioList()
        {
            var usuarios = _context.usuarios.ToList();

            if (usuarios == null || usuarios.Count == 0)
            {
                return NotFound("Nenhum usuário encontrado na lista.");
            }

         
            var usuariosPorNivel = usuarios
                .GroupBy(u => u.Niviel) 
                .ToDictionary(g => g.Key, g => g.Count());

            var responseObj = new UsuarioResponse
            {
                Message = "Todos os Usuários do Sistema",
                QuantidadeUsuarios = usuarios.Count,
                UsuariosPorNivel = usuariosPorNivel,
                Usuarios = usuarios
            };

            return Ok(responseObj);
        }
    }
}
