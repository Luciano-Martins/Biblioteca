using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Model;
using Repositorio.Data;
using Repositorio.IRepositorios;

namespace Repositorio.Repositorios
{
    public class LivroRepositorio : ILivroRepositorio
    {
        private readonly AppDbContexto _contexto;

        public LivroRepositorio(AppDbContexto contexto)
        {
            _contexto = contexto;
        }
        public async Task<IEnumerable<Livro>> GetAll()
        {
           
            return await GetLivroQuery().ToListAsync();
        }
        public async Task<Livro?> GetById(int id)
        {
            var livro = await GetLivroQuery().FirstOrDefaultAsync(f => f.LivroId == id);
            return livro;
        }
        public async Task<Livro> Create(Livro livro)
        {
            _contexto.Livros.Add(livro);
            await _contexto.SaveChangesAsync();
            return livro;
        }
        public async Task<Livro> Update(Livro livro)
        {
            _contexto.Livros.Update(livro);
            await _contexto.SaveChangesAsync();
            return livro;
        }
        public async Task Delete(Livro livro)
        {
            _contexto.Livros.Remove(livro);
            await _contexto.SaveChangesAsync();
        }
        private IQueryable<Livro> GetLivroQuery()
        {
            return _contexto.Livros
                // 1. Incluindo 1:N
                // O compilador pode reclamar se a propriedade 'Editora' no Livro.cs for 'Editora?'
                .Include(l => l.Editora!) // Adicione o '!' aqui

                // 2. Incluindo 1:N
                // O compilador pode reclamar se a propriedade 'Assunto' no Livro.cs for 'Assunto?'
                .Include(l => l.Assunto!) // Adicione o '!' aqui

                // 3. Incluindo o N:N
                // O compilador pode reclamar se 'LivroAutores' for 'ICollection<LivroAutor>?'
                .Include(l => l.LivroAutores!)
                    // 4. ThenInclude para o objeto Autor
                    // O compilador pode reclamar se 'la.Autor' no LivroAutor.cs for 'Autor?'
                    .ThenInclude(la => la.Autor!)
                    .OrderByDescending(id => id.LivroId);
        }
    }
}