using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Model
{
    public class Autor
    {

        public int AutorId { get; set; } // Chave primária
        public string NomeAutor { get; set; } = string.Empty;
        public string SobreNomeAutor { get; set; } = string.Empty;

        // Propriedade de Navegação (Para o relacionamento M-N com Livro)
        public ICollection<LivroAutor>? LivroAutores { get; set; }
    }
}