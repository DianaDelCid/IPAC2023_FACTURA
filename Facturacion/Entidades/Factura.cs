using System;

namespace Entidades
{
    public class Factura
    {
        //Propiedades
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string IdentidadCliente { get; set; }
        public string CodigoUsuario { get; set; }
        public string ISV { get; set; }
        public string Descuento { get; set; }
        public string Subtotal { get; set; }
        public string Total { get; set; }

        //Constructor vacio
        public Factura()
        {
        }

        //Constructor con parametros
        public Factura(int id, DateTime fecha, string identidadCliente, string codigoUsuario, string iSV, string descuento, string subtotal, string total)
        {
            Id = id;
            Fecha = fecha;
            IdentidadCliente = identidadCliente;
            CodigoUsuario = codigoUsuario;
            ISV = iSV;
            Descuento = descuento;
            Subtotal = subtotal;
            Total = total;
        }
    }
}
