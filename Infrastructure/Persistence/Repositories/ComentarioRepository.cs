using Domain.Base;
using Domain.Models;
using Domain.Repositories;
using Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class ComentarioRepository : IComentarioRepository
    {

        private readonly ApplicationDbContext _context;

        public ComentarioRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Response<string>> Add(ComentarioModel comentario)
        {
            _context.Comentarios.Add(comentario);
            await _context.SaveChangesAsync();

            return new Response<string> { Status = 201, Data = "Comentario registrado con éxito" };
        }

        public async Task<Response<string>> Delete(int id)
        {
            var comentario = await _context.Comentarios.FindAsync(id);

            if (comentario == null)
            {
                return new Response<string> { Status = 404, Data = "Comentario no encontrado" };
            }

            _context.Comentarios.Remove(comentario);
            await _context.SaveChangesAsync();

            return new Response<string> { Status = 200, Data = "Eliminación exitosa" };
        }

        public async Task<Response<List<ComentarioModel>>> GetByIdCandidata(int id)
        {
            var comentarios = await _context.Comentarios
                                    .Include(c => c.Estudiante)
                                    .Include(c => c.Candidata)
                                    .Where(c => c.CandidataId == id)
                                    .ToListAsync();

            if (comentarios == null || !comentarios.Any())
            {
                return new Response<List<ComentarioModel>> { Status = 404, Data = new List<ComentarioModel>() };
            }

            return new Response<List<ComentarioModel>> { Status = 200, Data = comentarios };
        }

        public async Task<Response<List<ComentarioModel>>> GetByIdEstudiante(int id)
        {
            var comentarios = await _context.Comentarios
                                    .Include(c => c.Estudiante)
                                    .Include(c => c.Candidata)
                                    .Where(c => c.EstudianteId == id)
                                    .ToListAsync();

            if (comentarios == null || !comentarios.Any())
            {
                return new Response<List<ComentarioModel>> { Status = 404, Data = new List<ComentarioModel>() };
            }

            return new Response<List<ComentarioModel>> { Status = 200, Data = comentarios };
        }

    }
}
