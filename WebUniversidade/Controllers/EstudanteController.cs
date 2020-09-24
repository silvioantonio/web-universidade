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

    }
}
