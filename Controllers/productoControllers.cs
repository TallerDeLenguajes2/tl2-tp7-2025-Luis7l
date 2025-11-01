using Microsoft.AspNetCore.Mvc;
using Models;

namespace tl2_tp7_2025_Luis7l.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductoController : ControllerBase
{
    private static List<Producto> productos = new List<Producto>();

    // POST /api/Producto
    [HttpPost]
    public IActionResult CrearProducto([FromBody] Producto nuevo)
    {
        if (nuevo == null)
            return BadRequest("El producto no puede ser nulo.");

        // Generar un id autoincremental
        nuevo.idProducto = productos.Count > 0 ? productos.Max(p => p.idProducto) + 1 : 1;
        productos.Add(nuevo);

        return CreatedAtAction(nameof(ObtenerPorId), new { id = nuevo.idProducto }, nuevo);
    }

    // PUT /api/Producto/{id}
    [HttpPut("{id}")]
    public IActionResult ModificarDescripcion(int id, [FromBody] string nuevaDescripcion)
    {
        var producto = productos.FirstOrDefault(p => p.idProducto == id);
        if (producto == null)
            return NotFound($"No se encontró un producto con ID {id}.");

        producto.descripcion = nuevaDescripcion;
        return Ok(producto);
    }

    // GET /api/Producto
    [HttpGet]
    public ActionResult<IEnumerable<Producto>> Listar()
    {
        return Ok(productos);
    }

    // GET /api/Producto/{id}
    [HttpGet("{id}")]
    public ActionResult<Producto> ObtenerPorId(int id)
    {
        var producto = productos.FirstOrDefault(p => p.idProducto == id);
        if (producto == null)
            return NotFound($"No se encontró un producto con ID {id}.");

        return Ok(producto);
    }

    // DELETE /api/Producto/{id}
    [HttpDelete("{id}")]
    public IActionResult Eliminar(int id)
    {
        var producto = productos.FirstOrDefault(p => p.idProducto == id);
        if (producto == null)
            return NotFound($"No se encontró un producto con ID {id}.");

        productos.Remove(producto);
        return NoContent();
    }
}
