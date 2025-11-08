using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Model;

namespace Repositorio.IRepositorios
{
    public interface IEditoraRepositorio
    {
        Task<IEnumerable<Editora>> GetAll();
        Task<Editora?> GetById(int id);
        Task<Editora> Update(Editora editora);
        Task<Editora> Create(Editora editora);
        Task Delete(Editora editora);
    }
}