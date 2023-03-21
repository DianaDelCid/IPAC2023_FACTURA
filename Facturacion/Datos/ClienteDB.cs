using Entidades;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Text;

namespace Datos
{
    public class ClienteDB
    {
        //Cadena de conexion, campos donde esta el servidor
        string cadena = "server=localhost; user=root; database=factura; password=diana21;";

        //METODO PARA PERMITIR REGISTRAR CLIENTES
        public bool Insertar(Cliente cliente) //parametro (Cliente-objeto de la clase del proyecto entidades)
        {
            bool inserto = false; //variable bool iniciada como falsa
            try //sentencia para capturar errores
            {
                //Sentencia SQL para insertar un registro
                StringBuilder sql = new StringBuilder();
                sql.Append("INSERT INTO cliente VALUES"); //para insertar en la tabla usuarios
                sql.Append(" (@Identidad, @Nombre, @Telefono, @Correo, @Direccion, @FechaNacimiento, @EstaActivo, @Foto);"); //colocarlos en orden como en la tabla de sql

                using (MySqlConnection _conexion = new MySqlConnection(cadena)) //para cerrar la conexion a la base de datos
                {
                    _conexion.Open(); //abre la conexion
                    using (MySqlCommand comando = new MySqlCommand(sql.ToString(), _conexion))
                    {
                        comando.CommandType = CommandType.Text; //sentencia de los parametros 
                        comando.Parameters.Add("@Identidad", MySqlDbType.VarChar, 25).Value = cliente.Identidad;
                        comando.Parameters.Add("@Nombre", MySqlDbType.VarChar, 50).Value = cliente.Nombre;
                        comando.Parameters.Add("@Telefono", MySqlDbType.VarChar, 15).Value = cliente.Telefono;
                        comando.Parameters.Add("@Correo", MySqlDbType.VarChar, 45).Value = cliente.Correo;
                        comando.Parameters.Add("@Direccion", MySqlDbType.VarChar, 100).Value = cliente.Direccion;
                        comando.Parameters.Add("@FechaNacimiento", MySqlDbType.DateTime).Value = cliente.FechaNacimiento;
                        comando.Parameters.Add("@EstaActivo", MySqlDbType.Bit).Value = cliente.EstaActivo;
                        comando.Parameters.Add("@Foto", MySqlDbType.LongBlob).Value = cliente.Foto;
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

        //METODO PARA EDITAR OMODIFICAR CLIENTE
        public bool Editar(Cliente cliente)
        {
            bool edito = false; //variable bool iniciada como falsa
            try //sentencia para capturar errores
            {
                //Sentencia SQL para iditar un registro
                StringBuilder sql = new StringBuilder();
                sql.Append(" UPDATE cliente SET "); //para iditar en la tabla usuarios
                sql.Append(" Nombre = @Nombre, Telefono = @Telefono, Correo = @Correo, Direccion = @Direccion, FechaNacimiento = @FechaNacimiento, EstaActivo = @EstaActivo, Foto = @Foto"); //campos a editar en sql
                sql.Append(" WHERE Identidad = @Identidad; "); //para ver que usuario se va a modificar

                using (MySqlConnection _conexion = new MySqlConnection(cadena)) //para cerrar la conexion a la base de datos
                {
                    _conexion.Open(); //abre la conexion
                    using (MySqlCommand comando = new MySqlCommand(sql.ToString(), _conexion))
                    {
                        comando.CommandType = CommandType.Text; //sentencia de los parametros 
                        comando.Parameters.Add("@Identidad", MySqlDbType.VarChar, 25).Value = cliente.Identidad;
                        comando.Parameters.Add("@Nombre", MySqlDbType.VarChar, 50).Value = cliente.Nombre;
                        comando.Parameters.Add("@Telefono", MySqlDbType.VarChar, 15).Value = cliente.Telefono;
                        comando.Parameters.Add("@Correo", MySqlDbType.VarChar, 45).Value = cliente.Correo;
                        comando.Parameters.Add("@Direccion", MySqlDbType.VarChar, 100).Value = cliente.Direccion;
                        comando.Parameters.Add("@FechaNacimiento", MySqlDbType.DateTime).Value = cliente.FechaNacimiento;
                        comando.Parameters.Add("@EstaActivo", MySqlDbType.Bit).Value = cliente.EstaActivo;
                        comando.Parameters.Add("@Foto", MySqlDbType.LongBlob).Value = cliente.Foto;
                        comando.ExecuteNonQuery(); //no vamos a ejecutar, no devuelve ningun registro 
                        edito = true; //si inserta es true(verdadero)
                    }
                } //al terminar la sentencia cierra la conexion

            }
            catch (System.Exception ex) //aqui devuelve el error si existe
            {

            }
            return edito;

        }

        //METODO PARA ELIMINAR CLIENTE
        public bool Eliminar(string identidad)
        {
            bool elimino = false; //variable bool iniciada como falsa
            try //sentencia para capturar errores
            {
                //Sentencia SQL para eliminar un registro
                StringBuilder sql = new StringBuilder();
                sql.Append(" DELETE FROM cliente"); //para eliminar en la tabla usuarios
                sql.Append(" WHERE Identidad = @Identidad; "); //para ver que usuario se va a eliminar

                using (MySqlConnection _conexion = new MySqlConnection(cadena)) //para cerrar la conexion a la base de datos
                {
                    _conexion.Open(); //abre la conexion
                    using (MySqlCommand comando = new MySqlCommand(sql.ToString(), _conexion))
                    {
                        comando.CommandType = CommandType.Text; //sentencia de los parametros 
                        comando.Parameters.Add("@Identidad", MySqlDbType.VarChar, 25).Value = identidad;
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

        //METODO QUE PERMITE TRAER TODOS LOS CLIENTES EN DATAGRIP
        public DataTable DevolverClientes()
        {
            DataTable dt = new DataTable(); //un DataTable
            try //sentencia para capturar errores
            {
                //Sentencia SQL para traer los registros
                StringBuilder sql = new StringBuilder();
                sql.Append(" SELECT *FROM cliente"); //para traer todos los registros

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

        //METODO PARA TRAER LA IMAGEN DEL CLIENTE
        public byte[] DevolverFoto(string identidad)
        {
            byte[] foto = new byte[0];

            try //sentencia para capturar errores
            {
                //Sentencia SQL para traer los registros
                StringBuilder sql = new StringBuilder();
                sql.Append(" SELECT Foto FROM cliente WHERE Identidad = @Identidad"); //para traer todos los registros

                using (MySqlConnection _conexion = new MySqlConnection(cadena)) //para cerrar la conexion a la base de datos
                {
                    _conexion.Open(); //abre la conexion
                    using (MySqlCommand comando = new MySqlCommand(sql.ToString(), _conexion))
                    {
                        comando.CommandType = CommandType.Text; //sentencia de los parametros 
                        comando.Parameters.Add("@Identidad", MySqlDbType.VarChar, 25).Value = identidad;
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


        //METODO PARA DEVOLVER CLIENTE QUE SIRVE PARA FACTURA
        public Cliente DevolverClientePorIdentidad(string identidad)
        {
            Cliente cliente = null;
            try //sentencia para capturar errores
            {
                //Sentencia SQL para traer los registros
                StringBuilder sql = new StringBuilder();
                sql.Append(" SELECT *FROM cliente WHERE Identidad = @Identidad;"); //para traer todos los registros

                using (MySqlConnection _conexion = new MySqlConnection(cadena)) //para cerrar la conexion a la base de datos
                {
                    _conexion.Open(); //abre la conexion
                    using (MySqlCommand comando = new MySqlCommand(sql.ToString(), _conexion))
                    {
                        comando.CommandType = CommandType.Text; //sentencia de los parametros 
                        MySqlDataReader dr = comando.ExecuteReader(); //DataRider

                        if (dr.Read())
                        {
                            cliente = new Cliente();

                            cliente.Identidad = identidad;
                            cliente.Nombre = dr["Nombre"].ToString();
                            cliente.Telefono = dr["Telefono"].ToString();
                            cliente.Correo = dr["Correo"].ToString();
                            cliente.Direccion = dr["Direccion"].ToString();
                            cliente.FechaNacimiento = Convert.ToDateTime(dr["FechaNacimiento"]);
                            cliente.EstaActivo = Convert.ToBoolean(dr["Nombre"]);

                        }
                    }
                } //al terminar la sentencia cierra la conexion

            }
            catch (System.Exception ex) //aqui devuelve el error si existe
            {

            }
            return cliente; //retornamos cliente

        }


    }
}
