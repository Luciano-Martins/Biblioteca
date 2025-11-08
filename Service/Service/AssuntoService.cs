using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Model;
using Repositorio.IRepositorios;
using Service.IServices;
using Model.DTOs;
using Service.IService; // 👈 Necessário para usar AssuntoDto

namespace Service.Service
{
    public class AssuntoService : IAssuntoService // Implementando a interface atualizada
    {
        private readonly IAssuntoRepositorio _repositorio;
        private readonly IMapper _mapper;

        public AssuntoService(IAssuntoRepositorio repositorio, IMapper mapper)
        {
            _repositorio = repositorio;
            _mapper = mapper;
        }

        // -------------------------------------------------------------------
        // MÉTODOS DE LEITURA (Entidade -> DTO)
        // -------------------------------------------------------------------

        public async Task<IEnumerable<AssuntoDto>> GetAll()
        {
            // 1. Busca a lista de Entidades
            var assuntoList = await _repositorio.GetAll();

            // 2. Mapeia a lista de Entidades para uma lista de DTOs
            return _mapper.Map<IEnumerable<AssuntoDto>>(assuntoList);
        }

        public async Task<AssuntoDto?> GetById(int id)
        {
            // 1. Busca a Entidade
            var assuntoEntidade = await _repositorio.GetById(id);

            // 2. Se não encontrou, retorna null
            if (assuntoEntidade == null)
            {
                return null;
            }

            // 3. Mapeia a Entidade para o DTO e retorna
            return _mapper.Map<AssuntoDto>(assuntoEntidade);
        }

        // -------------------------------------------------------------------
        // MÉTODOS DE ESCRITA (DTO -> Entidade -> DTO)
        // -------------------------------------------------------------------

        public async Task<AssuntoDto> Create(AssuntoDto assuntoDto)
        {
            // 1. Mapeia DTO (entrada) para Entidade (EF Core)
            var assuntoEntidade = _mapper.Map<Assunto>(assuntoDto);

            // 2. Chama o repositório com a Entidade
            var assuntoSalvo = await _repositorio.Create(assuntoEntidade);

            // 3. Mapeia a Entidade (salva) de volta para DTO (saída)
            return _mapper.Map<AssuntoDto>(assuntoSalvo);
        }

        public async Task<AssuntoDto> Update(AssuntoDto assuntoDto)
        {
            // 1. Mapeia DTO (entrada) para Entidade (EF Core)
            var assuntoEntidade = _mapper.Map<Assunto>(assuntoDto);

            // 2. Chama o repositório com a Entidade
            var assuntoSalvo = await _repositorio.Update(assuntoEntidade);

            // 3. Mapeia a Entidade (atualizada) de volta para DTO (saída)
            return _mapper.Map<AssuntoDto>(assuntoSalvo);
        }

        // -------------------------------------------------------------------
        // MÉTODO DELETE (Não usa DTOs)
        // -------------------------------------------------------------------

        public async Task<DeleteResult> Delete(int id)
        {
            var assunto = await _repositorio.GetById(id);
            if (assunto == null)
            {
                return DeleteResult.NotFound;
            }
            await _repositorio.Delete(assunto);
            return DeleteResult.Success;
        }
    }
}