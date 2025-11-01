using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models;
public class Presupuesto
{
    public int idPresupuesto { get; set; }
    public string NombreDestinatario { get; set; }
    public string fecha_Creacion { get; set; }
    public List<PresupuestosDetalle> detalle;

    public Presupuesto()
    {
        idPresupuesto = -1;
        NombreDestinatario = string.Empty;
        fecha_Creacion = string.Empty;
        detalle = new List<PresupuestosDetalle>();
    }
    public Presupuesto(string nombre, string fecha)
    {
        idPresupuesto = -1;
        NombreDestinatario = nombre;
        fecha_Creacion = fecha;
        detalle = new List<PresupuestosDetalle>();
    }
    public Presupuesto(int id, string nombre, string fecha)
    {
        idPresupuesto = id;
        NombreDestinatario = nombre;
        fecha_Creacion = fecha;
        detalle = new List<PresupuestosDetalle>();
    }

    public float montoPresupuesto()
    {
        return detalle.Sum(d => d.Producto.precio*d.Cantidad);
    }

    public double montoPresupuestoConIva()
    {
        return montoPresupuesto() * 1.21;
    }
    
    public int CantidadProductos()
    {
        return detalle.Sum(d => d.Cantidad);
    }
}
