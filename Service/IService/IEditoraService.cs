using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Model;
using Model.DTOs;
using Service.IServices;

namespace Service.IService
{
    public interface IEditoraService
    {
        Task<IEnumerable<EditoraDto>> GetAll();
        Task<EditoraDto?> GetById(int id);
        Task<EditoraDto> Update(EditoraDto editora);
        Task<EditoraDto> Create(EditoraDto editora);
        Task<DeleteResult> Delete(int id);
    }
}