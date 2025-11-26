using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Model;

namespace Repositorio.Data
{
    public class AppDbContexto : IdentityDbContext<ApplicationUser>
    {

        // DBSETs para as tabelas principais
        public DbSet<Livro> Livros { get; set; }
        public DbSet<Autor> Autores { get; set; }
        public DbSet<Assunto> Assuntos { get; set; }
        public DbSet<Editora> Editoras { get; set; }

        // DBSET para a tabela de junção (M-N)
        public DbSet<LivroAutor> LivroAutores { get; set; }
        public AppDbContexto(DbContextOptions<AppDbContexto> option) : base(option)
        {

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // 🔥 IMPORTANTE: adicionada a linha que faltava
            base.OnModelCreating(modelBuilder);
            // -----------------------------------------------------
            // 1. Configuração da Tabela de Junção (LivroAutor - M-N)
            // -----------------------------------------------------

            modelBuilder.Entity<LivroAutor>()
                .HasKey(la => new { la.LivroId, la.AutorId }); // Define a chave primária composta

            // Configura o relacionamento com Livro
            modelBuilder.Entity<LivroAutor>()
                .HasOne(la => la.Livro)
                .WithMany(l => l.LivroAutores) // O nome da Collection na classe Livro
                .HasForeignKey(la => la.LivroId);

            // Configura o relacionamento com Autor
            modelBuilder.Entity<LivroAutor>()
                .HasOne(la => la.Autor)
                .WithMany(a => a.LivroAutores) // O nome da Collection na classe Autor
                .HasForeignKey(la => la.AutorId);

            // -----------------------------------------------------
            // 2. Configuração dos Relacionamentos M-1 (Livro com Editora e Assunto)
            // -----------------------------------------------------

            // Relacionamento Livro (Muitos) para Editora (Um)
            modelBuilder.Entity<Livro>()
                .HasOne(l => l.Editora) // Propriedade de navegação na classe Livro
                .WithMany(e => e.Livros) // Propriedade de navegação na classe Editora
                .HasForeignKey(l => l.EditoraId); // Chave estrangeira na classe Livro

            // Relacionamento Livro (Muitos) para Assunto (Um)
            modelBuilder.Entity<Livro>()
                .HasOne(l => l.Assunto) // Propriedade de navegação na classe Livro
                .WithMany(a => a.Livros) // Propriedade de navegação na classe Assunto
                .HasForeignKey(l => l.AssuntoId); // Chave estrangeira na classe Livro
        }
    }
}