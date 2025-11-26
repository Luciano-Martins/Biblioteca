using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dtos
{
    public class RegisterDto
    {
        public string NomeCompleto { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
    }
}
