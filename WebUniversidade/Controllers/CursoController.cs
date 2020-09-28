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

        public IActionResult Criar()
        {
            DepartamentosPopulares();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Criar([Bind("CursoId, Creditos, DepartamentoId, Titulo")] Curso curso)
        {
            if (ModelState.IsValid)
            {
                Contexto.Add(curso);
                await Contexto.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            DepartamentosPopulares(curso.DepartmentID);
            return View(curso);
        }

        public async Task<IActionResult> Editar(int? id)
        {
            if(id == null)
            {
                return NotFound(new { message = "Codigo nao encontrado!!"});
            }

            var curso = await Contexto.Cursos.AsNoTracking().FirstOrDefaultAsync(x => x.CursoId == id);

            if (curso == null)
            {
                return NotFound(new { message = "Codigo nao encontrado!!" });
            }

            DepartamentosPopulares(curso.DepartmentID);

            return View(curso);

        }

        [HttpPost, ActionName("Editar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditarPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cursoAtualizado = await Contexto.Cursos.FirstOrDefaultAsync(c => c.CursoId == id);

            if (await TryUpdateModelAsync<Curso>(cursoAtualizado, "", c => c.Creditos, c => c.Departamento, c => c.Titulo))
            {
                try
                {
                    await Contexto.SaveChangesAsync();
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "Nao foi possivel salvar os dados!");
                }
                return RedirectToAction(nameof(Index));
            }
           
            DepartamentosPopulares(cursoAtualizado.DepartmentID);
            return View(cursoAtualizado);

        }

        [HttpGet]
        public async Task<IActionResult> Detalhes(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var curso = await Contexto.Cursos.Include(c => c.Departamento).AsNoTracking().FirstOrDefaultAsync(x => x.CursoId == id);

            if (curso == null)
            {
                return NotFound();
            }

            return View(curso);
        }

        public async Task<IActionResult> Deletar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var curso = await Contexto.Cursos.Include(c => c.Departamento).AsNoTracking().FirstOrDefaultAsync(x => x.CursoId == id);

            if (curso == null)
            {
                return NotFound();
            }
            
            return View(curso);
        }

        private void DepartamentosPopulares(object departamentoSelecionado = null)
        {
            var departamentosQuery = from d in Contexto.Departamentos orderby d.Nome select d;
            ViewBag.DepartamentoId = new ListaSelecionada(departamentosQuery.AsNoTracking(), "DepartamentoId", "Nome", departamentoSelecionado);
        }
    }

    internal class ListaSelecionada
    {
        private IQueryable<Departamento> queryables;
        private string v1;
        private string v2;
        private object departamentoSelecionado;

        public ListaSelecionada(IQueryable<Departamento> queryables, string v1, string v2, object departamentoSelecionado)
        {
            this.queryables = queryables;
            this.v1 = v1;
            this.v2 = v2;
            this.departamentoSelecionado = departamentoSelecionado;
        }
    }
}
