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
    public class AutorRepositorio : IAutorRepositorio
    {
        private readonly AppDbContexto _contexto;

        public AutorRepositorio(AppDbContexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<IEnumerable<Autor>> GetAll()
        {
            var autor = await _contexto.Autores
                .OrderByDescending(a => a.AutorId)
                .ToListAsync();
            return autor;
        }

        public async Task<Autor?> GetById(int id)
        {
            var autor = await _contexto.Autores.FindAsync(id);
            return autor;
        }

        public async Task<Autor> Create(Autor autor)
        {
            _contexto.Autores.Add(autor);
            await _contexto.SaveChangesAsync();
            return autor;
        }

        public async Task<Autor> Update(Autor autor)
        {
            _contexto.Autores.Update(autor);
            await _contexto.SaveChangesAsync();
            return autor;
        }
        public async Task Delete(Autor autor)
        {
            _contexto.Autores.Remove(autor);
            await _contexto.SaveChangesAsync();
        }
    }
}