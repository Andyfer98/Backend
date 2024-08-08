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
    public class CandidataRepository : ICandidataRepository
    {

        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public CandidataRepository(ApplicationDbContext context, IConfiguration Configuration)
        {
            _context = context;
            _configuration = Configuration;
        }

        public async Task<Response<CandidataModel>> GetById(int id)
        {
            var candidata = await _context.Candidatas.FindAsync(id);

            if (candidata == null)
            {
                return new Response<CandidataModel> { Status = 404, Data = new CandidataModel { } };
            }

            return new Response<CandidataModel> { Status = 200, Data = candidata };
        }


        public async Task<Response<string>> Add(CandidataModel candidata)
        {

            var candidataExistente = await _context.Candidatas
                .FirstOrDefaultAsync(c => c.Usuario == candidata.Usuario);

            if (candidataExistente != null)
            {
                return new Response<string> { Status = 400, Data = "Usuario ya registrado en otra candidata" };
            }


            _context.Candidatas.Add(candidata);
            await _context.SaveChangesAsync();

            return new Response<string> { Status = 201, Data = "Registro éxitoso" };
        }


        public async Task<Response<string>> Update(CandidataModel candidata)
        {

            var candidataExistente = await _context.Candidatas
                .FirstOrDefaultAsync(c => c.Id == candidata.Id);

            if (candidataExistente is null)
            {
                return new Response<string> { Status = 400, Data = "Candidata no existe" };
            }

            var usuarioExistente = await _context.Candidatas
               .FirstOrDefaultAsync(c => c.Usuario == candidata.Usuario && c.Id != candidata.Id);

            if (usuarioExistente != null)
            {
                return new Response<string> { Status = 400, Data = "Usuario ya registrado en otra candidata" };
            }

            _context.Entry(candidataExistente).CurrentValues.SetValues(candidata);
            await _context.SaveChangesAsync();

            return new Response<string> { Status = 200, Data = "Actualización éxitoso" };
        }


        public async Task<Response<string>> Delete(int id)
        {
            var candidata = await _context.Candidatas.FindAsync(id);

            if (candidata == null)
            {
                return new Response<string> { Status = 404, Data = "Candidata no encontrada" };
            }

            _context.Candidatas.Remove(candidata);
            await _context.SaveChangesAsync();

            return new Response<string> { Status = 200, Data = "Eliminación exitosa" };
        }


        public async Task<Response<List<CandidataModel>>> GetAll()
        {
            var candidatas = await _context.Candidatas.ToListAsync();
            return new Response<List<CandidataModel>> { Status = 200, Data = candidatas };
        }


        public async Task<Response<string>> Authentication(AuthenticationRequest authenticationRequest)
        {
            var candidata = await _context.Candidatas
                .FirstOrDefaultAsync(c => c.Usuario == authenticationRequest.Usuario);

            if (candidata == null || candidata.Contrasena != authenticationRequest.Contrasena)
            {
                return new Response<string> { Status = 401, Data = "Usuario o contraseña incorrectos" };
            }
            var token = CrearToken(candidata);

            return new Response<string> { Status = 200, Data = token };
        }


        private string CrearToken(CandidataModel candidata)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, candidata.Id.ToString()),
                new Claim(ClaimTypes.Name, "candidata"),
                new Claim(ClaimTypes.Name, candidata.NombresCompletos),
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
