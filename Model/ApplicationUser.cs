using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ApplicationUser : IdentityUser
    {
        // Se quiser campos extras...
        public string NomeCompleto { get; set; }
    }
}
