using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    [Table("articulo")]
    public class ArticuloModel
    {
        public int Id { get; set; }
        public string? Titulo { get; set; }

        public string? Contenido { get; set; }

        public string? Autor { get; set; }

        public DateTime FechaCreacion { get; set; }
    }
}
