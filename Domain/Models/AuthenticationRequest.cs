using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class AuthenticationRequest
    {
        public string? Usuario { get; set; }
        public string? Contrasena { get; set; }
    }

}
