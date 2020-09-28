using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebUniversidade.Data;
using WebUniversidade.Models;
using WebUniversidade.Models.EscolaViewModels;

namespace WebUniversidade.Controllers
{
    public class InstrutorController : Controller
    {
        private Contexto Contexto { get; }

        public InstrutorController(Contexto contexto)
        {
            this.Contexto = contexto;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int? id, int? cursoID)
        {
            var viewModel = new InstrutorIndexData();

            viewModel.Instrutores = await Contexto.Instrutores
                                                    .Include(i => i.Escritorio)
                                                    .Include(i => i.CursoInstrutores)
                                                        .ThenInclude(i => i.Curso)
                                                            .ThenInclude(i => i.Departamento)
                                                    .OrderBy(i => i.Sobrenome)
                                                    .ToListAsync();
            

            if (id != null)
            {
                ViewData["InstrutorId"] = id.Value;
                var instrutor = viewModel.Instrutores.Where(i => i.Id == id.Value).SingleOrDefault();
                viewModel.Cursos = instrutor.CursoInstrutores.Select(c => c.Curso);
            }

            if (cursoID != null)
            {
                ViewData["CursoId"] = cursoID.Value;

                var cursoSelecionad = viewModel.Cursos.Where(x => x.CursoId == cursoID).Single();
                await Contexto.Entry(cursoSelecionad).Collection(x => x.CursoEstudantes).LoadAsync();
                foreach (CursoEstudante enrollment in cursoSelecionad.CursoEstudantes)
                {
                    await Contexto.Entry(enrollment).Reference(x => x.Estudante).LoadAsync();
                }
                viewModel.CursoEstudantes = cursoSelecionad.CursoEstudantes;

                viewModel.CursoEstudantes = viewModel.Cursos.Where(x => x.CursoId == cursoID).SingleOrDefault().CursoEstudantes;
            }
            return View(viewModel);
        }

        public async Task<IActionResult> Editar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instrutor = await Contexto.Instrutores.Include(i => i.Escritorio).AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if (instrutor == null)
            {
                return NotFound();
            }

            return View(instrutor);

        }

        [HttpPost, ActionName("Editar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditarPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instrutorAtualizado = await Contexto.Instrutores.Include(i => i.Escritorio).AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if (await TryUpdateModelAsync<Instrutor>(instrutorAtualizado, "", i => i.Nome, i => i.Sobrenome, i => i.DataContratacao, i => i.Escritorio))
            {
                if (String.IsNullOrWhiteSpace(instrutorAtualizado.Escritorio?.Localizacao))
                {
                    instrutorAtualizado.Escritorio = null;
                }
                try
                {
                    await Contexto.SaveChangesAsync();
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "Erro ao tentar atualizar");
                }
                return RedirectToAction(nameof(Index));
            }

            return View(instrutorAtualizado);

        }

    }
}
