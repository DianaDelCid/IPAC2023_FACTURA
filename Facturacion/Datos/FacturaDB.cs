using Entidades;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace Datos
{
    public class FacturaDB
    {
        //Cadena de conexion, campos donde esta el servidor
        string cadena = "server=localhost; user=root; database=factura; password=diana21;";

        //METODO PARA GUARDAR LA FACTURA
        public bool Guardar(Factura factura, List<DetalleFactura> detalles)
        {
            bool inserto = false;
            int idFactura = 0; //variable para el id

            try
            {
                //SENTENCIAS SQL
                StringBuilder sqlFactura = new StringBuilder();
                sqlFactura.Append("INSERT INTO factura VALUES (@Fecha, @IdentidadCliente, @CodigoUsuario, @ISV, @Descuento, @SubTotal, @Total );"); //Sentencia para guardar
                sqlFactura.Append(" SELECT LAST_INSERT_ID();"); //sentencia que devuelve la ultima llave primaria de la tabla que se acaba de insertar

                //SENTENCIA PARA INSERTAR EL DETALLE
                StringBuilder sqlDetalle = new StringBuilder(); //objeto
                sqlDetalle.Append(" INSERT INTO detallefactura VALUES (@IdFactura, @CodigoProducto, @Precio, @Cantidad, @Total);"); //sentencia para insertar detalleFactura

                //SENTENCIA PARA ACTUALIZAR LA EXISTENCIA DE PRODUCTOS
                StringBuilder sqlExistencia = new StringBuilder();
                sqlExistencia.Append(" UPDATE producto SET Existencia = Existencia - @Cantidad WHERE Codigo = @Codigo;"); //Revaja la existencia de producto del codigo producto

                //OBJETO CADENA 
                using (MySqlConnection con = new MySqlConnection(cadena))
                {
                    con.Open(); //abrimos la conexion

                    //TRANSACCIONES ROLBA
                    MySqlTransaction transaction = con.BeginTransaction(System.Data.IsolationLevel.ReadCommitted);

                    try
                    {
                        using (MySqlCommand cmd1 = new MySqlCommand(sqlFactura.ToString(), con, transaction))
                        {
                            cmd1.CommandType = System.Data.CommandType.Text;
                            //Parametros de la factura
                            cmd1.Parameters.Add("@Fecha", MySqlDbType.DateTime).Value = factura.Fecha;
                            cmd1.Parameters.Add("@IdentidadCliente", MySqlDbType.VarChar, 25).Value = factura.IdentidadCliente;
                            cmd1.Parameters.Add("@CodigoUsuario", MySqlDbType.VarChar, 50).Value = factura.CodigoUsuario;
                            cmd1.Parameters.Add("@ISV", MySqlDbType.Decimal).Value = factura.ISV;
                            cmd1.Parameters.Add("@Descuento", MySqlDbType.Decimal).Value = factura.Descuento;
                            cmd1.Parameters.Add("@SubTotal", MySqlDbType.Decimal).Value = factura.Subtotal;
                            cmd1.Parameters.Add("@Total", MySqlDbType.Decimal).Value = factura.Total;
                            idFactura = Convert.ToInt32(cmd1.ExecuteScalar()); //le asignamos al id para capturar el id

                        }

                        //Iteramos una lista de detalle
                        foreach (DetalleFactura detalle in detalles) //se utiliza para las listas
                        {
                            //PARA DETALLES DE PRODUCTOS
                            using (MySqlCommand cmd2 = new MySqlCommand(sqlDetalle.ToString(), con, transaction))
                            {
                                cmd2.CommandType = System.Data.CommandType.Text;
                                //Parametros de detalle producto
                                cmd2.Parameters.Add("@IdFactura", MySqlDbType.Int32).Value = idFactura;
                                cmd2.Parameters.Add("@CodigoProducto", MySqlDbType.VarChar, 25).Value = detalle.CodigoProducto;
                                cmd2.Parameters.Add("@Precio", MySqlDbType.Decimal).Value = detalle.Precio;
                                cmd2.Parameters.Add("@Cantidad", MySqlDbType.Decimal).Value = detalle.Cantidad;
                                cmd2.Parameters.Add("@Total", MySqlDbType.Decimal).Value = detalle.Total;
                                cmd2.ExecuteNonQuery(); //porque no devuelve solo insert
                            }

                            //PARA EXISTENCIA DE PRODUCTO

                        }

                    }
                    catch (System.Exception)
                    {
                        inserto = false; //no inserto
                        transaction.Rollback(); //tendria un rollback (devuelve a la base de datos a donde quedo)
                    }


                }



            }
            catch (System.Exception)
            {

            }
            return inserto;
        }
    }
}
