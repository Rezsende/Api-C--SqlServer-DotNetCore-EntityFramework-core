using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using c_.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace c_.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserPlanosController : ControllerBase
    {
        private readonly FarmaContext _context;

        public UserPlanosController(FarmaContext farmaContext)
        {
            _context = farmaContext;
        }

        [HttpGet("ListaUsersPlanosCompleto")]
        public IActionResult ListaUsersPlanos()
        {
            var listaUsersPlanos = _context.UsersPlanos
                .Include(up => up.User)
                    .ThenInclude(u => u.ContatoUsers)
                 .Include(up => up.User)
                    .ThenInclude(u => u.Endereco)
                .Include(up => up.User)
                    .ThenInclude(u => u.ListaDebito)

                .Include(up => up.planos)
                .ToList();

            if (listaUsersPlanos == null || listaUsersPlanos.Count == 0)
            {
                return NotFound("Nenhum UsersPlanos encontrado.");
            }

            var resposta = new
            {
                Message = "Lista de Usuarios e Planos",
                UsersPlanos = listaUsersPlanos
                //para dto
                // UsersPlanos = listaUsersPlanos.Select(up => new UserPlanosDTO
                // {
                //     Nome = up.User.Nome,
                //     Estatus = up.User.Estatus
                // }).ToList()


            };

            return Ok(resposta);
        }



    }
}
