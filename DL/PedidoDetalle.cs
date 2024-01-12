using System;
using System.Collections.Generic;

namespace DL;

public partial class PedidoDetalle
{
    public int IdDetalle { get; set; }

    public int? IdPedido { get; set; }

    public decimal? Total { get; set; }

    public int? IdProducto { get; set; }

    public decimal? Precio { get; set; }

    public int? Cantidad { get; set; }

    public virtual Pedido? IdPedidoNavigation { get; set; }

    public virtual Producto? IdProductoNavigation { get; set; }
}
