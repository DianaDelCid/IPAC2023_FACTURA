namespace Entidades
{
    public class Producto
    {
        public string Codigo { get; set; } //propiedad llave
        public string Descripcion { get; set; }
        public int Existencia { get; set; }
        public decimal Precio { get; set; }
        public byte[] Imagen { get; set; }  //para agregar imagen del producto

        //Constructor vacio
        public Producto()
        {

        }

        //Constructor con parametros
        public Producto(string codigo, string descripcion, int existencia, decimal precio, byte[] imagen)
        {
            Codigo = codigo;
            Descripcion = descripcion;
            Existencia = existencia;
            Precio = precio;
            Imagen = imagen;
        }
    }
}
