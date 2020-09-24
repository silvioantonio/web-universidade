using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebUniversidade.Data;
using WebUniversidade.Models;

namespace WebUniversidade.Controllers
{
    public class EstudanteController : Controller
    {
        private Contexto Contexto { get; }

        public EstudanteController(Contexto contexto)
        {
            this.Contexto = contexto;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await Contexto.Estudantes.ToListAsync());
        }

        [HttpGet]
        [Route("Detalhes/{id:int}")]
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

        [HttpGet]
        [Route("Editar/{id:int}")]
        public async Task<IActionResult> Editar(int? id)
        {
            if (id == null)
            {
                return NotFound(new { message = "Codigo do estudante nao cadastrado no banco de dados" });
            }
            var estudante = await Contexto.Estudantes.FindAsync(id);

            if (estudante == null)
            {
                return NotFound(new { message = "Estudante nao cadastrado no banco de dados" });
            }
            return View(estudante);
        }

        [HttpPost]
        [Route("Editar/{id:int}")]
        public async Task<IActionResult> Editar(int? id, [Bind("Id, EnrollmentDate, Nome, Sobrenome")] Estudante estudante)
        {
            if (id != estudante.Id)
                return NotFound(new { message = "Id do estudante não esta na base de dados!" });

            if (ModelState.IsValid)
            {
                try
                {
                    Contexto.Update(estudante);
                    await Contexto.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "Nao foi possivel salvar os dados");
                }
            }
            return View(estudante);
        }

        [HttpGet]
        public IActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Criar(Estudante estudante)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Contexto.Add(estudante);
                    await Contexto.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException db)
            {
                ModelState.AddModelError("", "Não foi possivel salvar os dados, Tente novamente");
            }
            return View(estudante);
        }

        

    }
}
