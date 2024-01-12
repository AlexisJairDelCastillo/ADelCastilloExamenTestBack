using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Pedido
    {
        public int IdPedido { get; set; }

        public ML.Cliente? Cliente { get; set; }

        public ML.Producto? Producto { get; set; }

        public int? Cantidad { get; set; }

        public decimal? Total { get; set; }

        public List<object>? Pedidos { get; set; }
    }
}
