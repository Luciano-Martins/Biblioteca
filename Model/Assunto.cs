using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Model
{
    public class Assunto
    {

        public int AssuntoId { get; set; } // Chave primária
        public string NomeAssunto { get; set; } = string.Empty;

        // Propriedade de Navegação (Para o relacionamento 1-M com Livro)
        public ICollection<Livro>? Livros { get; set; }
    }
}