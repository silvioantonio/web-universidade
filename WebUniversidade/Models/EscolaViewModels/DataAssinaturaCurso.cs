using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebUniversidade.Models.EscolaViewModels
{
    public class DataAssinaturaCurso
    {
        public int CursoId { get; set; }
        public string Titulo { get; set; }
        public bool Assinatura { get; set; }
    }
}
