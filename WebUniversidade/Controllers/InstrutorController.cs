﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebUniversidade.Data;
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
                        .ThenInclude(i => i.CursoEstudantes)
                            .ThenInclude(i => i.Estudante)
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
                viewModel.CursoEstudantes = viewModel.Cursos.Where(x => x.CursoId == cursoID).SingleOrDefault().CursoEstudantes;
            }
            return View(viewModel);
        }

    }
}
