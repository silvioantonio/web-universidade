﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebUniversidade.Data;

namespace WebUniversidade.Migrations
{
    [DbContext(typeof(Contexto))]
    [Migration("20200928205102_RowVersion")]
    partial class RowVersion
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WebUniversidade.Models.Curso", b =>
                {
                    b.Property<int>("CursoId")
                        .HasColumnType("int");

                    b.Property<int>("Creditos")
                        .HasColumnType("int");

                    b.Property<int?>("DepartamentoId")
                        .HasColumnType("int");

                    b.Property<int>("DepartmentID")
                        .HasColumnType("int");

                    b.Property<string>("Titulo")
                        .HasColumnType("nvarchar(40)")
                        .HasMaxLength(40);

                    b.HasKey("CursoId");

                    b.HasIndex("DepartamentoId");

                    b.ToTable("Curso");
                });

            modelBuilder.Entity("WebUniversidade.Models.CursoEstudante", b =>
                {
                    b.Property<int>("CursoID")
                        .HasColumnType("int");

                    b.Property<int>("EstudanteId")
                        .HasColumnType("int");

                    b.Property<int>("CursoEstudateId")
                        .HasColumnType("int");

                    b.Property<int?>("Grade")
                        .HasColumnType("int");

                    b.HasKey("CursoID", "EstudanteId");

                    b.HasIndex("EstudanteId");

                    b.ToTable("CursoEstudante");
                });

            modelBuilder.Entity("WebUniversidade.Models.CursoInstrutor", b =>
                {
                    b.Property<int>("CursoId")
                        .HasColumnType("int");

                    b.Property<int>("InstrutorId")
                        .HasColumnType("int");

                    b.HasKey("CursoId", "InstrutorId");

                    b.HasIndex("InstrutorId");

                    b.ToTable("CursoInstrutor");
                });

            modelBuilder.Entity("WebUniversidade.Models.Departamento", b =>
                {
                    b.Property<int>("DepartamentoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DataInicio")
                        .HasColumnType("datetime2");

                    b.Property<int?>("InstrutorId")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<decimal>("Orcamento")
                        .HasColumnType("money");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.HasKey("DepartamentoId");

                    b.HasIndex("InstrutorId");

                    b.ToTable("Departamento");
                });

            modelBuilder.Entity("WebUniversidade.Models.Escritorio", b =>
                {
                    b.Property<int>("InstrutorId")
                        .HasColumnType("int");

                    b.Property<string>("Localizacao")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("InstrutorId");

                    b.ToTable("Escritorio");
                });

            modelBuilder.Entity("WebUniversidade.Models.Estudante", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("EnrollmentDate")
                        .HasColumnName("DataEntrada")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Sobrenome")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Estudante");
                });

            modelBuilder.Entity("WebUniversidade.Models.Instrutor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DataContratacao")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(40)")
                        .HasMaxLength(40);

                    b.Property<string>("Sobrenome")
                        .IsRequired()
                        .HasColumnType("nvarchar(40)")
                        .HasMaxLength(40);

                    b.HasKey("Id");

                    b.ToTable("Instrutor");
                });

            modelBuilder.Entity("WebUniversidade.Models.Curso", b =>
                {
                    b.HasOne("WebUniversidade.Models.Departamento", "Departamento")
                        .WithMany("Cursos")
                        .HasForeignKey("DepartamentoId");
                });

            modelBuilder.Entity("WebUniversidade.Models.CursoEstudante", b =>
                {
                    b.HasOne("WebUniversidade.Models.Curso", "Curso")
                        .WithMany("CursoEstudantes")
                        .HasForeignKey("CursoID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebUniversidade.Models.Estudante", "Estudante")
                        .WithMany("CursoEstudantes")
                        .HasForeignKey("EstudanteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WebUniversidade.Models.CursoInstrutor", b =>
                {
                    b.HasOne("WebUniversidade.Models.Curso", "Curso")
                        .WithMany("CursoInstrutores")
                        .HasForeignKey("CursoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebUniversidade.Models.Instrutor", "Instrutor")
                        .WithMany("CursoInstrutores")
                        .HasForeignKey("InstrutorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WebUniversidade.Models.Departamento", b =>
                {
                    b.HasOne("WebUniversidade.Models.Instrutor", "Administrador")
                        .WithMany()
                        .HasForeignKey("InstrutorId");
                });

            modelBuilder.Entity("WebUniversidade.Models.Escritorio", b =>
                {
                    b.HasOne("WebUniversidade.Models.Instrutor", "Instrutor")
                        .WithOne("Escritorio")
                        .HasForeignKey("WebUniversidade.Models.Escritorio", "InstrutorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
