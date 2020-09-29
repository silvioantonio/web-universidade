using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebUniversidade.Models
{
    public class Curso
    {
        //Basicamente, esse atributo permite que você insira a chave primária do curso, em vez de fazer com que ela seja gerada pelo banco de dados.
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CursoId { get; set; }

        [StringLength(40, MinimumLength = 3)]
        public string Titulo { get; set; }

        [Range(0, 5)]
        public int Creditos { get; set; }
        public int DepartamentoId { get; set; }
        public Departamento Departamento { get; set; }
        public ICollection<CursoEstudante> CursoEstudantes { get; set; }
        public ICollection<CursoInstrutor> CursoInstrutores { get; set; }
    }
}