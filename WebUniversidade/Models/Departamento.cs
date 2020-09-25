using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebUniversidade.Models
{
    public class Departamento
    {
        public int DepartamentoId { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string Nome { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public decimal Orcamento { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Data Inicio")]
        public DateTime DataInicio { get; set; }

        public int? InstrutorId { get; set; }
        public Instrutor Administrador { get; set; }
        public ICollection<Curso> Cursos { get; set; }
    }
}