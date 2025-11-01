using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models;

    public class PresupuestosDetalle
    {
        // Propiedad pública con nombre exactamente "Producto"
        public Producto Producto { get; set; } = new Producto();

        // Propiedad pública con nombre exactamente "Cantidad"
        public int Cantidad { get; set; }

        public PresupuestosDetalle(int cant, Producto producto)
        {
            Cantidad = cant;
            Producto = producto;
        }
        public PresupuestosDetalle()
        {
            Cantidad = 0;
            Producto = new Producto();
        }


    }

