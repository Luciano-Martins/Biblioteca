using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Model.DTOs
{
    public class LivroDto
    {
        // Usado em GET (Saída) e PUT (Entrada)
        public int LivroId { get; set; }

        // Campos principais
        public string NomeLivro { get; set; } = string.Empty;
        public string ISBN13 { get; set; } = string.Empty;
        public DateTime DataPub { get; set; }
        public decimal PrecoLivro { get; set; }
        public int NumeroPaginas { get; set; }

        // Relações 1:N (Chaves Estrangeiras para POST/PUT)
        public int EditoraId { get; set; }
        public int AssuntoId { get; set; }

        // Relações 1:N (Objetos de Navegação para GET)
        // Usamos o DTO para o GET para evitar o ciclo (ex: Livro -> Editora -> Livros...)
        public EditoraDto? Editora { get; set; }
        public AssuntoDto? Assunto { get; set; }

        // Relação M:N (Lista de Autores)
        // Para POST/PUT: o cliente envia APENAS os IDs.
        // Para GET: a resposta retorna os objetos completos dos Autores.
        public List<AutorDto> Autores { get; set; } = new List<AutorDto>();

        // NOTA IMPORTANTE: A entidade de ligação LivroAutor é completamente omitida.

    }
}