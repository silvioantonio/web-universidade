using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebUniversidade.Models
{
    public class Estudante
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string Nome { get; set; }

        [StringLength(50)]
        public string Sobrenome { get; set; }

        [DataType(DataType.Date)]
        [Column("DataEntrada")]
        [DisplayFormat(DataFormatString ="{0:dd-MM-yyyy}",ApplyFormatInEditMode =true)]
        public DateTime EnrollmentDate { get; set; }
        public ICollection<CursoEstudante> CursoEstudantes { get; set; }
    }
}
