using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    [Table("premio")]
    public class PremioModel
    {
        public int Id { get; set; }

        public string? Nombre { get; set; }

        public int PatrocinadorId { get; set; }

        public string? Puesto { get; set; }

        public string? Descripcion { get; set; }

        public virtual PatrocinadorModel? Patrocinador { get; set; }

    }
}
