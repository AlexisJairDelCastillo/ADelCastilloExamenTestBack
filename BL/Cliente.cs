using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Cliente
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.AdelCastilloTestBackContext context = new DL.AdelCastilloTestBackContext())
                {
                    var query = context.Clientes.FromSqlRaw("ClienteGetAll").ToList();

                    if (query.Count > 0)
                    {
                        result.Objects = new List<object>();

                        foreach (DL.Cliente resultCliente in query)
                        {
                            ML.Cliente cliente = new ML.Cliente();
                            cliente.IdCliente = resultCliente.IdCliente;
                            cliente.Nombre = resultCliente.Nombre;
                            cliente.Apellidos = resultCliente.Apellidos;
                            cliente.CorreoElectronico = resultCliente.CorreoElectronico;
                            cliente.Contrasenia = resultCliente.Contrasenia;

                            result.Objects.Add(cliente);
                        }
                    }

                    result.Correct = true;
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

        public static ML.Result GetById(int idUsuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.AdelCastilloTestBackContext context = new DL.AdelCastilloTestBackContext())
                {
                    var query = context.Clientes.FromSqlRaw($"ClienteGetById {idUsuario}").AsEnumerable().FirstOrDefault();

                    if (query != null)
                    {
                        ML.Cliente cliente = new ML.Cliente();
                        cliente.IdCliente = query.IdCliente;
                        cliente.Nombre = query.Nombre;
                        cliente.Apellidos = query.Apellidos;
                        cliente.CorreoElectronico = query.CorreoElectronico;
                        cliente.Contrasenia = query.Contrasenia;

                        result.Object = cliente;
                        result.Correct = true;
                    }
                }
            }catch(Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = "Ocurrio un error al mostrar el registro." + ex.Message;
            }

            return result;
        }

        public static ML.Result Login(string correoElectronico)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.AdelCastilloTestBackContext context = new DL.AdelCastilloTestBackContext())
                {
                    var query = context.Clientes.FromSqlRaw($"ClienteGetByEmail '{correoElectronico}'").AsEnumerable().FirstOrDefault();

                    if(query != null)
                    {
                        ML.Cliente cliente = new ML.Cliente();
                        cliente.IdCliente = query.IdCliente;
                        cliente.Nombre = query.Nombre;
                        cliente.Apellidos = query.Apellidos;
                        cliente.CorreoElectronico = query.CorreoElectronico;
                        cliente.Contrasenia = query.Contrasenia;

                        result.Object = cliente;
                        result.Correct = true;
                    }
                }
            }catch(Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = "Ocurrio un error al mostrar el registro." + ex.Message;
            }

            return result;
        }
    }
}
