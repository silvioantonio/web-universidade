using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebUniversidade.Models
{
    public class Estudante : Pessoa
    {
        [Display(Name = "Data de Registro")]
        [DataType(DataType.Date)]
        [Column("DataEntrada")]
        [DisplayFormat(DataFormatString ="{0:dd-MM-yyyy}",ApplyFormatInEditMode =true)]
        public DateTime EnrollmentDate { get; set; }

        public ICollection<CursoEstudante> CursoEstudantes { get; set; }

    }
}
