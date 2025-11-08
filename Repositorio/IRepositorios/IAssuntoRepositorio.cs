using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Model;

namespace Repositorio.IRepositorios
{
    public interface IAssuntoRepositorio
    {
        Task<IEnumerable<Assunto>> GetAll();
        Task<Assunto?> GetById(int id);
        Task<Assunto> Update(Assunto assunto);
        Task<Assunto> Create(Assunto assunto);
        Task Delete(Assunto assunto);
    }
}