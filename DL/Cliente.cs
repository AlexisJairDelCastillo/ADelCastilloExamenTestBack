using System;
using System.Collections.Generic;

namespace DL;

public partial class Cliente
{
    public int IdCliente { get; set; }

    public string? Nombre { get; set; }

    public string? Apellidos { get; set; }

    public string? CorreoElectronico { get; set; }

    public string? Contrasenia { get; set; }

    public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();
}
