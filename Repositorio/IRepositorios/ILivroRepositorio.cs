using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Model;

namespace Repositorio.IRepositorios
{
    public interface ILivroRepositorio
    {
        Task<IEnumerable<Livro>> GetAll();
        Task<Livro?> GetById(int id);
        Task<Livro> Update(Livro livro);
        Task<Livro> Create(Livro livro);
        Task Delete(Livro livro);
    }
}