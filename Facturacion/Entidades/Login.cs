using System;

namespace Entidades
{
    public class Login
    {
        //Propiedades
        public string CodigoUsuario { get; set; }
        public string Contraseña { get; set; }
        public string Rol { get; set; }
        public DateTime MyProperty { get; set; }

        //Constructor vacio, este siempre se debe crear 
        public Login()
        {
        }

        //Constructor con propiedades
        public Login(string codigoUsuario, string contraseña, string rol)
        {
            CodigoUsuario = codigoUsuario;
            Contraseña = contraseña;
            Rol = rol;
        }
    }
}
