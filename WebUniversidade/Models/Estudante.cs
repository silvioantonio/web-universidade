using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebUniversidade.Models
{
    public class Estudante
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public ICollection<CursoEstudante> CursoEstudantes { get; set; }
    }
}
