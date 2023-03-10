using Datos;
using Entidades;
using System;
using System.Windows.Forms;

namespace Vista
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void CancelarButton_Click(object sender, EventArgs e)
        {
            Close(); //metodo para que cierre el programa
        }

        private void AceptarButton_Click(object sender, EventArgs e)
        {
            //Condicion para validar si usuario ingresa los datos
            if (UsuarioTextBox.Text == string.Empty) //si esta vacio
            {
                errorProvider1.SetError(UsuarioTextBox, "Ingrese un usuario");
                UsuarioTextBox.Focus();
                return; //retorna la ejecucion, cancela la ejecucion del evento click
            }
            errorProvider1.Clear(); //borrar en error 
            if (string.IsNullOrEmpty(ContraseñaTextBox.Text)) //otra manera de validar si esta nulo o vacio
            {
                errorProvider1.SetError(ContraseñaTextBox, "Ingrese contraseña");
                ContraseñaTextBox.Focus();
                return;
            }
            errorProvider1.Clear();

            //VALIDAR EN LA BASE DE DATOS
            //INSTANCIACION DE OBJETOS
            Login login = new Login(UsuarioTextBox.Text, ContraseñaTextBox.Text);
            Usuario usuario = new Usuario();
            UsuarioDB usuarioDB = new UsuarioDB();

            usuario = usuarioDB.Autenticar(login); //usuario igual a usuariodb. metodo y parametro login

            //validar 
            if (usuario != null) //si esdistinto de null 
            {
                if (usuario.EstadoActivo) //si usuaurio esta activo
                {
                    //Mostrar el Menu
                    Menu menuFormulario = new Menu(); //Instanciamos el fomulario Menu
                    Hide(); //hide (ocultar) Para ocultar el formulario login
                    menuFormulario.Show(); //accedemos a ese objeto con el metodo show que muestra el form
                }
                else
                {
                    MessageBox.Show("El usuario no esta activo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else //si no un mensaje de error
            {
                MessageBox.Show("Datos de usuario incorrecto", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }




        }

        private void MostrarContraseñaButton_Click(object sender, EventArgs e)
        {
            //vamos a validar si contraseña es igual a * en la propiedad passwordchar
            if (ContraseñaTextBox.PasswordChar == '*')
            {
                //entonces decimos que la propiedad password sea null utilizamos secuencias de escape \0 que es para igualar a valor nulable
                ContraseñaTextBox.PasswordChar = '\0';
            }
            else //sino que vuelva a ocultar la contraseña 
            {
                ContraseñaTextBox.PasswordChar = '*';
            }
        }
    }
}
