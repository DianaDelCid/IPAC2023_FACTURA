using System;
using System.Windows.Forms;

namespace Vista
{
    public partial class Login : Form
    {
        public Login()
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

        }
    }
}
