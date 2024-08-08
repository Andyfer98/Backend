using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    [Table("estudiante")]
    public class EstudianteModel
    {

        public int Id { get; set; }

        public string? Nombres { get; set; }

        public string? Apellidos { get; set; }

        public string? Semestre { get; set; }

        public string? CorreoInstitucional { get; set; }

        public string? Usuario { get; set; }

        public string? Contrasena { get; set; }

    }
}
