using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebUniversidade.Models
{
    public class CursoEstudante
    {
        public int CursoEstudateId { get; set; }
        public int CursoID { get; set; }
        public int EstudanteId { get; set; }

        [DisplayFormat(NullDisplayText = "Sem grade")]
        public Grades? Grade { get; set; }
        public Curso Curso { get; set; }
        public Estudante Estudante { get; set; }
    }
}