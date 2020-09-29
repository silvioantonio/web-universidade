using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebUniversidade.Models
{
    public class Instrutor : Pessoa
    {

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Data da Contratacao")]
        public DateTime DataContratacao { get; set; }

        public IList<CursoInstrutor>  CursoInstrutores { get; set; }

        public Escritorio Escritorio { get; set; }

    }
}
