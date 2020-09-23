using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebUniversidade.Models;

namespace WebUniversidade.Data
{
    public static class InicializadorDb
    {

        public static void Inicializador(Contexto contexto)
        {
            contexto.Database.EnsureCreated();

            if (contexto.Estudantes.Any())
            {
                return; // O banco esta populado
            }

            var estudantes = new Estudante[] 
            {
                new Estudante{ Nome="Jackson", Sobrenome="Silva", EnrollmentDate=DateTime.Parse("2010-05-01")},
                new Estudante{ Nome="Maria", Sobrenome="das dores", EnrollmentDate=DateTime.Parse("2010-05-01")},
                new Estudante{ Nome="Florisvaldo", Sobrenome="Carvalho", EnrollmentDate=DateTime.Parse("2010-05-01")},
                new Estudante{ Nome="Eduardo", Sobrenome="Timber", EnrollmentDate=DateTime.Parse("2010-05-01")},
                new Estudante{ Nome="Rafaela", Sobrenome="Gomex", EnrollmentDate=DateTime.Parse("2010-05-01")},
                new Estudante{ Nome="Juliao", Sobrenome="Tiao", EnrollmentDate=DateTime.Parse("2010-05-01")},
                new Estudante{ Nome="Cauê", Sobrenome="M.", EnrollmentDate=DateTime.Parse("2010-05-01")},
                new Estudante{ Nome="Paula", Sobrenome="Mattos", EnrollmentDate=DateTime.Parse("2010-05-01")},
            };
            foreach (var estudante in estudantes)
            {
                contexto.Estudantes.Add(estudante);
            }


            var cursos = new Curso[]
            {
                new Curso{CursoId=1001, Titulo="Logica de Programação 1", Creditos=2},
                new Curso{CursoId=1002, Titulo="Desenvolvimento Pessoal", Creditos=1},
                new Curso{CursoId=1003, Titulo="Matematica 2", Creditos=4},
                new Curso{CursoId=1004, Titulo="Fisica 1", Creditos=3},
                new Curso{CursoId=1005, Titulo="Desenvolvimento de Software 1", Creditos=3},
                new Curso{CursoId=1006, Titulo="Java para iniciantes", Creditos=3},
            };
            foreach (var curso in cursos)
            {
                contexto.Cursos.Add(curso);
            }


            var cursoEstudante = new CursoEstudante[]
            {
                new CursoEstudante{EstudanteId=1, CursoID=1001, Grade=Grades.A},
                new CursoEstudante{EstudanteId=1, CursoID=1002, Grade=Grades.A},
                new CursoEstudante{EstudanteId=2, CursoID=1001, Grade=Grades.A},
                new CursoEstudante{EstudanteId=2, CursoID=1002, Grade=Grades.A},
                new CursoEstudante{EstudanteId=3, CursoID=1004, Grade=Grades.B},
                new CursoEstudante{EstudanteId=3, CursoID=1005, Grade=Grades.B},
                new CursoEstudante{EstudanteId=3, CursoID=1001, Grade=Grades.A},
                new CursoEstudante{EstudanteId=4, CursoID=1006, Grade=Grades.C},
                new CursoEstudante{EstudanteId=4, CursoID=1005, Grade=Grades.A},
                new CursoEstudante{EstudanteId=5, CursoID=1001, Grade=Grades.C},
                new CursoEstudante{EstudanteId=6, CursoID=1003, Grade=Grades.B},
                new CursoEstudante{EstudanteId=6, CursoID=1004, Grade=Grades.C},
                new CursoEstudante{EstudanteId=7, CursoID=1003, Grade=Grades.D},
                new CursoEstudante{EstudanteId=8, CursoID=1003, Grade=Grades.E},
            };
            foreach (var item in cursoEstudante)
            {
                contexto.CursoEstudantes.Add(item);
            }

            contexto.SaveChanges();

        }

    }
}
