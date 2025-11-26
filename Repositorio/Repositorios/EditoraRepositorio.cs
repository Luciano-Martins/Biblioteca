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
    public class EditoraRepositorio : IEditoraRepositorio
    {
        private readonly AppDbContexto _contexto;

        public EditoraRepositorio(AppDbContexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<IEnumerable<Editora>> GetAll()
        {
            return await _contexto.Editoras
                .OrderByDescending(a => a.EditoraId)
                .ToListAsync();
        }

        public async Task<Editora?> GetById(int id)
        {
            return await _contexto.Editoras.FindAsync(id);
        }
        public async Task<Editora> Create(Editora editora)
        {
            await _contexto.Editoras.AddAsync(editora);
            await _contexto.SaveChangesAsync();
            return editora;
        }
        public async Task<Editora> Update(Editora editora)
        {
            _contexto.Editoras.Update(editora);
            await _contexto.SaveChangesAsync();
            return editora;
        }
        public async Task Delete(Editora editora)
        {
            _contexto.Editoras.Remove(editora);
            await _contexto.SaveChangesAsync();
        }

    }
}