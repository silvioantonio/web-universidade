using Microsoft.EntityFrameworkCore;
using WebUniversidade.Models;

namespace WebUniversidade.Data
{
    public class Contexto : DbContext
    {
        public Contexto(DbContextOptions<Contexto> options) : base(options)
        {

        }

        public DbSet<Curso> Cursos { get; set; }
        public DbSet<CursoEstudante> CursoEstudantes { get; set; }
        public DbSet<Estudante> Estudantes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Curso>().ToTable("Curso");
            modelBuilder.Entity<CursoEstudante>().ToTable("CursoEstudante");
            modelBuilder.Entity<CursoEstudante>().HasKey(ce => new { ce.CursoID, ce.EstudanteId });
            modelBuilder.Entity<Estudante>().ToTable("Estudante");
        }

    }
}
