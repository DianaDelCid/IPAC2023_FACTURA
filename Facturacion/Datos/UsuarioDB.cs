using Entidades;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Text;

namespace Datos
{
    public class UsuarioDB
    {
        //Cadena de conexion, campos donde esta el servidor
        string cadena = "server=localhost; user=root; database=factura; password=diana21;";

        //Metodos para poder interactuar con la tabla usuario
        public Usuario Autenticar(Login login) //resive un objeto de la clase login y devuelve todo el objeto usuario
        {
            Usuario user = null; //objeto user con valor null VACIO
            try //sentencia para capturar errores
            {
                //CODIGO PARA TRAER UN USUARIO Y VER SI EXISTE
                //Sentencia SQL
                StringBuilder sql = new StringBuilder();
                sql.Append("SELECT * FROM usuario WHERE CodigoUsuario = @CodigoUsuario AND Contrasena = @Contrasena;"); //devuelve todos lo usuarios de la tabla usuarios

                using (MySqlConnection _conexion = new MySqlConnection(cadena)) //para cerrar la conexion a la base de datos
                {
                    _conexion.Open(); //abre la conexion
                    using (MySqlCommand comando = new MySqlCommand(sql.ToString(), _conexion))
                    {
                        comando.CommandType = CommandType.Text; //sentencia de texto
                        comando.Parameters.Add("@CodigoUsuario", MySqlDbType.VarChar, 50).Value = login.CodigoUsuario;
                        comando.Parameters.Add("@Contrasena", MySqlDbType.VarChar, 50).Value = login.Contraseña;

                        //ejecutar sentencia
                        MySqlDataReader dr = comando.ExecuteReader();
                        if (dr.Read())
                        {
                            user = new Usuario(); //instanciando user ya con datos

                            user.CodigoUsuario = dr["CodigoUsuario"].ToString();
                            user.Nombre = dr["Nombre"].ToString();
                            user.Contraseña = dr["Contrasena"].ToString();
                            user.Correo = dr["Correo"].ToString();
                            user.Rol = dr["Rol"].ToString();
                            user.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"]); //convertir en datatime
                            user.EstadoActivo = Convert.ToBoolean(dr["EstaActivo"]); //convertir en boolean
                            //ponerlo de ultimo por imagen es null al principio
                            if (dr["Foto"].GetType() != typeof(DBNull)) //si tipo de dato es distinto de null
                            {
                                user.Foto = (byte[])dr["Foto"]; //este es un arreglo de byte
                            }
                        }
                    }
                } //al terminar la sentencia cierra la conexion

            }
            catch (System.Exception ex) //aqui devuelve el error si existe
            {

            }


            return user; //retornamos el objeto
        }

        //METODO PARA PERMITIR INGRESAR UN NUEVO USUARIO
        public bool Insertar(Usuario user)
        {
            bool inserto = false; //variable bool iniciada como falsa
            try //sentencia para capturar errores
            {
                //CODIGO PARA TRAER UN USUARIO Y VER SI EXISTE
                //Sentencia SQL para intsertar un registro
                StringBuilder sql = new StringBuilder();
                sql.Append("INSERT INTO usuario VALUES"); //para insertar en la tabla usuarios
                sql.Append(" (@CodigoUsuario, @Nombre, @Contrasena, @Correo, @Rol, @Foto, @FechaCreacion, @EstaActivo);"); //colocarlos en orden como en la tabla de sql

                using (MySqlConnection _conexion = new MySqlConnection(cadena)) //para cerrar la conexion a la base de datos
                {
                    _conexion.Open(); //abre la conexion
                    using (MySqlCommand comando = new MySqlCommand(sql.ToString(), _conexion))
                    {
                        comando.CommandType = CommandType.Text; //sentencia de los parametros 
                        comando.Parameters.Add("@CodigoUsuario", MySqlDbType.VarChar, 50).Value = user.CodigoUsuario;
                        comando.Parameters.Add("@Nombre", MySqlDbType.VarChar, 50).Value = user.Nombre;
                        comando.Parameters.Add("@Contrasena", MySqlDbType.VarChar, 80).Value = user.Contraseña;
                        comando.Parameters.Add("@Correo", MySqlDbType.VarChar, 45).Value = user.Correo;
                        comando.Parameters.Add("@Rol", MySqlDbType.VarChar, 20).Value = user.Rol;
                        comando.Parameters.Add("@Foto", MySqlDbType.LongBlob).Value = user.Foto;
                        comando.Parameters.Add("@FechaCreacion", MySqlDbType.DateTime).Value = user.FechaCreacion;
                        comando.Parameters.Add("@EstaActivo", MySqlDbType.Bit).Value = user.EstadoActivo;

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

        //METODO PARA EDITAR USUARIO
        public bool Editar(Usuario user)
        {
            bool idito = false; //variable bool iniciada como falsa
            try //sentencia para capturar errores
            {
                //CODIGO PARA TRAER UN USUARIO Y VER SI EXISTE
                //Sentencia SQL para iditar un registro
                StringBuilder sql = new StringBuilder();
                sql.Append(" UPDATE usuario SET "); //para iditar en la tabla usuarios
                sql.Append(" Nombre = @Nombre, Contrasena = @Contrasena, Correo = @Correo, Rol = @Rol, Foto = @Foto, EstaActivo = @EstaActivo"); //campos a editar en sql
                sql.Append(" WHERE CodigoUsuario = @CodigoUsuario; "); //para ver que usuario se va a modificar

                using (MySqlConnection _conexion = new MySqlConnection(cadena)) //para cerrar la conexion a la base de datos
                {
                    _conexion.Open(); //abre la conexion
                    using (MySqlCommand comando = new MySqlCommand(sql.ToString(), _conexion))
                    {
                        comando.CommandType = CommandType.Text; //sentencia de los parametros 
                        comando.Parameters.Add("@CodigoUsuario", MySqlDbType.VarChar, 50).Value = user.CodigoUsuario;
                        comando.Parameters.Add("@Nombre", MySqlDbType.VarChar, 50).Value = user.Nombre;
                        comando.Parameters.Add("@Contrasena", MySqlDbType.VarChar, 80).Value = user.Contraseña;
                        comando.Parameters.Add("@Correo", MySqlDbType.VarChar, 45).Value = user.Correo;
                        comando.Parameters.Add("@Rol", MySqlDbType.VarChar, 20).Value = user.Rol;
                        comando.Parameters.Add("@Foto", MySqlDbType.LongBlob).Value = user.Foto;
                        comando.Parameters.Add("@EstaActivo", MySqlDbType.Bit).Value = user.EstadoActivo;

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
        public bool Eliminar(string codigoUsuario)
        {
            bool elimino = false; //variable bool iniciada como falsa
            try //sentencia para capturar errores
            {
                //CODIGO PARA TRAER UN USUARIO Y VER SI EXISTE
                //Sentencia SQL para eliminar un registro
                StringBuilder sql = new StringBuilder();
                sql.Append(" DELETE FROM usuario"); //para eliminar en la tabla usuarios
                sql.Append(" WHERE CodigoUsuario = @CodigoUsuario; "); //para ver que usuario se va a eliminar

                using (MySqlConnection _conexion = new MySqlConnection(cadena)) //para cerrar la conexion a la base de datos
                {
                    _conexion.Open(); //abre la conexion
                    using (MySqlCommand comando = new MySqlCommand(sql.ToString(), _conexion))
                    {
                        comando.CommandType = CommandType.Text; //sentencia de los parametros 
                        comando.Parameters.Add("@CodigoUsuario", MySqlDbType.VarChar, 50).Value = codigoUsuario;
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
        public DataTable DevolverUsuarios()
        {
            DataTable dt = new DataTable(); //un DataTable
            try //sentencia para capturar errores
            {
                //CODIGO PARA TRAER UN USUARIO Y VER SI EXISTE
                //Sentencia SQL para traer los registros
                StringBuilder sql = new StringBuilder();
                sql.Append(" SELECT *FROM usuario"); //para traer todos los registros

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

        //METODO PARA TRAER LA IMAGEN DEL USUARIO
        public byte[] DevolverFoto(string codigoUsuario)
        {
            byte[] foto = new byte[0];

            try //sentencia para capturar errores
            {
                //CODIGO PARA TRAER UN USUARIO Y VER SI EXISTE
                //Sentencia SQL para traer los registros
                StringBuilder sql = new StringBuilder();
                sql.Append(" SELECT Foto FROM usuario WHERE CodigoUsuario = @CodigoUsuario"); //para traer todos los registros

                using (MySqlConnection _conexion = new MySqlConnection(cadena)) //para cerrar la conexion a la base de datos
                {
                    _conexion.Open(); //abre la conexion
                    using (MySqlCommand comando = new MySqlCommand(sql.ToString(), _conexion))
                    {
                        comando.CommandType = CommandType.Text; //sentencia de los parametros 
                        comando.Parameters.Add("@CodigoUsuario", MySqlDbType.VarChar, 50).Value = codigoUsuario;
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



    }
}
