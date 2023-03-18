using Entidades;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Text;

namespace Datos
{
    public class ProductoDB
    {
        //Cadena de conexion, campos donde esta el servidor
        string cadena = "server=localhost; user=root; database=factura; password=diana21;";

        //METODO PARA PERMITIR INGRESAR UN NUEVO PRODUCTO
        public bool Insertar(Producto producto) //parametro (producto-objeto de la clase del proyecto entidades)
        {
            bool inserto = false; //variable bool iniciada como falsa
            try //sentencia para capturar errores
            {
                //CODIGO PARA TRAER UN PRODUCTO Y VER SI EXISTE
                //Sentencia SQL para intsertar un registro
                StringBuilder sql = new StringBuilder();
                sql.Append("INSERT INTO producto VALUES"); //para insertar en la tabla usuarios
                sql.Append(" (@Codigo, @Descripcion, @Existencia, @Precio, @Foto, @EstaActivo);"); //colocarlos en orden como en la tabla de sql

                using (MySqlConnection _conexion = new MySqlConnection(cadena)) //para cerrar la conexion a la base de datos
                {
                    _conexion.Open(); //abre la conexion
                    using (MySqlCommand comando = new MySqlCommand(sql.ToString(), _conexion))
                    {
                        comando.CommandType = CommandType.Text; //sentencia de los parametros 
                        comando.Parameters.Add("@Codigo", MySqlDbType.VarChar, 80).Value = producto.Codigo;
                        comando.Parameters.Add("@Descripcion", MySqlDbType.VarChar, 200).Value = producto.Descripcion;
                        comando.Parameters.Add("@Existencia", MySqlDbType.Int32).Value = producto.Existencia;
                        comando.Parameters.Add("@Precio", MySqlDbType.Decimal).Value = producto.Precio;
                        comando.Parameters.Add("@Foto", MySqlDbType.LongBlob).Value = producto.Foto;
                        comando.Parameters.Add("@EstaActivo", MySqlDbType.Bit).Value = producto.EstaActivo;

                        comando.ExecuteNonQuery(); //no vamos a ejecutar, no devuelve ningun registro 
                        inserto = true; //si inserta es true(verdadero)
                    }
                } //al terminar la sentencia cierra la conexion
            }
            catch (System.Exception ex) //aqui devuelve el error si existe
            {

            }
            return inserto;
        }

        //METODO PARA EDITAR O REGISTRAR PRODUCTO
        public bool Editar(Producto producto)
        {
            bool idito = false; //variable bool iniciada como falsa
            try //sentencia para capturar errores
            {
                //CODIGO PARA TRAER UN USUARIO Y VER SI EXISTE
                //Sentencia SQL para iditar un registro
                StringBuilder sql = new StringBuilder();
                sql.Append(" UPDATE producto SET "); //para iditar en la tabla usuarios
                sql.Append(" Descripcion = @Descripcion, Existencia = @Existencia, Precio = @Precio, Foto = @Foto, EstaActivo = @EstaActivo"); //campos a editar en sql
                sql.Append(" WHERE Codigo = @Codigo; "); //para ver que usuario se va a modificar

                using (MySqlConnection _conexion = new MySqlConnection(cadena)) //para cerrar la conexion a la base de datos
                {
                    _conexion.Open(); //abre la conexion
                    using (MySqlCommand comando = new MySqlCommand(sql.ToString(), _conexion))
                    {
                        comando.CommandType = CommandType.Text; //sentencia de los parametros 
                        comando.Parameters.Add("@Codigo", MySqlDbType.VarChar, 80).Value = producto.Codigo;
                        comando.Parameters.Add("@Descripcion", MySqlDbType.VarChar, 200).Value = producto.Descripcion;
                        comando.Parameters.Add("@Existencia", MySqlDbType.Int32).Value = producto.Existencia;
                        comando.Parameters.Add("@Precio", MySqlDbType.Decimal).Value = producto.Precio;
                        comando.Parameters.Add("@Foto", MySqlDbType.LongBlob).Value = producto.Foto;
                        comando.Parameters.Add("@EstaActivo", MySqlDbType.Bit).Value = producto.EstaActivo;
                        comando.ExecuteNonQuery(); //no vamos a ejecutar, no devuelve ningun registro 
                        idito = true; //si inserta es true(verdadero)
                    }
                } //al terminar la sentencia cierra la conexion

            }
            catch (System.Exception ex) //aqui devuelve el error si existe
            {

            }
            return idito;

        }

        //METODO PARA ELIMINAR USUARIO
        public bool Eliminar(string codigo)
        {
            bool elimino = false; //variable bool iniciada como falsa
            try //sentencia para capturar errores
            {
                //Sentencia SQL para eliminar un registro
                StringBuilder sql = new StringBuilder();
                sql.Append(" DELETE FROM producto"); //para eliminar en la tabla usuarios
                sql.Append(" WHERE Codigo = @Codigo; "); //para ver que usuario se va a eliminar

                using (MySqlConnection _conexion = new MySqlConnection(cadena)) //para cerrar la conexion a la base de datos
                {
                    _conexion.Open(); //abre la conexion
                    using (MySqlCommand comando = new MySqlCommand(sql.ToString(), _conexion))
                    {
                        comando.CommandType = CommandType.Text; //sentencia de los parametros 
                        comando.Parameters.Add("@Codigo", MySqlDbType.VarChar, 80).Value = codigo;
                        comando.ExecuteNonQuery(); //no vamos a ejecutar, no devuelve ningun registro 
                        elimino = true; //si elimina es true(verdadero)
                    }
                } //al terminar la sentencia cierra la conexion
            }
            catch (System.Exception ex) //aqui devuelve el error si existe
            {

            }
            return elimino;

        }

        //METODO QUE PERMITE TRAER TODOS LOS USUARIOS EN DATAGRIP
        public DataTable DevolverProductos()
        {
            DataTable dt = new DataTable(); //un DataTable
            try //sentencia para capturar errores
            {
                //CODIGO PARA TRAER UN USUARIO Y VER SI EXISTE
                //Sentencia SQL para traer los registros
                StringBuilder sql = new StringBuilder();
                sql.Append(" SELECT *FROM producto"); //para traer todos los registros

                using (MySqlConnection _conexion = new MySqlConnection(cadena)) //para cerrar la conexion a la base de datos
                {
                    _conexion.Open(); //abre la conexion
                    using (MySqlCommand comando = new MySqlCommand(sql.ToString(), _conexion))
                    {
                        comando.CommandType = CommandType.Text; //sentencia de los parametros 
                        MySqlDataReader dr = comando.ExecuteReader(); //DataRider
                        dt.Load(dr); //se lo pasamos al datatable
                    }
                } //al terminar la sentencia cierra la conexion

            }
            catch (System.Exception ex) //aqui devuelve el error si existe
            {

            }
            return dt;

        }

        //METODO PARA TRAER LA IMAGEN DEL PRODUCTO
        public byte[] DevolverFoto(string codigo)
        {
            byte[] foto = new byte[0];

            try //sentencia para capturar errores
            {
                //Sentencia SQL para traer los registros
                StringBuilder sql = new StringBuilder();
                sql.Append(" SELECT Foto FROM producto WHERE Codigo = @Codigo"); //para traer todos los registros

                using (MySqlConnection _conexion = new MySqlConnection(cadena)) //para cerrar la conexion a la base de datos
                {
                    _conexion.Open(); //abre la conexion
                    using (MySqlCommand comando = new MySqlCommand(sql.ToString(), _conexion))
                    {
                        comando.CommandType = CommandType.Text; //sentencia de los parametros 
                        comando.Parameters.Add("@Codigo", MySqlDbType.VarChar, 80).Value = codigo;
                        MySqlDataReader dr = comando.ExecuteReader(); //DataRider

                        //validacion si hay algo
                        if (dr.Read())
                        {
                            foto = (byte[])dr["Foto"]; //le pasamos la foto
                        }
                    }
                } //al terminar la sentencia cierra la conexion

            }
            catch (System.Exception ex) //aqui devuelve el error si existe
            {

            }
            return foto;
        }

        //METODO PARA DEVOLVER PRODUCTO QUE SIRVE PARA FACTURA
        public Producto DevolverProductoPorCodigo(string codigo)
        {
            Producto producto = null;
            try //sentencia para capturar errores
            {
                //Sentencia SQL para traer los registros
                StringBuilder sql = new StringBuilder();
                sql.Append(" SELECT *FROM producto WHERE Codigo = @Codigo;"); //para traer todos los registros

                using (MySqlConnection _conexion = new MySqlConnection(cadena)) //para cerrar la conexion a la base de datos
                {
                    _conexion.Open(); //abre la conexion
                    using (MySqlCommand comando = new MySqlCommand(sql.ToString(), _conexion))
                    {
                        comando.CommandType = CommandType.Text; //sentencia de los parametros 
                        MySqlDataReader dr = comando.ExecuteReader(); //DataRider

                        if (dr.Read())
                        {
                            producto = new Producto();

                            producto.Codigo = codigo;
                            producto.Descripcion = dr["Descripcion"].ToString();
                            producto.Existencia = Convert.ToInt32(dr["Existencia"]);
                            producto.Precio = Convert.ToDecimal(dr["Precio"]);
                            producto.EstaActivo = Convert.ToBoolean(dr["EstaActivo"]);
                        }
                    }
                } //al terminar la sentencia cierra la conexion

            }
            catch (System.Exception ex) //aqui devuelve el error si existe
            {

            }
            return producto; //retornamos cliente

        }

    }
}
