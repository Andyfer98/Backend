using Domain.Base;
using Domain.Models;
using Domain.Repositories;
using Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class PatrocinadorRepository : IPatrocinadorRepository
    {

        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public PatrocinadorRepository(ApplicationDbContext context, IConfiguration Configuration)
        {
            _context = context;
            _configuration = Configuration;
        }

        public async Task<Response<string>> Add(PatrocinadorModel patrocinador)
        {
            var patrocinadorExistente = await _context.Patrocinadores
                .FirstOrDefaultAsync(c => c.Empresa == patrocinador.Empresa);

            if (patrocinadorExistente != null)
            {
                return new Response<string> { Status = 400, Data = "Empresa ya registrada" };
            }

            var candidataExistente = await _context.Patrocinadores
                .FirstOrDefaultAsync(c => c.Usuario == patrocinador.Usuario);

            if (candidataExistente != null)
            {
                return new Response<string> { Status = 400, Data = "Usuario ya registrado en otra empresa" };
            }

            _context.Patrocinadores.Add(patrocinador);
            await _context.SaveChangesAsync();

            return new Response<string> { Status = 201, Data = "Registro éxitoso" };
        }


        public async Task<Response<string>> Authentication(AuthenticationRequest authenticationRequest)
        {
            var patrocinador = await _context.Patrocinadores
                .FirstOrDefaultAsync(c => c.Usuario == authenticationRequest.Usuario);

            if (patrocinador == null || patrocinador.Contrasena != authenticationRequest.Contrasena)
            {
                return new Response<string> { Status = 401, Data = "Usuario o contraseña incorrectos" };
            }
            var token = CrearToken(patrocinador);

            return new Response<string> { Status = 200, Data = token };
        }


        public async Task<Response<string>> Delete(int id)
        {
            var patrocinador = await _context.Patrocinadores.FindAsync(id);

            if (patrocinador == null)
            {
                return new Response<string> { Status = 404, Data = "Patrocinador no encontrado" };
            }

            _context.Patrocinadores.Remove(patrocinador);
            await _context.SaveChangesAsync();

            return new Response<string> { Status = 200, Data = "Eliminación exitosa" };
        }


        public async Task<Response<List<PatrocinadorModel>>> GetAll()
        {
            var patrocinadores = await _context.Patrocinadores.ToListAsync();
            return new Response<List<PatrocinadorModel>> { Status = 200, Data = patrocinadores };
        }


        public async Task<Response<PatrocinadorModel>> GetById(int id)
        {
            var patrocinador = await _context.Patrocinadores.FindAsync(id);

            if (patrocinador == null)
            {
                return new Response<PatrocinadorModel> { Status = 404, Data = new PatrocinadorModel { } };
            }

            return new Response<PatrocinadorModel> { Status = 200, Data = patrocinador };
        }


        public async Task<Response<string>> Update(PatrocinadorModel patrocinador)
        {
            var patrocinadorExistente = await _context.Patrocinadores
                .FirstOrDefaultAsync(c => c.Id == patrocinador.Id);

            if (patrocinadorExistente is null)
            {
                return new Response<string> { Status = 400, Data = "Patrocinador no existe" };
            }

            var usuarioExistente = await _context.Patrocinadores
               .FirstOrDefaultAsync(c => c.Usuario == patrocinador.Usuario && c.Id != patrocinador.Id);

            if (usuarioExistente != null)
            {
                return new Response<string> { Status = 400, Data = "Usuario ya registrado en otra empresa" };
            }

            _context.Entry(patrocinadorExistente).CurrentValues.SetValues(patrocinador);
            await _context.SaveChangesAsync();

            return new Response<string> { Status = 200, Data = "Actualización éxitoso" };
        }


        private string CrearToken(PatrocinadorModel patrocinador)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, patrocinador.Id.ToString()),
                new Claim(ClaimTypes.Name, "patrocinador"),
                new Claim(ClaimTypes.Name, patrocinador.Empresa),
            };
            var appSettingsToken = _configuration.GetSection("AppSettings:Token").Value;
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appSettingsToken));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}
