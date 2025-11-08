using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Model;
using Model.DTOs;
using Service.IServices;

namespace Service.IService
{
    public interface IAutorService
    {
        Task<IEnumerable<AutorDto>> GetAll();
        Task<AutorDto?> GetById(int id);
        Task<AutorDto> Update(AutorDto autor);
        Task<AutorDto> Create(AutorDto autor);
        Task<DeleteResult> Delete(int id);
    }
}