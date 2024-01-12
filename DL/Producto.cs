using System;
using System.Collections.Generic;

namespace DL;

public partial class Producto
{
    public int IdProducto { get; set; }

    public string? NombreProducto { get; set; }

    public string? Descripcion { get; set; }

    public decimal? Precio { get; set; }

    public int? IdCategoria { get; set; }

    public string? NombreCategoria { get; set; }

    public virtual Categorium? IdCategoriaNavigation { get; set; }

    public virtual ICollection<PedidoDetalle> PedidoDetalles { get; set; } = new List<PedidoDetalle>();
}
