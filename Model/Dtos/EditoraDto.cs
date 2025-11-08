using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Model.DTOs
{
    public class EditoraDto
    {
        // Usado em GET (Saída)
        public int EditoraId { get; set; }

        // Usado em POST/PUT (Entrada) e GET (Saída)
        public string NomeEditora { get; set; } = string.Empty;

        // NOTA: Omitimos a coleção ICollection<Livro> para quebrar o ciclo.
    }
}