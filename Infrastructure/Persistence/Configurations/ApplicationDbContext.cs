using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Configurations
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() { }

        public ApplicationDbContext(DbContextOptions options) : base(options) { }


        public DbSet<EstudianteModel> Estudiantes { get; set; }

        public DbSet<CandidataModel> Candidatas { get; set; }

        public DbSet<PatrocinadorModel> Patrocinadores { get; set; }

        public DbSet<PremioModel> Premios { get; set; }

        public DbSet<ComentarioModel> Comentarios { get; set; }

        public DbSet<VotoModel> Votos { get; set; }

        public DbSet<AnuncioModel> Anuncios { get; set; }

        public DbSet<ArticuloModel> Articulos { get; set; }

    }
}
