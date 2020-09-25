using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebUniversidade.Models
{
    public class Instrutor
    {
        public int Id { get; set; }

        [Required]
        [StringLength(40, MinimumLength =3)]
        public string Nome { get; set; }

        [Required]
        [StringLength(40, MinimumLength = 3)]
        public string Sobrenome { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Data da Contratacao")]
        public DateTime DataContratacao { get; set; }

        public IList<CursoInstrutor>  CursoInstrutores { get; set; }

        public Escritorio Escritorio { get; set; }

        [Display(Name = "Nome Completo")]
        public string NomeCompleto { get { return Nome + ", " + Sobrenome; } }

    }
}
