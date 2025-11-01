using Microsoft.AspNetCore.Mvc;
using Models;

namespace tl2_tp7_2025_Luis7l.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PresupuestosController : ControllerBase
{
    // Simulación de base de datos en memoria
    private static List<Presupuesto> presupuestos = new List<Presupuesto>();
    private static List<Producto> productos = new List<Producto>
    {
        new Producto(1, "Notebook Lenovo", 850000),
        new Producto(2, "Mouse inalámbrico", 15000),
        new Producto(3, "Monitor Samsung 24\"", 210000)
    };

    // POST /api/Presupuesto
    [HttpPost]
    public IActionResult CrearPresupuesto([FromBody] Presupuesto nuevo)
    {
        if (nuevo == null)
            return BadRequest("El presupuesto no puede ser nulo.");

        nuevo.idPresupuesto = presupuestos.Count > 0 ? presupuestos.Max(p => p.idPresupuesto) + 1 : 1;
        nuevo.detalle = new List<PresupuestosDetalle>();
        presupuestos.Add(nuevo);

        return CreatedAtAction(nameof(ObtenerPorId), new { id = nuevo.idPresupuesto }, nuevo);
    }

    // POST /api/Presupuesto/{id}/ProductoDetalle
    [HttpPost("{id}/ProductoDetalle")]
    public IActionResult AgregarProductoAlPresupuesto(int id, [FromBody] PresupuestosDetalle detalle)
    {
        var presupuesto = presupuestos.FirstOrDefault(p => p.idPresupuesto == id);
        if (presupuesto == null)
            return NotFound($"No se encontró el presupuesto con ID {id}.");

        var producto = productos.FirstOrDefault(p => p.idProducto == detalle.Producto.idProducto);
        if (producto == null)
            return NotFound($"No se encontró el producto con ID {detalle.Producto.idProducto}.");

        // Usar el producto real desde la lista
        var nuevoDetalle = new PresupuestosDetalle(detalle.Cantidad, producto);
        presupuesto.detalle.Add(nuevoDetalle);

        return Ok(presupuesto);
    }

    // GET /api/Presupuesto/{id}
    [HttpGet("{id}")]
    public ActionResult<Presupuesto> ObtenerPorId(int id)
    {
        var presupuesto = presupuestos.FirstOrDefault(p => p.idPresupuesto == id);
        if (presupuesto == null)
            return NotFound($"No se encontró el presupuesto con ID {id}.");

        return Ok(presupuesto);
    }

    // GET /api/Presupuesto
    [HttpGet]
    public ActionResult<IEnumerable<Presupuesto>> Listar()
    {
        return Ok(presupuestos);
    }

    // DELETE /api/Presupuesto/{id}
    [HttpDelete("{id}")]
    public IActionResult EliminarPresupuesto(int id)
    {
        var presupuesto = presupuestos.FirstOrDefault(p => p.idPresupuesto == id);
        if (presupuesto == null)
            return NotFound($"No se encontró el presupuesto con ID {id}.");

        presupuestos.Remove(presupuesto);
        return NoContent();
    }
}
