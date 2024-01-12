using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Pedido
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.AdelCastilloTestBackContext context = new DL.AdelCastilloTestBackContext())
                {
                    var query = context.Pedidos.FromSqlRaw("PedidoGetAll").ToList();

                    if (query.Count > 0)
                    {
                        result.Objects = new List<object>();

                        foreach (DL.Pedido resultPedido in query)
                        {
                            ML.Pedido pedido = new ML.Pedido();
                            pedido.IdPedido = resultPedido.IdPedido;
                            pedido.Cliente = new ML.Cliente();
                            pedido.Cliente.IdCliente = resultPedido.IdCliente.Value;
                            pedido.Cliente.Nombre = resultPedido.Nombre;
                            pedido.Cliente.Apellidos = resultPedido.Apellidos;
                            pedido.Total = resultPedido.Total;

                            result.Objects.Add(pedido);
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

        public static ML.Result Delete(ML.Pedido pedido)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.AdelCastilloTestBackContext context = new DL.AdelCastilloTestBackContext())
                {
                    int query = context.Database.ExecuteSqlRaw($"PedidoDelete {pedido.IdPedido}");

                    if (query > 0)
                    {
                        result.Correct = true;
                        result.Message = "El registro se elimino correctamente.";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = "Ocurrio un error al eliminar el registro." + ex.Message;
            }

            return result;
        }
    }
}
