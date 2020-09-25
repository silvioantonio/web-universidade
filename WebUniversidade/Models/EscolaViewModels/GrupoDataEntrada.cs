using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebUniversidade.Models.EscolaViewModels
{
    public class GrupoDataEntrada
    {
        [DataType(DataType.Date)]
        public DateTime? DataEntrada { get; set; }
        public int EstudanteCount { get; set; }
    }
}
