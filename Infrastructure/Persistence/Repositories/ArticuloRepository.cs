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
    public class ArticuloRepository: IArticuloRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;


        public ArticuloRepository(ApplicationDbContext context, IConfiguration Configuration)
        {
            _context = context;
            _configuration = Configuration;
        }

        public async Task<Response<ArticuloModel>> GetById(int id)
        {
            var articulo = await _context.Articulos.FindAsync(id);

            if (articulo == null)
            {
                return new Response<ArticuloModel> { Status = 404, Data = new ArticuloModel { } };
            }
            return new Response<ArticuloModel> { Status = 200, Data = articulo };

        }

        public async Task<Response<string>> Add(ArticuloModel articulo)
        {
            _context.Articulos.Add(articulo);
            await _context.SaveChangesAsync();

            return new Response<string> { Status = 201, Data = "Articulo registrado con éxito" };
        }

        public async Task<Response<string>> Delete(int id)
        {
            var articulo = await _context.Articulos.FindAsync(id);

            if (articulo == null)
            {
                return new Response<string> { Status = 404, Data = "articulo no encontrado" };
            }

            _context.Articulos.Remove(articulo);
            await _context.SaveChangesAsync();

            return new Response<string> { Status = 200, Data = "Eliminación exitosa" };
        }

        public async Task<Response<string>> Put(ArticuloModel articulo)
        {
            var articuloExistente = await _context.Articulos
                .FirstOrDefaultAsync(e => e.Id == articulo.Id);

            if (articuloExistente == null)
            {
                return new Response<string> { Status = 404, Data = "Articulo no encontrado" };
            }

            // Verifica si ya existe un articulo con el mismo título
            var articuloConMismoTitulo= await _context.Articulos
                .FirstOrDefaultAsync(e => e.Titulo == articulo.Titulo && e.Id != articulo.Id);

            if (articuloConMismoTitulo != null)
            {
                return new Response<string> { Status = 400, Data = "Articulo ya registrado" };
            }

            _context.Entry(articuloExistente).CurrentValues.SetValues(articulo);
            await _context.SaveChangesAsync();

            return new Response<string> { Status = 200, Data = "Actualizacion éxitosa" };
        }

        public async Task<Response<List<ArticuloModel>>> GetAll()
        {
            var articulos = await _context.Articulos.ToListAsync();
            return new Response<List<ArticuloModel>> { Status = 200, Data = articulos };
        }
    }
}
