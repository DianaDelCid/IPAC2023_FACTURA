using System;
using System.Windows.Forms;

namespace Vista
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void UsuariosToolStripButton_Click(object sender, EventArgs e)
        {
            //instanciar un objeto de la clase usuarios
            UsuariosForm userForm = new UsuariosForm();
            userForm.MdiParent = this; //propiedad que dice que va a tener un padre
            userForm.Show(); //mostrar el formulario
        }

        private void ProductosToolStripButton_Click(object sender, EventArgs e)
        {
            ProductosForm productosForm = new ProductosForm();
            productosForm.MdiParent = this;
            productosForm.Show();
        }

        //Para traer el fomulario de clientes
        private void ClientesToolStripButton_Click(object sender, EventArgs e)
        {
            ClientesForm clientesForm = new ClientesForm();
            clientesForm.MdiParent = this;
            clientesForm.Show();
        }

        private void NuevafacturaToolStripButton_Click(object sender, EventArgs e)
        {
            FacturaForm facturaForm = new FacturaForm();
            facturaForm.MdiParent = this;
            facturaForm.Show();
        }
    }
}
