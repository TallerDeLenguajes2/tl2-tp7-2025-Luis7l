using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models;


public class Producto
{
    public int idProducto { get; set; }
    public string descripcion { get; set; } = string.Empty;
    public float precio { get; set; }


    public Producto(int id, string desc, float pre)
    {
        idProducto = id;
        descripcion = desc;
        precio = pre;
    }

    public Producto()
    {
        idProducto = -1;
        descripcion = string.Empty;
        precio = 0;
    }

}
