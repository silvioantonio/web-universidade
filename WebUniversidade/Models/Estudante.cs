using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebUniversidade.Models
{
    public class Estudante
    {
        public int Id { get; set; }

        [StringLength(50, MinimumLength = 3)]
        [Required]
        public string Nome { get; set; }

        [StringLength(50, MinimumLength = 3)]
        [Required]
        public string Sobrenome { get; set; }

        [Display(Name = "Data de Registro")]
        [DataType(DataType.Date)]
        [Column("DataEntrada")]
        [DisplayFormat(DataFormatString ="{0:dd-MM-yyyy}",ApplyFormatInEditMode =true)]
        public DateTime EnrollmentDate { get; set; }

        public ICollection<CursoEstudante> CursoEstudantes { get; set; }

        [Display(Name ="Nome Completo")]
        public string NomeCompleto
        {
            get { return Nome + ", " + Sobrenome; }
        }
    }
}
