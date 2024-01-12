using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Producto
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.AdelCastilloTestBackContext context = new DL.AdelCastilloTestBackContext())
                {
                    var query = context.Productos.FromSqlRaw("ProductoGetAll").ToList();

                    if(query.Count > 0)
                    {
                        result.Objects = new List<object>();

                        foreach(DL.Producto resultProducto in query)
                        {
                            ML.Producto producto = new ML.Producto();
                            producto.IdProducto = resultProducto.IdProducto;
                            producto.NombreProducto = resultProducto.NombreProducto;
                            producto.Descripcion = resultProducto.Descripcion;
                            producto.Precio = resultProducto.Precio;
                            producto.Categoria = new ML.Categoria();
                            producto.Categoria.IdCategoria = resultProducto.IdCategoria.Value;
                            producto.Categoria.NombreCategoria = resultProducto.NombreCategoria;

                            result.Objects.Add(producto);
                        }
                    }

                    result.Correct = true;
                }
            }catch(Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = "Ocurrio un error al mostrar los registros." + ex.Message;
            }

            return result;
        }
    }
}
