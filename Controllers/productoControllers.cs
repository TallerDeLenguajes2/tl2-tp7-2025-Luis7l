using Microsoft.AspNetCore.Mvc;
using Models;
using Persistence; // 1. IMPORTANTE: Importar el namespace de tus repositorios

namespace tl2_tp7_2025_Luis7l.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductoController : ControllerBase
{
    // 2. Eliminar la lista estática
    // private static List<Producto> productos = new List<Producto>();

    // 3. Declarar el repositorio
    private readonly ProductoRepository _productoRepository;

    // 4. Crear un constructor para inicializar el repositorio
    public ProductoController()
    {
        _productoRepository = new ProductoRepository();
    }

    // 5. Modificar TODOS los métodos para usar el repositorio

    // POST /api/Producto
    [HttpPost]
    public IActionResult CrearProducto([FromBody] Producto nuevo)
    {
        if (nuevo == null)
            return BadRequest("El producto no puede ser nulo.");

        // Usa el repositorio para INSERTAR en la BD
        int nuevoId = _productoRepository.Alta(nuevo);
        nuevo.idProducto = nuevoId;

        return CreatedAtAction(nameof(ObtenerPorId), new { id = nuevo.idProducto }, nuevo);
    }

    // PUT /api/Producto/{id}
    // Modifiqué este método para que coincida con lo que tu repositorio espera
    [HttpPut("{id}")]
    public IActionResult ModificarProducto(int id, [FromBody] Producto producto)
    {
        if (id != producto.idProducto)
            return BadRequest("Los IDs no coinciden.");

        // Llama al repositorio para hacer UPDATE en la BD
        _productoRepository.actualizarProducto(id, producto);
        return Ok(producto);
    }

    // GET /api/Producto
    [HttpGet]
    public ActionResult<IEnumerable<Producto>> Listar()
    {
        // Llama al repositorio para hacer SELECT en la BD
        return Ok(_productoRepository.ListarProductos());
    }

    // GET /api/Producto/{id}
    [HttpGet("{id}")]
    public ActionResult<Producto> ObtenerPorId(int id)
    {
        // Llama al repositorio para hacer SELECT... WHERE en la BD
        // (Asegúrate de implementar 'DetallesProductos' en tu repo, ver Paso 3)
        var producto = _productoRepository.DetallesProductos(id); 
        
        if (producto == null)
            return NotFound($"No se encontró un producto con ID {id}.");

        return Ok(producto);
    }

    // DELETE /api/Producto/{id}
    [HttpDelete("{id}")]
    public IActionResult Eliminar(int id)
    {
        // Llama al repositorio para hacer DELETE en la BD
        _productoRepository.eliminarProducto(id);
        return NoContent(); // 204 No Content
    }
}