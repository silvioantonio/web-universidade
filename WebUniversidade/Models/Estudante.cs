using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebUniversidade.Models
{
    public class Estudante
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:dd-MM-yyyy}",ApplyFormatInEditMode =true)]
        public DateTime EnrollmentDate { get; set; }
        public ICollection<CursoEstudante> CursoEstudantes { get; set; }
    }
}
