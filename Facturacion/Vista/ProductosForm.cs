using Datos;
using Entidades;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Vista
{
    public partial class ProductosForm : Syncfusion.Windows.Forms.Office2010Form
    {
        public ProductosForm()
        {
            InitializeComponent();
        }

        string operacion; //Variable global
        //Objetos
        Producto producto; //solo lo declaramos
        ProductoDB productoDB = new ProductoDB();

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

        //Metodo para limpiar los controles
        private void LimpiarControles()
        {
            CodigoTextBox.Clear();
            DescripcionTextBox.Clear();
            ExistenciaTextBox.Clear();
            PrecioTextBox.Clear();
            EstaActivoCheckBox.Checked = false;
            ImagenPictureBox.Image = null;
            producto = null; //declaramos el objeto nulo para la imagen no se repita
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
            if (ProductosDataGridView.SelectedRows.Count > 0) //si usuario selecciono 1 registro
            {
                //pasamos todos los datos de la celdas en casa uno de los controles
                CodigoTextBox.Text = ProductosDataGridView.CurrentRow.Cells["Codigo"].Value.ToString();
                DescripcionTextBox.Text = ProductosDataGridView.CurrentRow.Cells["Descripcion"].Value.ToString();
                ExistenciaTextBox.Text = ProductosDataGridView.CurrentRow.Cells["Existencia"].Value.ToString();
                PrecioTextBox.Text = ProductosDataGridView.CurrentRow.Cells["Precio"].Value.ToString();
                EstaActivoCheckBox.Checked = Convert.ToBoolean(ProductosDataGridView.CurrentRow.Cells["EstaActivo"].Value);

                byte[] img = productoDB.DevolverFoto(ProductosDataGridView.CurrentRow.Cells["Codigo"].Value.ToString());

                if (img.Length > 0) //si el arreglo es mayor de 0 byte
                {
                    MemoryStream ms = new MemoryStream(img);
                    ImagenPictureBox.Image = System.Drawing.Bitmap.FromStream(ms); //pasamos la imagen al picturebox
                }
                HabilitarControles(); //y habilitamos los controles
                CodigoTextBox.ReadOnly = true; //para no permitir que usuario modifique este campo, ya que es una llave primaria
            }
            else
            {
                MessageBox.Show("Debe seleccionar un registro");
            }
        }

        private void GuardarButton_Click(object sender, System.EventArgs e)
        {
            producto = new Producto(); // aqui si ya lo instanciamos 
            //Ponerlo al inicio para no duplicar 
            producto.Codigo = CodigoTextBox.Text;
            producto.Descripcion = DescripcionTextBox.Text;
            producto.Precio = Convert.ToDecimal(PrecioTextBox.Text);
            producto.Existencia = Convert.ToInt32(ExistenciaTextBox.Text);
            producto.EstaActivo = EstaActivoCheckBox.Checked;

            if (ImagenPictureBox.Image != null) //si es distinto a null si tiene imagen
            {
                //la propiedad de foto es un arreglo de byte asi que hay que convertirla la imagen a arreglo de byte
                System.IO.MemoryStream ms = new System.IO.MemoryStream(); //instanciamos un nuevo obejto de memory

                //al ms le pasamos la foto que tiene el picureBox
                ImagenPictureBox.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);

                //Y para pasarlo a la propiedad
                producto.Foto = ms.GetBuffer();
            }

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

                bool inserto = productoDB.Insertar(producto); //variable que trae el metodo de insertar
                if (inserto)
                {
                    DeshabilitarControles();
                    LimpiarControles();
                    TraerProductos();
                    MessageBox.Show("Registro Guardado con exito", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("No se pudo guardar el registro", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (operacion == "Modificar")
            {
                bool modifico = productoDB.Editar(producto); //variable para traer el metodo editar
                if (modifico)
                {
                    CodigoTextBox.ReadOnly = false; //loponemos false porque si no nos permite poner codigo
                    DeshabilitarControles();
                    LimpiarControles();
                    TraerProductos();
                    MessageBox.Show("Registro actualizado con exito", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("No se pudo actualizar el registro", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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

        //evento key press del precio
        private void PrecioTextBox_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            //Validamos que no permita que ingrese letras solo numeros y que permita ingresar punto(.) para decimal
            if (!char.IsDigit(e.KeyChar) && (e.KeyChar != '.') && e.KeyChar != '\b') //si este caracter no es un numero (!)negando isdigtal para decimales
            {
                e.Handled = true; //handled lo cancela, no lo coloca en el textbox
            }

            //validar que solo deje ingresar dos numeros despues del punto
            if ((e.KeyChar == '.') && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true; //handled lo cancela, no lo coloca en el textbox
            }
        }

        private void AdjuntarImagenButton_Click(object sender, System.EventArgs e)
        {
            //Abre una ventana para abjuntar openFileDialog es una clase 
            OpenFileDialog dialog = new OpenFileDialog();

            //Mostrar la ventana y capturar lo que el usuario selecciono
            DialogResult resultado = dialog.ShowDialog();

            //Condicion para evaluar si usuario selecciono imagen
            if (resultado == DialogResult.OK) //Si resultado fue sactifactorio
            {
                //a la clase Image asignamos el metodo FromFile convierte el tipo de imagen y le pasamos dialog el archivo quqe trae el dialog y se lo pasa al pictubox
                ImagenPictureBox.Image = Image.FromFile(dialog.FileName);//file name para capturar nombre del archivo
            }
        }

        //evento Load del formulario quiere decir que los mostramos cuando carga el formulario
        private void ProductosForm_Load(object sender, EventArgs e)
        {
            TraerProductos(); //llamamos el metodo
        }

        //METODO PARA TRAER PRODUCTOS
        private void TraerProductos()
        {
            ProductosDataGridView.DataSource = productoDB.DevolverProductos(); //el data va a ser igual a la productodb metodo
        }

        private void EliminarButton_Click(object sender, EventArgs e)
        {
            //Validar que usuario seleccione un registro
            if (ProductosDataGridView.SelectedRows.Count > 0)
            {
                DialogResult resultado = MessageBox.Show("Esta seguro de eliminar el registro", "Advertencia", MessageBoxButtons.YesNo); //ventana para verificar

                if (resultado == DialogResult.Yes) //si es un si
                {
                    bool elimino = productoDB.Eliminar(ProductosDataGridView.CurrentRow.Cells["Codigo"].Value.ToString());

                    if (elimino)
                    {
                        LimpiarControles();
                        DeshabilitarControles();
                        TraerProductos();
                        MessageBox.Show("Registro Eliminado");
                    }
                    else
                    {
                        MessageBox.Show("No se pudo eliminar registro");
                    }
                }
                //si no no hace nada
            }
        }
    }
}
