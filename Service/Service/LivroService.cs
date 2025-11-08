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
    public class LivroService : ILivroService
    {
        private readonly ILivroRepositorio _repositorio;
        private readonly IMapper _mapper; // 👈 1. Injetar IMapper

        // 2. Atualizar o Construtor
        public LivroService(ILivroRepositorio repositorio, IMapper mapper)
        {
            _repositorio = repositorio;
            _mapper = mapper;
        }

        // -------------------------------------------------------------------
        // MÉTODOS DE LEITURA (Entidade -> DTO)
        // -------------------------------------------------------------------

        public async Task<IEnumerable<LivroDto>> GetAll()
        {
            var livroList = await _repositorio.GetAll();
            // Mapeia a lista de Entidades para uma lista de DTOs
            return _mapper.Map<IEnumerable<LivroDto>>(livroList);
        }

        public async Task<LivroDto?> GetById(int id)
        {
            var livroEntidade = await _repositorio.GetById(id);
            if (livroEntidade == null)
            {
                return null;
            }
            // Mapeia a Entidade para o DTO
            return _mapper.Map<LivroDto>(livroEntidade);
        }

        // -------------------------------------------------------------------
        // MÉTODOS DE ESCRITA (DTO -> Entidade -> DTO)
        // -------------------------------------------------------------------

        public async Task<LivroDto> Create(LivroDto livroDto)
        {
            // 1. Mapeia DTO (entrada) para Entidade (LivroAutores será ignorado pelo AutoMapper)
            var livroEntidade = _mapper.Map<Livro>(livroDto);

            // -------------------------------------------------------------------
            // 🛑 LÓGICA MANUAL PARA O N:N (Mapeando AutoresIds para LivroAutores)
            // -------------------------------------------------------------------
            // Supondo que LivroDto tem a propriedade List<AutorDto> chamada Autores.
            if (livroDto.Autores != null && livroDto.Autores.Any())
            {
                // Mapeamos a lista de AutorDto para a lista de ligação LivroAutor
                livroEntidade.LivroAutores = livroDto.Autores
                    .Select(autorDto => new LivroAutor
                    {
                        AutorId = autorDto.AutorId,
                        // O LivroId será preenchido pelo EF Core no momento do SaveChanges
                    })
                    .ToList();
            }
            // -------------------------------------------------------------------

            // 2. Salva a Entidade
            var livroSalvo = await _repositorio.Create(livroEntidade);

            // 3. Mapeia a Entidade salva de volta para DTO
            return _mapper.Map<LivroDto>(livroSalvo);
        }

        public async Task<LivroDto> Update(LivroDto livroDto)
        {
            // 1. **Primeiro, obtenha a entidade existente** para garantir que o EF Core a esteja rastreando.
            var livroEntidade = await _repositorio.GetById(livroDto.LivroId);

            if (livroEntidade == null)
            {
                // Você pode lançar uma exceção ou retornar null, dependendo da sua regra.
                // Aqui, vamos apenas mapear para um NotFound no Controller.
                return null!; // Indicamos que não foi encontrado
            }

            // 2. Atualize as propriedades simples com o AutoMapper. 
            // O AutoMapper NÃO atualiza relacionamentos de coleção complexos (LivroAutores).
            _mapper.Map(livroDto, livroEntidade);

            // -------------------------------------------------------------------
            // 🛑 LÓGICA MANUAL PARA ATUALIZAR O N:N
            // Essa lógica deve limpar as ligações antigas e adicionar as novas.
            // -------------------------------------------------------------------

            // Limpa as ligações existentes para a entidade
            livroEntidade.LivroAutores?.Clear();

            // Adiciona as novas ligações a partir dos AutoresIDs no DTO
            if (livroDto.Autores != null && livroDto.Autores.Any())
            {
                livroEntidade.LivroAutores = livroDto.Autores
                    .Select(autorDto => new LivroAutor
                    {
                        LivroId = livroEntidade.LivroId,
                        AutorId = autorDto.AutorId
                    })
                    .ToList();
            }
            // -------------------------------------------------------------------

            // 3. Salva a Entidade rastreada e atualizada
            var livroAtualizado = await _repositorio.Update(livroEntidade);

            // 4. Mapeia a Entidade atualizada de volta para DTO
            return _mapper.Map<LivroDto>(livroAtualizado);
        }

        // -------------------------------------------------------------------
        // MÉTODO DELETE (Não usa DTOs)
        // -------------------------------------------------------------------

        public async Task<DeleteResult> Delete(int id)
        {
            var livro = await _repositorio.GetById(id);
            if (livro == null)
            {
                return DeleteResult.NotFound;
            }
            await _repositorio.Delete(livro);
            return DeleteResult.Success;
        }

    }
}