using Domain.Base;
using Domain.Models;
using Domain.Repositories;
using Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class EstudianteRepository : IEstudianteRepository
    {

        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public EstudianteRepository(ApplicationDbContext context, IConfiguration Configuration)
        {
            _context = context;
            _configuration = Configuration;
        }

        public async Task<Response<EstudianteModel>> GetById(int id)
        {
            var estudiante = await _context.Estudiantes.FindAsync(id);

            if (estudiante == null)
            {
                return new Response<EstudianteModel> { Status = 404, Data = new EstudianteModel { } };
            }

            return new Response<EstudianteModel> { Status = 200, Data = estudiante };
        }


        public async Task<Response<string>> Add(EstudianteModel estudiante)
        {

            var usuarioExistente = await _context.Estudiantes
                .FirstOrDefaultAsync(e => e.Usuario == estudiante.Usuario);

            if (usuarioExistente != null)
            {
                return new Response<string> { Status = 400, Data = "Usuario ya registrado" };
            }

            var correoExistente = await _context.Estudiantes
                .FirstOrDefaultAsync(e => e.CorreoInstitucional == estudiante.CorreoInstitucional);

            if (correoExistente != null)
            {
                return new Response<string> { Status = 400, Data = "Correo institucional ya registrado" };
            }

            _context.Estudiantes.Add(estudiante);
            await _context.SaveChangesAsync();

            return new Response<string> { Status = 201, Data = "Registro éxitoso" };
        }

        public async Task<Response<string>> Put(EstudianteModel estudiante)
        {
            var estudianteExistente = await _context.Estudiantes
                .FirstOrDefaultAsync(e => e.Id == estudiante.Id);

            if (estudianteExistente == null)
            {
                return new Response<string> { Status = 404, Data = "Estudiante no encontrado" };
            }

            var usuarioExistente = await _context.Estudiantes
                .FirstOrDefaultAsync(e => e.Usuario == estudiante.Usuario && e.Id != estudiante.Id);

            if (usuarioExistente != null)
            {
                return new Response<string> { Status = 400, Data = "Usuario ya registrado" };
            }

            var correoExistente = await _context.Estudiantes
                .FirstOrDefaultAsync(e => e.CorreoInstitucional == estudiante.CorreoInstitucional && e.Id != estudiante.Id);

            if (correoExistente != null)
            {
                return new Response<string> { Status = 400, Data = "Correo institucional ya registrado" };
            }

            _context.Entry(estudianteExistente).CurrentValues.SetValues(estudiante);
            await _context.SaveChangesAsync();

            return new Response<string> { Status = 200, Data = "Actualización éxitosa" };
        }

        public async Task<Response<string>> Delete(int id)
        {
            var estudiante = await _context.Estudiantes.FindAsync(id);

            if (estudiante == null)
            {
                return new Response<string> { Status = 404, Data = "Estudiante no encontrado" };
            }

            _context.Estudiantes.Remove(estudiante);
            await _context.SaveChangesAsync();

            return new Response<string> { Status = 200, Data = "Estudiante eliminado correctamente" };
        }


        public async Task<Response<string>> Authentication(AuthenticationRequest authenticationRequest)
        {
            var estudiante = await _context.Estudiantes
                .FirstOrDefaultAsync(e => e.Usuario == authenticationRequest.Usuario);

            if (estudiante == null || estudiante.Contrasena != authenticationRequest.Contrasena)
            {
                return new Response<string> { Status = 401, Data = "Usuario o contraseña incorrectos" };
            }
            var token = CrearToken(estudiante);

            return new Response<string> { Status = 200, Data = token };
        }


        private string CrearToken(EstudianteModel usuario)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Name, "estudiante"),
                new Claim(ClaimTypes.Name, usuario.Nombres + ' ' + usuario.Apellidos),
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
