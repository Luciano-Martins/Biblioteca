using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Model.DTOs
{
    public class AutorDto
    {
        // Usado em GET (Saída)
        public int AutorId { get; set; }

        // Usado em POST/PUT (Entrada) e GET (Saída)
        public string NomeAutor { get; set; } = string.Empty;
        public string SobreNomeAutor { get; set; } = string.Empty;

        // NOTA: Omitimos a coleção ICollection<LivroAutor> para quebrar o ciclo.

    }
}