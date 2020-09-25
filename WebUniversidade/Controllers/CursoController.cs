using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebUniversidade.Data;

namespace WebUniversidade.Controllers
{
    public class CursoController : Controller
    {
        private Contexto Contexto { get; }

        public CursoController(Contexto contexto)
        {
            this.Contexto = contexto;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var cursos = Contexto.Cursos
                .Include(c => c.Departamento)
                .AsNoTracking();
            return View(await cursos.ToListAsync());
        }
    }
}
