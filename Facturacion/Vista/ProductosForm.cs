using System.Windows.Forms;

namespace Vista
{
    public partial class ProductosForm : Syncfusion.Windows.Forms.Office2010Form
    {
        public ProductosForm()
        {
            InitializeComponent();
        }

        string operacion; //Variable globar

        private void NuevoButton_Click(object sender, System.EventArgs e)
        {
            operacion = "Nuevo";
            HabilitarControles();
        }

        //Metodo para Habilitar los controles
        private void HabilitarControles()
        {
            CodigoTextBox.Enabled = true;
            DescripcionTextBox.Enabled = true;
            ExistenciaTextBox.Enabled = true;
            PrecioTextBox.Enabled = true;
            AdjuntarImagenButton.Enabled = true;
            GuardarButton.Enabled = true;
            CancelarButton.Enabled = true;
            NuevoButton.Enabled = false;
        }

        //Metodo para deshabiliatr controles
        private void DeshabilitarControles()
        {
            CodigoTextBox.Enabled = false;
            DescripcionTextBox.Enabled = false;
            ExistenciaTextBox.Enabled = false;
            PrecioTextBox.Enabled = false;
            AdjuntarImagenButton.Enabled = false;
            GuardarButton.Enabled = false;
            CancelarButton.Enabled = false;
            NuevoButton.Enabled = true;
        }

        private void CancelarButton_Click(object sender, System.EventArgs e)
        {
            DeshabilitarControles();
        }

        private void ModificarButton_Click(object sender, System.EventArgs e)
        {
            operacion = "Modificar";
        }

        private void GuardarButton_Click(object sender, System.EventArgs e)
        {
            if (operacion == "Nuevo") //Vamos a guardar el nuevo producto
            {
                if (string.IsNullOrEmpty(CodigoTextBox.Text))
                {
                    errorProvider1.SetError(CodigoTextBox, "Ingrese un código");
                    CodigoTextBox.Focus();
                    return;
                }
                errorProvider1.Clear();

                if (string.IsNullOrEmpty(ExistenciaTextBox.Text))
                {
                    errorProvider1.SetError(ExistenciaTextBox, "Ingrese una Existencia");
                    ExistenciaTextBox.Focus();
                    return;
                }
                errorProvider1.Clear();
                if (string.IsNullOrEmpty(PrecioTextBox.Text))
                {
                    errorProvider1.SetError(PrecioTextBox, "Ingrese un Precio");
                    PrecioTextBox.Focus();
                    return;
                }
                errorProvider1.Clear();

                if (string.IsNullOrEmpty(CodigoTextBox.Text))
                {
                    errorProvider1.SetError(CodigoTextBox, "Ingrese un codigo");
                    CodigoTextBox.Focus();
                    return;
                }
                errorProvider1.Clear();

                //


            }
        }

        private void ExistenciaTextBox_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            //Validamos que no permita que ingrese letras solo numeros
            if (char.IsNumber(e.KeyChar)) //si este caracter no es un numero (!)negando number es numerico
            {

            }
            else
            {
                e.Handled = true; //handled locancela, no lo colaca en el textbox
            }
        }

        private void PrecioTextBox_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            //Validamos que no permita que ingrese letras solo numeros y que permita ingresar punto(.) para decimal
            if (!char.IsDigit(e.KeyChar) && (e.KeyChar != '.')) //si este caracter no es un numero (!)negando isdigtal para decimales
            {
                e.Handled = true; //handled lo cancela, no lo coloca en el textbox
            }

            //validar que solo deje ingresar dos numeros despues del punto
            if ((e.KeyChar == '.') && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true; //handled lo cancela, no lo coloca en el textbox
            }
        }
    }
}
