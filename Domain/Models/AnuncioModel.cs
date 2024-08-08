using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    [Table("anuncio")]
    public class AnuncioModel
    {
        public int Id { get; set; }
        public string? Titulo { get; set; }

        public string? Descripcion { get; set; }

        public string? TipoAnuncio { get; set; }

        public string? Autor { get; set; }
    }
}

