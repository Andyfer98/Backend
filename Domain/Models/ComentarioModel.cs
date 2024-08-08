using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    [Table("comentario")]
    public class ComentarioModel
    {
        public int Id { get; set; }

        public string? Comentario { get; set; }

        public int EstudianteId { get; set; }

        public int CandidataId { get; set; }

        public DateTime FechaRegistro { get; set; }

        public virtual EstudianteModel? Estudiante { get; set; }

        public virtual CandidataModel? Candidata { get; set; }


    }
}
