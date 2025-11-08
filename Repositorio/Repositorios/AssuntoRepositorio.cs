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
    public class AssuntoRepositorio : IAssuntoRepositorio
    {
        private readonly AppDbContexto _contexto;

        public AssuntoRepositorio(AppDbContexto contexto)
        {
            _contexto = contexto;
        }
        public async Task<IEnumerable<Assunto>> GetAll()
        {
            return await _contexto.Assuntos
                .OrderByDescending(a => a.AssuntoId)
                .ToListAsync();
        }
        public async Task<Assunto?> GetById(int id)
        {
            return await _contexto.Assuntos.FindAsync(id);
        }

        public async Task<Assunto> Create(Assunto assunto)
        {
            await _contexto.Assuntos.AddAsync(assunto);
            await _contexto.SaveChangesAsync();
            return assunto;
        }

        public async Task<Assunto> Update(Assunto assunto)
        {
            _contexto.Assuntos.Update(assunto);
            await _contexto.SaveChangesAsync();
            return assunto;
        }

        public async Task Delete(Assunto assunto)
        {
            _contexto.Assuntos.Remove(assunto);
            await _contexto.SaveChangesAsync();
        }
    }
}