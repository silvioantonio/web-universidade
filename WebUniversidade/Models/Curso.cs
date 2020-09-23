using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebUniversidade.Models
{
    public class Curso
    {
        //Basicamente, esse atributo permite que você insira a chave primária do curso, em vez de fazer com que ela seja gerada pelo banco de dados.
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CursoId { get; set; }
        public string Titulo { get; set; }
        public int Creditos { get; set; }

        public ICollection<CursoEstudante> CursoEstudantes { get; set; }
    }
}