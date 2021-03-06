﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public async Task<IActionResult> Index(string sortOrder, string searchString, int? pageNumber, string currentFilter)
        {

            ViewData["Sort"] = sortOrder;
            ViewData["ParametroNome"] = string.IsNullOrEmpty(sortOrder) ? "nome_desc" : "";
            ViewData["ParametroData"] = sortOrder == "Data" ? "data_desc" : "Data";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["Filtro"] = searchString;

            var estudantes = Contexto.Estudantes.Select(e => e);

            if (!string.IsNullOrEmpty(searchString))
            {
                estudantes = estudantes.Where(e => e.Nome.Contains(searchString) || e.Sobrenome.Contains(searchString));
            }

            estudantes = sortOrder switch
            {
                "nome_desc" => estudantes.OrderByDescending(e => e.Nome),
                "Data" => estudantes.OrderBy(d => d.EnrollmentDate),
                "data_desc" => estudantes.OrderByDescending(d => d.EnrollmentDate),
                _ => estudantes.OrderBy(e => e.Nome),
            };

            int tamanhoPagina = 4;

            return View(await ListaPaginada<Estudante>.CriarAsync( estudantes.AsNoTracking(), pageNumber ?? 1, tamanhoPagina));
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
        [Route("Deletar/{id:int}")]
        public async Task<IActionResult> Deletar(int? id, bool? errosSalvos = false )
        {
            if (id == null)
                return NotFound("Id do estudante não encontrado!!!");

            var estudante = await Contexto.Estudantes.AsNoTracking().FirstOrDefaultAsync( e => e.Id == id);

            if (estudante == null)
            {
                return NotFound("Estudante nao encontrado!!");
            }

            if (errosSalvos.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] = "Falha a tentar deletar o estudante!";
            }

            return View(estudante);
        }

        [HttpPost]
        [Route("Deletar/{id:int}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Deletar(int id)
        {
            try
            {
                var estudante = new Estudante() { Id = id };
                Contexto.Entry(estudante).State = EntityState.Deleted;
                await Contexto.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException )
            {
                return RedirectToAction(nameof(Deletar), new { id, saveChangesError = true });
            }
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
            catch (DbUpdateException )
            {
                ModelState.AddModelError("", "Não foi possivel salvar os dados, Tente novamente");
            }
            return View(estudante);
        }

        

    }
}
