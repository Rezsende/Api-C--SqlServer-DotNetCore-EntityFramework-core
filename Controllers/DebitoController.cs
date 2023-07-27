using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using c_.Context;
using c_.DTO;
using c_.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace c_.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DebitoController : ControllerBase
    {

        private readonly FarmaContext _context;

        public DebitoController(FarmaContext farmaContext)
        {
            _context = farmaContext;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<ListaDebito>>> GetDebitosPorId(int userId)
        {
            var debitos = await _context.ListaDebitos.Where(x => x.UserId == userId).ToListAsync();
            return debitos;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<List<ListaDebito>>> PostDebitosPorId(ListaDebitoPostDTO request)
        {
            var user = await _context.User.FindAsync(request.UserId);
            if (user == null)
                return NotFound();

            var novaListadeDebito = new ListaDebito
            {
                Descricao = request.Descricao,
                Messagem = request.Messagem,
                Valor = request.Valor,
                User = user
            };
            _context.ListaDebitos.Add(novaListadeDebito);
            await _context.SaveChangesAsync();
            return await GetDebitosPorId(novaListadeDebito.UserId);
        }


        [Authorize]
        [HttpGet("todosComdebitos")]

        public async Task<ActionResult<List<ListaDebitoPostDTO>>> GetTodosDebitos()
        {
            var debitos = await _context.ListaDebitos
                .Include(d => d.User) // Carregar o usuário associado ao débito
                .ToListAsync();

            var debitosComUsuarioDTO = debitos.Select(d => new ListaDebitoPostDTO
            {
                DebitoId = d.Id,
                Descricao = d.Descricao,
                Messagem = d.Messagem,
                Valor = d.Valor,
                UserId = d.UserId,
                // NomeUsuario = d.User != null ? d.User.Nome : "Usuário não encontrado" // Nome do usuário ou uma mensagem padrão se o usuário for nulo
            }).ToList();

            return debitosComUsuarioDTO;
        }
        
        
        [Authorize]
        [HttpDelete("{debitoId}")]
        public async Task<IActionResult> ExcluirDebitoPorId(int debitoId)
        {
            var debito = await _context.ListaDebitos.FindAsync(debitoId);

            if (debito == null)
            {
                return NotFound("Débito não encontrado.");
            }

            _context.ListaDebitos.Remove(debito);
            await _context.SaveChangesAsync();

            return Ok("Débito excluído com sucesso.");
        }



    }
}