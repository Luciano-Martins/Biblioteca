using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Model;
using Model.DTOs;
using Service.IServices;

namespace Service.IService
{
    public interface IAssuntoService
    {
        // 1. GetAll deve retornar uma coleção de DTOs
        Task<IEnumerable<AssuntoDto>> GetAll();

        // 2. GetById deve retornar um DTO
        Task<AssuntoDto?> GetById(int id);

        // 3. Create deve receber um DTO e retornar o DTO
        Task<AssuntoDto> Create(AssuntoDto assuntoDto);

        // 4. Update deve receber um DTO e retornar o DTO
        Task<AssuntoDto> Update(AssuntoDto assuntoDto);

        // 5. Delete não muda (pois lida apenas com o ID)
        Task<DeleteResult> Delete(int id);
    }
}