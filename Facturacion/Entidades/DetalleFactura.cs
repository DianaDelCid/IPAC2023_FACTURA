namespace Entidades
{
    public class DetalleFactura
    {
        //Propiedades
        public int Id { get; set; }
        public int IdFactura { get; set; }
        public string CodigoProducto { get; set; }
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }
        public decimal Total { get; set; }

        //Constructor vacio
        public DetalleFactura()
        {
        }

        //Constructor con parametros
        public DetalleFactura(int id, int idFactura, string codigoProducto, decimal precio, int cantidad, decimal total)
        {
            Id = id;
            IdFactura = idFactura;
            CodigoProducto = codigoProducto;
            Precio = precio;
            Cantidad = cantidad;
            Total = total;
        }
    }
}
