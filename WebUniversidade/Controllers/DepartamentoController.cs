using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using WebUniversidade.Data;
using WebUniversidade.Models;

namespace WebUniversidade.Controllers
{
    public class DepartamentoController : Controller
    {

        private Contexto Contexto { get; }

        public DepartamentoController(Contexto contexto)
        {
            this.Contexto = contexto;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var departamentos = Contexto.Departamentos
                .Include(c => c.Administrador)
                .AsNoTracking();
            return View(await departamentos.ToListAsync());
        }

        public async Task<IActionResult> Detalhes(int? id)
        {
            if (id == null)
            {
                return NotFound(new { message = "Codigo nao encontrado!!" });
            }

            //var departamento = await Contexto.Departamentos.AsNoTracking().FirstOrDefaultAsync(x => x.DepartamentoId == id);
            // Apenas outra forma de fazer a consulta utilizando sql
            string query = "SELECT * FROM Departamento WHERE DepartamentoId = {0}";

            Departamento departamento = await Contexto.Departamentos.FromSqlRaw(query, id)
                .Include(a => a.Administrador).AsNoTracking().FirstOrDefaultAsync();

            if (departamento == null)
            {
                return NotFound(new { message = "Departamento nao encontrado!!" });
            }
            return View(departamento);

        }

        public IActionResult Criar()
        {

            ViewData["InstrutorId"] = new SelectList(Contexto.Instrutores, "Id", "NomeCompleto");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Criar([Bind("DepartamentoId, Nome, Orcamento, DataInicio, InstrutorId, RowVersion")] Departamento departamento)
        {
            if (ModelState.IsValid)
            {
                Contexto.Add(departamento);
                await Contexto.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["InstrutorId"] = new SelectList(Contexto.Instrutores, "Id", "NomeCompleto", departamento.InstrutorId);
            return View(departamento);
        }

        public async Task<IActionResult> Editar(int? id)
        {
            if (id == null)
            {
                return NotFound(new { message = "Codigo nao encontrado!!" });
            }

            var departamento = await Contexto.Departamentos.AsNoTracking().FirstOrDefaultAsync(x => x.DepartamentoId == id);

            if (departamento == null)
            {
                return NotFound(new { message = "Departamento nao encontrado!!" });
            }
            ViewData["InstrutorId"] = new SelectList(Contexto.Instrutores, "Id", "NomeCompleto", departamento.InstrutorId);
            return View(departamento);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int? id, byte[] rowVersion)
        {
            if (id == null)
            {
                return NotFound();
            }

            var departamentoAtualizado = await Contexto.Departamentos.Include(i => i.Administrador).FirstOrDefaultAsync(x => x.DepartamentoId == id);

            if (departamentoAtualizado == null)
            {
                var departamentoDeletado = new Departamento();

                await TryUpdateModelAsync(departamentoDeletado);

                ModelState.AddModelError(string.Empty, "Não foi possivel salvar as atualizações. O departamento foi deletado por outro usuario.");

                ViewData["InstrutorId"] = new SelectList(Contexto.Instrutores, "Id", "NomeCompleto", departamentoDeletado.InstrutorId);

                return View(departamentoDeletado);
            }

            Contexto.Entry(departamentoAtualizado).Property("RowVersion").OriginalValue = rowVersion;

            if (await TryUpdateModelAsync<Departamento>(departamentoAtualizado, "", s => s.Nome, s => s.DataInicio, s => s.Orcamento, s => s.InstrutorId))
            {
                try
                {
                    await Contexto.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    var exceptionentry = ex.Entries.Single();
                    var clientes = (Departamento)exceptionentry.Entity;
                    var databaseEntry = exceptionentry.GetDatabaseValues();

                    if (databaseEntry == null)
                    {
                        ModelState.AddModelError(string.Empty, "Nao foi possivel salvar as mudanças. O departamento foi deletado por outro usuario!");
                    }
                    else
                    {
                        var databaseValues = (Departamento)databaseEntry.ToObject();

                        if (databaseValues.Nome != clientes.Nome)
                        {
                            ModelState.AddModelError("Nome", $"Valo atual: {databaseValues.Nome}");
                        }
                        if (databaseValues.Orcamento != clientes.Orcamento)
                        {
                            ModelState.AddModelError("Orcamento", $"Valor atual: {databaseValues.Orcamento:c}");
                        }
                        if (databaseValues.DataInicio != clientes.DataInicio)
                        {
                            ModelState.AddModelError("DataInicio", $"Valor atual: {databaseValues.DataInicio}");
                        }
                        if (databaseValues.InstrutorId != clientes.InstrutorId)
                        {
                            Instrutor databaseIntrutor = await Contexto.Instrutores.FirstOrDefaultAsync(i => i.Id == databaseValues.InstrutorId);
                            ModelState.AddModelError("InstrutorId", $"Valor atual: {databaseIntrutor?.NomeCompleto}");
                        }

                        ModelState.AddModelError(string.Empty, "O dato atual que voce esta tentando editar, foi modificado por outro usuario!");

                        departamentoAtualizado.RowVersion = (byte[])databaseValues.RowVersion;

                        ModelState.Remove("RowVersion");
                    }
                }
            }

            ViewData["InstrutorId"] = new SelectList(Contexto.Instrutores, "Id", "NomeCompleto", departamentoAtualizado.InstrutorId);

            return View(departamentoAtualizado);
        }

        public async Task<IActionResult> Deletar(int? id, bool? concurrencyError)
        {
            if (id == null)
            {
                return NotFound();
            }

            var departamento = await Contexto.Departamentos.Include(d => d.Administrador).AsNoTracking().FirstOrDefaultAsync(x => x.DepartamentoId == id);

            if (departamento == null)
            {
                if (concurrencyError.GetValueOrDefault())
                {
                    return RedirectToAction(nameof(Index));
                }
                return NotFound();
            }

            if (concurrencyError.GetValueOrDefault())
            {
                ViewData["ConcurrencyErrorMessage"] = "O dado que voce tentou deletar foi modificado por outro usuario apos voce selecionar os dados originais";
            }
            return View(departamento);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Deletar(Departamento departamento)
        {
            try
            {
                if (await Contexto.Departamentos.AnyAsync(x => x.DepartamentoId == departamento.DepartamentoId))
                {
                    Contexto.Departamentos.Remove(departamento);
                    await Contexto.SaveChangesAsync();
                }
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                return RedirectToAction(nameof(Deletar), new { concurrencyError = true, id = departamento.DepartamentoId });
            }
        }

    }
}
