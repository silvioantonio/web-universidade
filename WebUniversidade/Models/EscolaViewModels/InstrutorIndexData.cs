using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebUniversidade.Models.EscolaViewModels
{
    public class InstrutorIndexData
    {
        public IEnumerable<Instrutor> Instrutores { get; set; }
        public IEnumerable<Curso> Cursos { get; set; }
        public IEnumerable<CursoEstudante> CursoEstudantes { get; set; }
    }
}
