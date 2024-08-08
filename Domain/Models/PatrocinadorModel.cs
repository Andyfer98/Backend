using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    [Table("patrocinador")]
    public class PatrocinadorModel
    {
        public int Id { get; set; }

        public string? Empresa { get; set; }

        public string? Website { get; set; }

        public string? UrlLogo { get; set; }

        public string? Usuario { get; set; }

        public string? Contrasena { get; set; }

        public virtual ICollection<PremioModel> Premios { get; set; } = new List<PremioModel>();

    }
}
