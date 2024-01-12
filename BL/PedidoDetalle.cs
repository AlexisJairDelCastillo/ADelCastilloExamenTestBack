using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class PedidoDetalle
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.AdelCastilloTestBackContext context = new DL.AdelCastilloTestBackContext())
                {
                    var query = context.PedidoDetalles.FromSqlRaw("PedidoDetalleGetAll").ToList();

                    if (query.Count > 0)
                    {
                        result.Objects = new List<object>();

                        foreach (DL.PedidoDetalle resultDetalle in query)
                        {
                            ML.PedidoDetalle detalle = new ML.PedidoDetalle();
                            detalle.IdDetalle = resultDetalle.IdDetalle;
                            detalle.Pedido = new ML.Pedido();
                            detalle.Pedido.IdPedido = resultDetalle.IdPedido.Value;
                            detalle.Producto = new ML.Producto();
                            detalle.Producto.IdProducto = resultDetalle.IdProducto.Value;
                            detalle.Cantidad = resultDetalle.Cantidad.Value;
                            detalle.Producto.Precio = resultDetalle.Precio.Value;
                            detalle.Pedido.Total = resultDetalle.Total.Value;

                            result.Objects.Add(detalle);
                        }

                        result.Correct = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = "Ocurrio un error al mostrar los registros." + ex.Message;
            }

            return result;
        }

        public static ML.Result Add(ML.Pedido pedido)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.AdelCastilloTestBackContext context = new DL.AdelCastilloTestBackContext())
                {
                    int query = context.Database.ExecuteSqlRaw($"PedidoAdd {pedido.Cliente.IdCliente}, {pedido.Producto.IdProducto}, {pedido.Cantidad}");

                    if (query > 0)
                    {
                        result.Correct = true;
                        result.Message = "El registro se inserto correctamente.";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = "Ocurrio un error al insertar el registro." + ex.Message;
            }

            return result;
        }
    }
}
