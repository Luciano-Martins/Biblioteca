using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Model
{
    public class Livro
    {

        public int LivroId { get; set; } // Chave primária
        public string NomeLivro { get; set; } = string.Empty;
        public string ISBN13 { get; set; } = string.Empty;
        public DateTime DataPub { get; set; }
        public decimal PrecoLivro { get; set; }
        public int NumeroPaginas { get; set; }

        // Chaves Estrangeiras para relacionamentos 1-M
        public int EditoraId { get; set; }
        public int AssuntoId { get; set; }

        // Propriedades de Navegação (Para relacionamentos 1-M)
        public Editora? Editora { get; set; }
        public Assunto? Assunto { get; set; }

        // Propriedade de Navegação (Para o relacionamento M-N com Autor)
        public ICollection<LivroAutor>? LivroAutores { get; set; }
    }
}