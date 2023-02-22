using System;

namespace Entidades
{
    public class Usuario
    {
        //Propiedades
        public string CodigoUsuario { get; set; }
        public string Nombre { get; set; }
        public string Contraseña { get; set; }
        public string Correo { get; set; }
        public string Rol { get; set; }
        public DateTime FechaCreacion { get; set; }
        public bool EstadoActivo { get; set; } //propiedad para ver si usuario esta activo o no

        //Constructor vacio
        public Usuario()
        {
        }

        //Constructor con propiedaes
        public Usuario(string codigoUsuario, string nombre, string contraseña, string correo, string rol, DateTime fechaCreacion, bool estadoActivo)
        {
            CodigoUsuario = codigoUsuario;
            Nombre = nombre;
            Contraseña = contraseña;
            Correo = correo;
            Rol = rol;
            FechaCreacion = fechaCreacion;
            EstadoActivo = estadoActivo;
        }
    }
}
