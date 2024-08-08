using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    [Table("candidata")]
    public class CandidataModel
    {
        public int Id { get; set; }

        public string? NombresCompletos { get; set; }

        public DateTime FechaNacimiento { get; set; }

        public string? Semestre { get; set; }

        public string? UrlImagen { get; set; }

        public string? Propuesta { get; set; }

        public string? Usuario { get; set; }

        public string? Contrasena { get; set; }

    }
}
