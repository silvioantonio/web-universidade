using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebUniversidade.Data;

namespace WebUniversidade.Controllers
{
    public class EstudanteController : Controller
    {
        private Contexto Contexto { get; }

        public EstudanteController(Contexto contexto)
        {
            this.Contexto = contexto;
        }

        public async Task<IActionResult> Index()
        {
            return View(await Contexto.Estudantes.ToListAsync());
        }

        public async Task<IActionResult> Detalhes(int? id)
        {
            if (id == null)
                return NotFound(new { message = "Codigo passado não encontrado na base de dados!" });

            var estudante = await Contexto.Estudantes
                .Include(e => e.CursoEstudantes)
                .ThenInclude(e => e.Curso)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (estudante == null)
                return NotFound(new { message = "Estudante não encontrado na base de dados!" });

            return View(estudante);
        }

    }
}
