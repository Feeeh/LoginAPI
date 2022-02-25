using LoginAPI.Data;
using LoginAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoginAPI.Controllers
{
    public class UsuarioAPIController : Controller
    {
        private readonly AppDbContext _context;

        public UsuarioAPIController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("Api/GetUsuario")]
        public async Task<ActionResult<List<Usuario>>> GetUsuario()
        {
            return Ok(await _context.Usuario.ToListAsync());
        }

        [HttpGet("Api/GetUsuario/{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario(int id)
        {
            var usuario = await _context.Usuario.FindAsync(id);
            if (usuario == null) return BadRequest("Usuario não encontrado");
            return Ok(usuario);
        }

        [HttpPost("Api/AddUsuario")]
        public async Task<ActionResult<List<Usuario>>> AddUsuario([FromBody] Usuario usuario)
        {
            await _context.Usuario.AddAsync(usuario);
            await _context.SaveChangesAsync();
            return Ok(await _context.Usuario.ToListAsync());
        }

        [HttpPut("Api/UpdateUsuario")]
        public async Task<ActionResult<List<Usuario>>> UpdateUsuario([FromBody] Usuario request)
        {
            var usuario = await _context.Usuario.FindAsync(request.Id);
            if (usuario == null) return BadRequest("Usuario não encontrado");

            usuario.User = request.User;
            usuario.Password = request.Password;
            usuario.Email = request.Email;
            usuario.DataDeNascimento = request.DataDeNascimento;
            usuario.Termos = request.Termos;

            await _context.SaveChangesAsync();

            return Ok(await _context.Usuario.ToListAsync());
        }

        [HttpDelete("Api/DeleteUsuario/{id}")]
        public async Task<ActionResult<Usuario>> DeleteUsuario(int id)
        {
            var usuario = await _context.Usuario.FindAsync(id);
            if (usuario == null) return BadRequest("Usuario não encontrado");

            _context.Usuario.Remove(usuario);
            await _context.SaveChangesAsync();

            return Ok(await _context.Usuario.ToListAsync());
        }
    }
}
