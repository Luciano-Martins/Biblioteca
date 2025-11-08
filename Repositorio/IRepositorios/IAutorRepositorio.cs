using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Model;

namespace Repositorio.IRepositorios
{
    public interface IAutorRepositorio
    {
        Task<IEnumerable<Autor>> GetAll();
        Task<Autor?> GetById(int id);
        Task<Autor> Update(Autor autor);
        Task<Autor> Create(Autor autor);
        Task Delete(Autor autor);
    }
}