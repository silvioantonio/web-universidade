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
            Console.Write("Entrou");
            if (contexto.Estudantes.Any())
            {
                Console.WriteLine("Saiu");
                return; // O banco esta populado
            }

            var estudantes = new Estudante[] 
            {
                new Estudante { Nome = "Carson",   Sobrenome = "Alexander",EnrollmentDate = DateTime.Parse("2010-09-01") },
                new Estudante { Nome = "Meredith", Sobrenome = "Alonso",EnrollmentDate = DateTime.Parse("2012-09-01") },
                new Estudante { Nome = "Arturo",   Sobrenome = "Anand",EnrollmentDate = DateTime.Parse("2013-09-01") },
                new Estudante { Nome = "Gytis",    Sobrenome = "Barzdukas",EnrollmentDate = DateTime.Parse("2012-09-01") },
                new Estudante { Nome = "Yan",      Sobrenome = "Li",EnrollmentDate = DateTime.Parse("2012-09-01") },
                new Estudante { Nome = "Peggy",    Sobrenome = "Justice",EnrollmentDate = DateTime.Parse("2011-09-01") },
                new Estudante { Nome = "Laura",    Sobrenome = "Norman",EnrollmentDate = DateTime.Parse("2013-09-01") },
                new Estudante { Nome = "Nino",     Sobrenome = "Olivetto",EnrollmentDate = DateTime.Parse("2005-09-01") }
            };
            foreach (var estudante in estudantes)
            {
                contexto.Estudantes.Add(estudante);
            }
            contexto.SaveChanges();

            var instrutores = new Instrutor[]
            {
                new Instrutor { Nome = "Kim",     Sobrenome = "Abercrombie",
                    DataContratacao = DateTime.Parse("1995-03-11") },
                new Instrutor { Nome = "Fadi",    Sobrenome = "Fakhouri",
                    DataContratacao = DateTime.Parse("2002-07-06") },
                new Instrutor { Nome = "Roger",   Sobrenome = "Harui",
                    DataContratacao = DateTime.Parse("1998-07-01") },
                new Instrutor { Nome = "Candace", Sobrenome = "Kapoor",
                    DataContratacao = DateTime.Parse("2001-01-15") },
                new Instrutor { Nome = "Roger",   Sobrenome = "Zheng",
                    DataContratacao = DateTime.Parse("2004-02-12") }
            };

            foreach (Instrutor i in instrutores)
            {
                contexto.Instrutor.Add(i);
            }
            contexto.SaveChanges();

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
            contexto.SaveChanges();

            var departments = new Departamento[]
            {
                new Departamento { Nome = "English", Orcamento = 350000, DataInicio = DateTime.Parse("2007-09-01"), InstrutorId = instrutores.Single( i => i.Sobrenome == "Abercrombie").Id },
                new Departamento { Nome = "Mathematics", Orcamento = 100000, DataInicio = DateTime.Parse("2007-09-01"), InstrutorId  = instrutores.Single( i => i.Sobrenome == "Fakhouri").Id },
                new Departamento { Nome = "Engineering", Orcamento = 350000, DataInicio = DateTime.Parse("2007-09-01"), InstrutorId  = instrutores.Single( i => i.Sobrenome == "Harui").Id },
                new Departamento { Nome = "Economics",   Orcamento = 100000, DataInicio = DateTime.Parse("2007-09-01"), InstrutorId  = instrutores.Single( i => i.Sobrenome == "Kapoor").Id }
            };

            foreach (Departamento d in departments)
            {
                contexto.Departamento.Add(d);
            }
            contexto.SaveChanges();

            var courses = new Curso[]
            {
                new Curso {CursoId = 1050, Titulo = "Chemistry", Creditos = 3,
                    DepartmentID = departments.Single( s => s.Nome == "Engineering").DepartamentoId
                },
                new Curso {CursoId = 4022, Titulo = "Microeconomics", Creditos = 3,
                    DepartmentID = departments.Single( s => s.Nome == "Economics").DepartamentoId
                },
                new Curso {CursoId = 4041, Titulo = "Macroeconomics", Creditos = 3,
                    DepartmentID = departments.Single( s => s.Nome == "Economics").DepartamentoId
                },
                new Curso {CursoId = 1045, Titulo = "Calculus",       Creditos = 4,
                    DepartmentID = departments.Single( s => s.Nome == "Mathematics").DepartamentoId
                },
                new Curso {CursoId = 3141, Titulo = "Trigonometry",   Creditos = 4,
                    DepartmentID = departments.Single( s => s.Nome == "Mathematics").DepartamentoId
                },
                new Curso {CursoId = 2021, Titulo = "Composition",    Creditos = 3,
                    DepartmentID = departments.Single( s => s.Nome == "English").DepartamentoId
                },
                new Curso {CursoId = 2042, Titulo = "Literature",     Creditos = 4,
                    DepartmentID = departments.Single( s => s.Nome == "English").DepartamentoId
                },
            };

            foreach (Curso c in courses)
            {
                contexto.Cursos.Add(c);
            }
            contexto.SaveChanges();

            var officeAssignments = new Escritorio[]
            {
                new Escritorio {
                    InstrutorId = instrutores.Single( i => i.Sobrenome == "Fakhouri").Id,
                    Localizacao = "Smith 17" },
                new Escritorio {
                    InstrutorId = instrutores.Single( i => i.Sobrenome == "Harui").Id,
                    Localizacao = "Gowan 27" },
                new Escritorio {
                    InstrutorId = instrutores.Single( i => i.Sobrenome == "Kapoor").Id,
                    Localizacao = "Thompson 304" },
            };

            foreach (Escritorio o in officeAssignments)
            {
                contexto.Escritorio.Add(o);
            }
            contexto.SaveChanges();

            var courseInstructors = new CursoInstrutor[]
            {
                new CursoInstrutor {
                    CursoId = courses.Single(c => c.Titulo == "Chemistry" ).CursoId,
                    InstrutorId = instrutores.Single(i => i.Sobrenome == "Kapoor").Id
                    },
                new CursoInstrutor {
                    CursoId = courses.Single(c => c.Titulo == "Chemistry" ).CursoId,
                    InstrutorId = instrutores.Single(i => i.Sobrenome == "Harui").Id
                    },
                new CursoInstrutor {
                    CursoId = courses.Single(c => c.Titulo == "Microeconomics" ).CursoId,
                    InstrutorId = instrutores.Single(i => i.Sobrenome == "Zheng").Id
                    },
                new CursoInstrutor {
                    CursoId = courses.Single(c => c.Titulo == "Macroeconomics" ).CursoId,
                    InstrutorId = instrutores.Single(i => i.Sobrenome == "Zheng").Id
                    },
                new CursoInstrutor {
                    CursoId = courses.Single(c => c.Titulo == "Calculus" ).CursoId,
                    InstrutorId = instrutores.Single(i => i.Sobrenome == "Fakhouri").Id
                    },
                new CursoInstrutor {
                    CursoId = courses.Single(c => c.Titulo == "Trigonometry" ).CursoId,
                    InstrutorId = instrutores.Single(i => i.Sobrenome == "Harui").Id
                    },
                new CursoInstrutor {
                    CursoId = courses.Single(c => c.Titulo == "Composition" ).CursoId,
                    InstrutorId = instrutores.Single(i => i.Sobrenome == "Abercrombie").Id
                    },
                new CursoInstrutor {
                    CursoId = courses.Single(c => c.Titulo == "Literature" ).CursoId,
                    InstrutorId = instrutores.Single(i => i.Sobrenome == "Abercrombie").Id
                    },
            };

            foreach (CursoInstrutor ci in courseInstructors)
            {
                contexto.CursoInstrutor.Add(ci);
            }
            contexto.SaveChanges();

            var enrollments = new CursoEstudante[]
            {
                new CursoEstudante {
                    EstudanteId = estudantes.Single(s => s.Sobrenome == "Alexander").Id,
                    CursoID = courses.Single(c => c.Titulo == "Chemistry" ).CursoId,
                    Grade = Grades.A
                },
                    new CursoEstudante {
                    EstudanteId = estudantes.Single(s => s.Sobrenome == "Alexander").Id,
                    CursoID = courses.Single(c => c.Titulo == "Microeconomics" ).CursoId,
                    Grade = Grades.C
                    },
                    new CursoEstudante {
                    EstudanteId = estudantes.Single(s => s.Sobrenome == "Alexander").Id,
                    CursoID = courses.Single(c => c.Titulo == "Macroeconomics" ).CursoId,
                    Grade = Grades.B
                    },
                    new CursoEstudante {
                        EstudanteId = estudantes.Single(s => s.Sobrenome == "Alonso").Id,
                    CursoID = courses.Single(c => c.Titulo == "Calculus" ).CursoId,
                    Grade = Grades.B
                    },
                    new CursoEstudante {
                        EstudanteId = estudantes.Single(s => s.Sobrenome == "Alonso").Id,
                    CursoID = courses.Single(c => c.Titulo == "Trigonometry" ).CursoId,
                    Grade = Grades.B
                    },
                    new CursoEstudante {
                    EstudanteId = estudantes.Single(s => s.Sobrenome == "Alonso").Id,
                    CursoID = courses.Single(c => c.Titulo == "Composition" ).CursoId,
                    Grade = Grades.B
                    },
                    new CursoEstudante {
                    EstudanteId = estudantes.Single(s => s.Sobrenome == "Anand").Id,
                    CursoID = courses.Single(c => c.Titulo == "Chemistry" ).CursoId
                    },
                    new CursoEstudante {
                    EstudanteId = estudantes.Single(s => s.Sobrenome == "Anand").Id,
                    CursoID = courses.Single(c => c.Titulo == "Microeconomics").CursoId,
                    Grade = Grades.B
                    },
                new CursoEstudante {
                    EstudanteId = estudantes.Single(s => s.Sobrenome == "Barzdukas").Id,
                    CursoID = courses.Single(c => c.Titulo == "Chemistry").CursoId,
                    Grade = Grades.B
                    },
                    new CursoEstudante {
                    EstudanteId = estudantes.Single(s => s.Sobrenome == "Li").Id,
                    CursoID = courses.Single(c => c.Titulo == "Composition").CursoId,
                    Grade = Grades.B
                    },
                    new CursoEstudante {
                    EstudanteId = estudantes.Single(s => s.Sobrenome == "Justice").Id,
                    CursoID = courses.Single(c => c.Titulo == "Literature").CursoId,
                    Grade = Grades.B
                    }
            };

            foreach (CursoEstudante e in enrollments)
            {
                var enrollmentInDataBase = contexto.CursoEstudantes.Where(
                    s =>
                            s.Estudante.Id == e.EstudanteId &&
                            s.Curso.CursoId == e.CursoID).SingleOrDefault();
                if (enrollmentInDataBase == null)
                {
                    contexto.CursoEstudantes.Add(e);
                }
            }
            contexto.SaveChanges();
        }

    }
}
