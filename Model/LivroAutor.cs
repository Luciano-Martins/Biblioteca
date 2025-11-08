using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Model
{
    public class LivroAutor
    {
        // Chaves Estrangeiras (que compõem a Chave Primária Composta)
        public int LivroId { get; set; }
        public int AutorId { get; set; }

        // Propriedades de Navegação
        public Livro? Livro { get; set; }
        public Autor? Autor { get; set; }
    }
}