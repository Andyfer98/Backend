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
    public class PremioRepository : IPremioRepository
    {

        private readonly ApplicationDbContext _context;

        public PremioRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Response<string>> Add(PremioModel premio)
        {
            _context.Premios.Add(premio);
            await _context.SaveChangesAsync();

            return new Response<string> { Status = 201, Data = "Registro éxitoso" };
        }

        public async Task<Response<string>> Update(PremioModel premio)
        {
            var premioExistente = await _context.Premios.FindAsync(premio.Id);

            if (premioExistente is null)
            {
                return new Response<string> { Status = 400, Data = "Premio no existe" };
            }

            _context.Entry(premioExistente).CurrentValues.SetValues(premio);
            await _context.SaveChangesAsync();

            return new Response<string> { Status = 200, Data = "Actualización éxitoso" };
        }

        public async Task<Response<string>> Delete(int id)
        {
            var premioExistente = await _context.Premios.FindAsync(id);

            if (premioExistente is null)
            {
                return new Response<string> { Status = 400, Data = "Premio no existe" };
            }

            _context.Premios.Remove(premioExistente);
            await _context.SaveChangesAsync();

            return new Response<string> { Status = 200, Data = "Eliminación exitosa" };
        }

        public async Task<Response<List<PremioModel>>> GetAllById(int id)
        {
            var premios = await _context.Premios.Where(p => p.PatrocinadorId == id).ToListAsync();
            return new Response<List<PremioModel>> { Status = 200, Data = premios };
        }

        public async Task<Response<List<PremioModel>>> GetAll()
        {
            var premios = await _context.Premios.ToListAsync();
            return new Response<List<PremioModel>> { Status = 200, Data = premios };
        }

    }
}
