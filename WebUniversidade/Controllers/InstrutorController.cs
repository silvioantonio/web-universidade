using Microsoft.AspNetCore.Components.Forms;
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
            var viewModel = new InstrutorIndexData
            {
                Instrutores = await Contexto.Instrutores
                                                    .Include(i => i.Escritorio)
                                                    .Include(i => i.CursoInstrutores)
                                                        .ThenInclude(i => i.Curso)
                                                            .ThenInclude(i => i.Departamento)
                                                    .OrderBy(i => i.Sobrenome)
                                                    .ToListAsync()
            };


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

            var instrutor = await Contexto.Instrutores
                .Include(i => i.Escritorio)
                .Include(i => i.CursoInstrutores)
                .ThenInclude(i => i.Curso).AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if (instrutor == null)
            {
                return NotFound();
            }

            DataAssinaturaCurso(instrutor);
            return View(instrutor);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int? id, string[] cursosSelecionados)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instrutorAtualizado = await Contexto.Instrutores
                .Include(i => i.Escritorio)
                .Include(i => i.CursoInstrutores)
                    .ThenInclude(i => i.Curso)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (await TryUpdateModelAsync(instrutorAtualizado, "", i => i.Nome, i => i.Sobrenome, i => i.DataContratacao, i => i.Escritorio))
            {
                if (string.IsNullOrWhiteSpace(instrutorAtualizado.Escritorio?.Localizacao))
                {
                    instrutorAtualizado.Escritorio = null;
                }
                
                AtualizarCursosInstrutor(cursosSelecionados, instrutorAtualizado);

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

            AtualizarCursosInstrutor(cursosSelecionados, instrutorAtualizado);
            DataAssinaturaCurso(instrutorAtualizado);

            return View(instrutorAtualizado);

        }

        [HttpDelete]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Deletar(int id)
        {
            var instrutor = await Contexto.Instrutores.Include(i => i.CursoInstrutores).SingleAsync();
            var departamentos = await Contexto.Departamentos.Where(d => d.InstrutorId == id).ToListAsync();

            departamentos.ForEach(d => d.InstrutorId = null);

            Contexto.Instrutores.Remove(instrutor);

            await Contexto.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private void AtualizarCursosInstrutor(string[] cursosSelecionados, Instrutor instrutorAtualizado)
        {
            if (cursosSelecionados == null)
            {
                instrutorAtualizado.CursoInstrutores = new List<CursoInstrutor>();
                return;
            }

            var cursosSelct = new HashSet<string>(cursosSelecionados);
            var cursosInstrutores = new HashSet<int>(instrutorAtualizado.CursoInstrutores.Select(c => c.Curso.CursoId));

            foreach (var curso in Contexto.Cursos)
            {
                if (cursosSelct.Contains(curso.CursoId.ToString()))
                {
                    if (!cursosInstrutores.Contains(curso.CursoId))
                    {
                        instrutorAtualizado.CursoInstrutores.Add(new CursoInstrutor { InstrutorId = instrutorAtualizado.Id, CursoId = curso.CursoId });
                    }
                }
                else
                {
                    if (cursosInstrutores.Contains(curso.CursoId))
                    {
                        CursoInstrutor cursoRemover = instrutorAtualizado.CursoInstrutores.FirstOrDefault(x => x.CursoId == curso.CursoId);
                        Contexto.Remove(cursoRemover);
                    }
                }
            }

        }

        private void DataAssinaturaCurso(Instrutor instrutorAtualizado)
        {
            var todosCursos = Contexto.Cursos;
            var cursosIntrutores = new HashSet<int>(instrutorAtualizado.CursoInstrutores.Select(c => c.CursoId));
            var viewModel = new List<DataAssinaturaCurso>();

            foreach (var curso in todosCursos)
            {
                viewModel.Add(new DataAssinaturaCurso
                {
                    CursoId = curso.CursoId,
                    Titulo = curso.Titulo,
                    Assinatura = cursosIntrutores.Contains(curso.CursoId)
                });
            }
            ViewData["Cursos"] = viewModel;
        }
    }
}
