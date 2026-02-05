using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Model.DTOs;
using Model;

namespace Biblioteca.WebApi.Helpers
{
    public class BibliotecaProfile : Profile
    {
        public BibliotecaProfile()
        {
            // ====================================================================
            // 1. Mapeamentos Simples (Entidade <-> DTO)
            // ====================================================================

            // Assunto
            CreateMap<Assunto, AssuntoDto>().ReverseMap();

            // Editora
            CreateMap<Editora, EditoraDto>().ReverseMap();

            // Autor
            CreateMap<Autor, AutorDto>().ReverseMap();


            // ====================================================================
            // 2. Mapeamento de Livro (Tratamento do Relacionamento N:N)
            // ====================================================================

            // A. Mapeamento de Saída (Entidade Livro -> LivroDto) - Para GET/Resposta

            CreateMap<Livro, LivroDto>()
                // Mapeamento da Editora e Assunto (Propriedades de navegação 1:N)
                // O AutoMapper cuida disso automaticamente, pois já mapeamos Editora/Assunto -> Dto.
                // Mas, precisamos mapear o relacionamento M-N (LivroAutores -> Autores)
                .ForMember(
                  dest => dest.Autores,
                  opt => opt.MapFrom(src =>
                        // 1. Se LivroAutores for nulo, use uma coleção vazia de LivroAutor.
                        // 2. Chame .Select() em seguida para transformar a coleção de ligação
                        //    na coleção final de Autor.
                        src.LivroAutores.Select(la => la.Autor)));

            // B. Mapeamento de Entrada (LivroDto -> Entidade Livro) - Para POST/PUT

            CreateMap<LivroDto, Livro>()
                // Ignorar as listas de navegação complexas para evitar o rastreamento desnecessário
                // (O LivroAutores será gerenciado manualmente no Service)
                .ForMember(
                    dest => dest.LivroAutores,
                    opt => opt.Ignore()
                )
                // Ignorar os DTOs de navegação (já que estamos enviando apenas o ID)
                .ForMember(
                    dest => dest.Assunto,
                    opt => opt.Ignore()
                )
                .ForMember(
                    dest => dest.Editora,
                    opt => opt.Ignore()
                );
        }
    }
}