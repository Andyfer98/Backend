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
    public class AnuncioRepository : IAnuncioRepository
    {

        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;


        public AnuncioRepository(ApplicationDbContext context, IConfiguration Configuration)
        {
            _context = context;
            _configuration = Configuration;
        }

        public async Task<Response<AnuncioModel>> GetById(int id)
        {
            var anuncio = await _context.Anuncios.FindAsync(id);

            if (anuncio == null)
            {
                return new Response<AnuncioModel> { Status = 404, Data = new AnuncioModel { } };
            }
            return new Response<AnuncioModel> { Status = 200, Data = anuncio };

        }

        public async Task<Response<string>> Add(AnuncioModel anuncio)
        {
            _context.Anuncios.Add(anuncio);
            await _context.SaveChangesAsync();

            return new Response<string> { Status = 201, Data = "Anuncio registrado con éxito" };
        }

        public async Task<Response<string>> Delete(int id)
        {
            var anuncio = await _context.Anuncios.FindAsync(id);

            if (anuncio == null)
            {
                return new Response<string> { Status = 404, Data = "Anuncio no encontrado" };
            }

            _context.Anuncios.Remove(anuncio);
            await _context.SaveChangesAsync();

            return new Response<string> { Status = 200, Data = "Eliminación exitosa" };
        }

        public async Task<Response<string>> Put(AnuncioModel anuncio)
        {
            var anuncioExistente = await _context.Anuncios
                .FirstOrDefaultAsync(e => e.Id == anuncio.Id);

            if (anuncioExistente == null)
            {
                return new Response<string> { Status = 404, Data = "Anuncio no encontrado" };
            }

            // Verifica si ya existe un anuncio con el mismo título
            var anuncioConMismoTitulo = await _context.Anuncios
                .FirstOrDefaultAsync(e => e.Titulo == anuncio.Titulo && e.Id != anuncio.Id);

            if (anuncioConMismoTitulo != null)
            {
                return new Response<string> { Status = 400, Data = "Anuncio ya registrado" };
            }

            _context.Entry(anuncioExistente).CurrentValues.SetValues(anuncio);
            await _context.SaveChangesAsync();

            return new Response<string> { Status = 200, Data = "Actualizacion éxitosa" };
        }

        public async Task<Response<List<AnuncioModel>>> GetAll()
        {
            var anuncios = await _context.Anuncios.ToListAsync();
            return new Response<List<AnuncioModel>> { Status = 200, Data = anuncios };
        }


    }
}
