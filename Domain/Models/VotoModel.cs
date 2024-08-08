using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    [Table("voto")]
    public class VotoModel
    {
        public int Id { get; set; }

        public int EstudianteId { get; set; }

        public int CandidataId { get; set; }

        public DateTime FechaVotacion { get; set; }

        public virtual CandidataModel? Candidata { get; set; }

        public virtual EstudianteModel? Estudiante { get; set; }
    }
}
