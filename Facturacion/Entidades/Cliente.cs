using System;

namespace Entidades
{
    public class Cliente
    {
        //Propiedades 
        public string Identidad { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public string Direccion { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public bool EstaActivo { get; set; }
        public byte[] Foto { get; set; }  //para agregar imagen del CLIENTE

        //Constructor vacio
        public Cliente()
        {

        }

        //Constructor con los parametros de las propiedades
        public Cliente(string identidad, string nombre, string telefono, string correo, string direccion, DateTime fechaNacimiento, bool estaActivo, byte[] foto)
        {
            Identidad = identidad;
            Nombre = nombre;
            Telefono = telefono;
            Correo = correo;
            Direccion = direccion;
            FechaNacimiento = fechaNacimiento;
            EstaActivo = estaActivo;
            Foto = foto;
        }


    }
}
