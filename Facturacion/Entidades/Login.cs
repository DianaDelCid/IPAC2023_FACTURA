namespace Entidades
{
    public class Login
    {
        //Propiedades
        public string CodigoUsuario { get; set; }
        public string Contraseña { get; set; }

        //Constructor vacio, este siempre se debe crear 
        public Login()
        {
        }

        //Constructor con propiedades
        public Login(string codigoUsuario, string contraseña)
        {
            CodigoUsuario = codigoUsuario;
            Contraseña = contraseña;
        }
    }
}
