using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Cliente
    {
        public int IdCliente { get; set; }

        public string? Nombre { get; set; }

        public string? Apellidos { get; set; }

        public string? CorreoElectronico { get; set; }

        public string? Contrasenia { get; set; }

        public List<object>? Clientes { get; set; }
    }
}
