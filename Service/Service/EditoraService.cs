using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Model;
using Model.DTOs;
using Repositorio.IRepositorios;
using Service.IService;
using Service.IServices;

namespace Service.Service
{
    public class EditoraService : IEditoraService
    {
        private readonly IEditoraRepositorio _repositorio;
        private readonly IMapper _mapper;

        public EditoraService(IEditoraRepositorio repositorio, IMapper mapper)
        {
            _repositorio = repositorio;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EditoraDto>> GetAll()
        {
            var editoraList = await _repositorio.GetAll();
            // 2. Mapeia a lista de Entidades para uma lista de DTOs
            return _mapper.Map<IEnumerable<EditoraDto>>(editoraList);
        }

        public async Task<EditoraDto?> GetById(int id)
        {
            // 1. Busca a Entidade
            var editoraEntidade = await _repositorio.GetById(id);

            // 2. Se não encontrou, retorna null
            if (editoraEntidade == null)
            {
                return null;
            }

            // 3. Mapeia a Entidade para o DTO e retorna
            return _mapper.Map<EditoraDto>(editoraEntidade);
        }
        public async Task<EditoraDto> Create(EditoraDto editora)
        {
            // 1. Mapeia DTO (entrada) para Entidade (EF Core)
            var editoraEntidade = _mapper.Map<Editora>(editora);

            // 2. Chama o repositório com a Entidade
            var editoraSalvo = await _repositorio.Create(editoraEntidade);

            // 3. Mapeia a Entidade (salva) de volta para DTO (saída)
            return _mapper.Map<EditoraDto>(editoraSalvo);
        }
        public async Task<EditoraDto> Update(EditoraDto editora)
        {
            // 1. Mapeia DTO (entrada) para Entidade (EF Core)
            var editoraEntidade = _mapper.Map<Editora>(editora);

            // 2. Chama o repositório com a Entidade
            var editoraSalvo = await _repositorio.Update(editoraEntidade);

            // 3. Mapeia a Entidade (atualizada) de volta para DTO (saída)
            return _mapper.Map<EditoraDto>(editoraSalvo);
        }
        public async Task<DeleteResult> Delete(int id)
        {
            var editora = await _repositorio.GetById(id);
            if (editora == null)
            {
                return DeleteResult.NotFound;
            }
            await _repositorio.Delete(editora);
            return DeleteResult.Success;
        }
    }
}