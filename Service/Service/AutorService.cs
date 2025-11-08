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
    public class AutorService : IAutorService
    {
        private readonly IAutorRepositorio _repositorio;
        private readonly IMapper _mapper;

        public AutorService(IAutorRepositorio repositorio, IMapper mapper)
        {
            _repositorio = repositorio;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AutorDto>> GetAll()
        {
            var autorList = await _repositorio.GetAll();
            // 2. Mapeia a lista de Entidades para uma lista de DTOs
            return _mapper.Map<IEnumerable<AutorDto>>(autorList);

        }

        public async Task<AutorDto?> GetById(int id)
        {
            // 1. Busca a Entidade
            var autorEntidade = await _repositorio.GetById(id);

            // 2. Se não encontrou, retorna null
            if (autorEntidade == null)
            {
                return null;
            }

            // 3. Mapeia a Entidade para o DTO e retorna
            return _mapper.Map<AutorDto>(autorEntidade);
        }

        public async Task<AutorDto> Create(AutorDto autor)
        {
            // 1. Mapeia DTO (entrada) para Entidade (EF Core)
            var autorEntidade = _mapper.Map<Autor>(autor);

            // 2. Chama o repositório com a Entidade
            var autorSalvo = await _repositorio.Create(autorEntidade);

            // 3. Mapeia a Entidade (salva) de volta para DTO (saída)
            return _mapper.Map<AutorDto>(autorSalvo);
        }

        public async Task<AutorDto> Update(AutorDto autor)
        {
            // 1. Mapeia DTO (entrada) para Entidade (EF Core)
            var autorEntidade = _mapper.Map<Autor>(autor);

            // 2. Chama o repositório com a Entidade
            var autorSalvo = await _repositorio.Update(autorEntidade);

            // 3. Mapeia a Entidade (atualizada) de volta para DTO (saída)
            return _mapper.Map<AutorDto>(autorSalvo);
        }
        public async Task<DeleteResult> Delete(int id)
        {
            var autor = await _repositorio.GetById(id);
            if (autor == null)
            {
                return DeleteResult.NotFound;
            }
            await _repositorio.Delete(autor);
            return DeleteResult.Success;
        }

    }
}