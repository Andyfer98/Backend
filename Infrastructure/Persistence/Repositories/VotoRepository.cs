using Domain.Base;
using Domain.Models;
using Domain.Repositories;
using Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class VotoRepository : IVotoRepository
    {

        private readonly ApplicationDbContext _context;

        public VotoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Response<string>> Add(VotoModel voto)
        {
            var votoExistente = await _context.Votos
                                              .Where(v => v.EstudianteId == voto.EstudianteId)
                                              .FirstOrDefaultAsync();

            if (votoExistente != null)
            {
                return new Response<string> { Status = 400, Data = "El estudiante ya ha votado por una candidata." };
            }

            _context.Votos.Add(voto);
            await _context.SaveChangesAsync();
            return new Response<string> { Status = 201, Data = "Voto registrado con éxito" };
        }

        public async Task<Response<string>> Delete(int id)
        {
            var voto = await _context.Votos.FindAsync(id);

            if (voto == null)
            {
                return new Response<string> { Status = 404, Data = "Voto no encontrado" };
            }

            _context.Votos.Remove(voto);
            await _context.SaveChangesAsync();

            return new Response<string> { Status = 200, Data = "Eliminación exitosa" };
        }

        public async Task<Response<List<VotoModel>>> GetByIdCandidata(int id)
        {
            var votos = await _context.Votos
                                      .Include(c => c.Candidata)
                                      .Include(c => c.Estudiante)
                                      .Where(v => v.CandidataId == id)
                                      .ToListAsync();

            if (votos == null || !votos.Any())
            {
                return new Response<List<VotoModel>> { Status = 404, Data = new List<VotoModel>() };
            }

            return new Response<List<VotoModel>> { Status = 200, Data = votos };
        }

        public async Task<Response<VotoModel>> GetByIdEstudiante(int id)
        {
            var voto = await _context.Votos
                                      .Include(c => c.Candidata)
                                      .Include(c => c.Estudiante)
                                      .Where(v => v.EstudianteId == id)
                                      .FirstOrDefaultAsync();

            if (voto == null)
            {
                return new Response<VotoModel> { Status = 404, Data = new VotoModel() };
            }

            return new Response<VotoModel> { Status = 200, Data = voto };
        }
    }
}
