using Datos;
using Entidades;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Vista
{
    //Agrega formulario pero con herencia de Syncfusion.Windows.Forms.Office2007Form no de FORM
    public partial class UsuariosForm : Syncfusion.Windows.Forms.Office2010Form
    {
        public UsuariosForm()
        {
            InitializeComponent();
        }

        string tipoOperacion; //variable global para ver la operacion
        //si viene de modificar un usuario o de un usuario nuevo

        DataTable dt = new DataTable(); //variable global, es el que va a resivir el metodo DevolverUsuarios
        UsuarioDB usuarioDB = new UsuarioDB(); //instanciacion de la clase de usuarioDB

        Usuario user = new Usuario(); //objeto para usuario

        //Metodo para habilitar los controles del form
        private void HabilitarControles()
        {
            CodigoTextBox.Enabled = true; //solo ponemos la propiedad en verdadero
            NombreTextBox.Enabled = true;
            ContraseñaTextBox.Enabled = true;
            CorreoTextBox.Enabled = true;
            RolComboBox.Enabled = true;
            EstaActivoCheckBox.Enabled = true;
            AdjuntarFotoButton.Enabled = true;
            GuardarButton.Enabled = true;
            CancelarButton.Enabled = true;
            ModificarButton.Enabled = false;
        }

        //Metodo para Deshabilitar los controles del form
        private void DeshabilitarControles()
        {
            CodigoTextBox.Enabled = false; //solo ponemos la propiedad en falso 
            NombreTextBox.Enabled = false;
            ContraseñaTextBox.Enabled = false;
            CorreoTextBox.Enabled = false;
            RolComboBox.Enabled = false;
            EstaActivoCheckBox.Enabled = false;
            AdjuntarFotoButton.Enabled = false;
            GuardarButton.Enabled = false;
            CancelarButton.Enabled = false;
            ModificarButton.Enabled = true;
        }
        private void LimpiarControles()
        {
            CodigoTextBox.Clear();
            NombreTextBox.Clear();
            ContraseñaTextBox.Clear();
            CorreoTextBox.Clear();
            RolComboBox.Text = "";
            EstaActivoCheckBox.Checked = false;
            FotoPictureBox.Image = null; //para borra la foto

        }
        private void NuevoButton_Click(object sender, System.EventArgs e)
        {
            CodigoTextBox.Focus();
            HabilitarControles();
            tipoOperacion = "Nuevo";
        }

        private void CancelarButton_Click(object sender, System.EventArgs e)
        {
            DeshabilitarControles();
            LimpiarControles();
        }

        private void GuardarButton_Click(object sender, System.EventArgs e)
        {
            if (tipoOperacion == "Nuevo")
            {
                if (string.IsNullOrEmpty(CodigoTextBox.Text)) //Para validar si un string esta nulo o vacio
                {
                    errorProvider1.SetError(CodigoTextBox, "Ingrese un codigo");
                    CodigoTextBox.Focus();
                    return;
                }
                errorProvider1.Clear();
                if (string.IsNullOrEmpty(NombreTextBox.Text)) //Para validar si un string esta nulo o vacio
                {
                    errorProvider1.SetError(NombreTextBox, "Ingrese el nombre");
                    CodigoTextBox.Focus();
                    return;
                }
                errorProvider1.Clear();
                if (string.IsNullOrEmpty(ContraseñaTextBox.Text)) //Para validar si un string esta nulo o vacio
                {
                    errorProvider1.SetError(ContraseñaTextBox, "Ingrese contraseña");
                    ContraseñaTextBox.Focus();
                    return;
                }
                errorProvider1.Clear();
                if (string.IsNullOrEmpty(RolComboBox.Text)) //Para validar si un string esta nulo o vacio
                {
                    errorProvider1.SetError(RolComboBox, "Ingrese el rol");
                    RolComboBox.Focus();
                    return;
                }
                errorProvider1.Clear();

                user.CodigoUsuario = CodigoTextBox.Text;
                user.Nombre = NombreTextBox.Text;
                user.Contraseña = ContraseñaTextBox.Text;
                user.Rol = RolComboBox.Text;
                user.Correo = CorreoTextBox.Text;
                user.EstadoActivo = EstaActivoCheckBox.Checked;

                if (FotoPictureBox.Image != null) //si es distinto a null si tiene imagen
                {
                    //la propiedad de foto es un arreglo de byte asi que hay que convertirla la imagen a arreglo de byte
                    System.IO.MemoryStream ms = new System.IO.MemoryStream(); //instanciamos un nuevo obejto de memory

                    //al ms le pasamos la foto que tiene el picureBox
                    FotoPictureBox.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);

                    //Y para pasarlo a la propiedad
                    user.Foto = ms.GetBuffer();
                }

                //Insertar en la Base de Datos
                bool inserto = usuarioDB.Insertar(user); //variable boleana que trae el metodo

                //condicion 
                if (inserto)
                {
                    LimpiarControles();
                    DeshabilitarControles();
                    TraerUsuarios(); //llamamos al metodo traer usuario
                    MessageBox.Show("Registro Guardado");
                }
                else
                {
                    MessageBox.Show("No se pudo guardar el registro");
                }

            }
            else if (tipoOperacion == "Modificar")
            {

                //vuelve a pasar las propiedades al objeto user
                user.CodigoUsuario = CodigoTextBox.Text;
                user.Nombre = NombreTextBox.Text;
                user.Contraseña = ContraseñaTextBox.Text;
                user.Rol = RolComboBox.Text;
                user.Correo = CorreoTextBox.Text;
                user.EstadoActivo = EstaActivoCheckBox.Checked;

                if (FotoPictureBox.Image != null) //si es distinto a null si tiene imagen
                {
                    //la propiedad de foto es un arreglo de byte asi que hay que convertirla la imagen a arreglo de byte
                    System.IO.MemoryStream ms = new System.IO.MemoryStream(); //instanciamos un nuevo obejto de memory

                    //al ms le pasamos la foto que tiene el picureBox
                    FotoPictureBox.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);

                    //Y para pasarlo a la propiedad
                    user.Foto = ms.GetBuffer();
                }

                bool modifico = usuarioDB.Editar(user);
                if (modifico)
                {
                    LimpiarControles();
                    DeshabilitarControles();
                    TraerUsuarios();
                    MessageBox.Show("Registro actualizado correctamente");
                }
                else
                {
                    MessageBox.Show("No se pudo actualizar el registro");
                }
            }
        }

        private void ModificarButton_Click(object sender, System.EventArgs e)
        {
            tipoOperacion = "Modificar";
            //validar si seleciona 1
            if (UsuariosDataGridView.SelectedRows.Count > 0) //optiene la fila que el usuario a selecccionado
            {
                //pasamos cada una de las celdas a los textbox
                CodigoTextBox.Text = UsuariosDataGridView.CurrentRow.Cells["CodigoUsuario"].Value.ToString();
                NombreTextBox.Text = UsuariosDataGridView.CurrentRow.Cells["Nombre"].Value.ToString();
                ContraseñaTextBox.Text = UsuariosDataGridView.CurrentRow.Cells["Contrasena"].Value.ToString();
                CorreoTextBox.Text = UsuariosDataGridView.CurrentRow.Cells["Correo"].Value.ToString();
                RolComboBox.Text = UsuariosDataGridView.CurrentRow.Cells["Rol"].Value.ToString();
                EstaActivoCheckBox.Checked = Convert.ToBoolean(UsuariosDataGridView.CurrentRow.Cells["EstaActivo"].Value);

                byte[] miFoto = usuarioDB.DevolverFoto(UsuariosDataGridView.CurrentRow.Cells["CodigoUsuario"].Value.ToString());

                if (miFoto.Length > 0) //si el arreglo es mayor de 0 byte
                {
                    MemoryStream ms = new MemoryStream(miFoto);
                    FotoPictureBox.Image = System.Drawing.Bitmap.FromStream(ms); //pasamos la imagen al picturebox
                }

                HabilitarControles(); //y habilitamos los controles

            }
            else
            {
                MessageBox.Show("Debe seleccionar un registro");
            }
        }

        private void AdjuntarFotoButton_Click(object sender, System.EventArgs e)
        {
            //Abre una ventana para abjuntar openFileDialog es una clase 
            OpenFileDialog dialog = new OpenFileDialog();

            //Mostrar la ventana y capturar lo que el usuario selecciono
            DialogResult resultado = dialog.ShowDialog();

            //Condicion para evaluar si usuario selecciono imagen
            if (resultado == DialogResult.OK) //Si resultado fue sactifactorio
            {
                //a la clase Image asignamos el metodo FromFile convierte el tipo de imagen y le pasamos dialog el archivo quqe trae el dialog y se lo pasa al pictubox
                FotoPictureBox.Image = Image.FromFile(dialog.FileName);//file name para capturar nombre del archivo
            }

        }

        //EVENTO LOAD PARA CARGAR LOS USUARIOS
        private void UsuariosForm_Load(object sender, System.EventArgs e)
        {
            TraerUsuarios();
        }

        private void TraerUsuarios()
        {
            dt = usuarioDB.DevolverUsuarios(); //traemos el metodo
            UsuariosDataGridView.DataSource = dt; //se lo asignamos al datagripview
        }

        private void EliminarButton_Click(object sender, EventArgs e)
        {
            //validar si seleccion 1 registro
            if (UsuariosDataGridView.SelectedRows.Count > 0) //optiene la fila que el usuario a selecccionado
            {
                DialogResult resultado = MessageBox.Show("Esta seguro de eliminar el registro", "Advertencia", MessageBoxButtons.YesNo); //ventana para verificar

                if (resultado == DialogResult.Yes) //si es un si
                {
                    bool elimino = usuarioDB.Eliminar(UsuariosDataGridView.CurrentRow.Cells["CodigoUsuario"].Value.ToString());

                    if (elimino)
                    {
                        LimpiarControles();
                        DeshabilitarControles();
                        TraerUsuarios();
                        MessageBox.Show("Registro Eliminado");
                    }
                    else
                    {
                        MessageBox.Show("No se pudo eliminar registro");
                    }
                }
                //si no no hace nada
            }
            else
            {
                MessageBox.Show("Debe seleccionar un registro");
            }
        }
    }
}
