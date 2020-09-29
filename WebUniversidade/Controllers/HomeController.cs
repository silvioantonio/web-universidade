using System.Data.Common;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebUniversidade.Data;
using WebUniversidade.Models;
using WebUniversidade.Models.EscolaViewModels;

namespace WebUniversidade.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly Contexto _contexto;

        public HomeController(ILogger<HomeController> logger, Contexto contexto)
        {
            _logger = logger;
            _contexto = contexto;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<ActionResult> Sobre()
        {

            List<GrupoDataEntrada> grupos = new List<GrupoDataEntrada>();
            var connection = _contexto.Database.GetDbConnection();

            try
            {
                await connection.OpenAsync();
                using (var comando = connection.CreateCommand())
                {
                    string query = "SELECT DataEntrada, COUNT(*) AS EstudanteCount " +
                        "FROM Estudante " +
                        "GROUP BY DataEntrada ";

                    comando.CommandText = query;
                    DbDataReader dbDataReader = await comando.ExecuteReaderAsync();

                    if (dbDataReader.HasRows)
                    {
                        while (await dbDataReader.ReadAsync())
                        {
                            var row = new GrupoDataEntrada { DataEntrada = dbDataReader.GetDateTime(0), EstudanteCount = dbDataReader.GetInt32(1) };
                            grupos.Add(row);
                        }
                    }

                    dbDataReader.Dispose();

                }
            }
            finally
            {

                connection.Close();
            }

            return View(grupos);
        }

    }
}
