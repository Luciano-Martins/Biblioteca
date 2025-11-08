using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Model;
using Model.DTOs;
using Service.IServices;

namespace Service.IService
{
    public interface ILivroService
    {
        // Todos os métodos que manipulam dados (CRUD) devem usar o DTO
        Task<IEnumerable<LivroDto>> GetAll();
        Task<LivroDto?> GetById(int id);
        Task<LivroDto> Update(LivroDto livroDto);
        Task<LivroDto> Create(LivroDto livroDto);
        Task<DeleteResult> Delete(int id);
    }
}