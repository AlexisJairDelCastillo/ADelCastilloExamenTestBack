using System;
using System.Collections.Generic;

namespace DL;

public partial class Pedido
{
    public int IdPedido { get; set; }

    public int? IdCliente { get; set; }

    public string? Nombre { get; set; }

    public string? Apellidos { get; set; }

    public decimal? Total { get; set; }

    public virtual Cliente? IdClienteNavigation { get; set; }

    public virtual ICollection<PedidoDetalle> PedidoDetalles { get; set; } = new List<PedidoDetalle>();
}
