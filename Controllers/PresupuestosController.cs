using Microsoft.AspNetCore.Mvc;
using Models;
using Persistence; // 1. IMPORTANTE: Importar el namespace de tus repositorios

namespace tl2_tp7_2025_Luis7l.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PresupuestosController : ControllerBase
{
    // 2. Eliminar las listas estáticas
    // private static List<Presupuesto> presupuestos = new List<Presupuesto>();
    // private static List<Producto> productos = new List<Producto> { ... };

    // 3. Declarar los repositorios
    private readonly PresupuestoRepository _presupuestoRepository;
    private readonly ProductoRepository _productoRepository; // Necesario para validar productos

    // 4. Crear un constructor para inicializar los repositorios
    public PresupuestosController()
    {
        _presupuestoRepository = new PresupuestoRepository();
        _productoRepository = new ProductoRepository();
    }

    // 5. Modificar TODOS los métodos para usar los repositorios

    // POST /api/Presupuesto
    [HttpPost]
    public IActionResult CrearPresupuesto([FromBody] Presupuesto nuevo)
    {
        if (nuevo == null)
            return BadRequest("El presupuesto no puede ser nulo.");

        // Usa el repositorio para INSERTAR en la BD
        _presupuestoRepository.DarDeAltaPresupuesto(nuevo);
        
        // NOTA: Tu método DarDeAltaPresupuesto no devuelve el ID,
        // así que no podemos retornarlo fácilmente. 
        // Idealmente, debería devolver el ID como el repo de Productos.
        
        // Devolvemos el objeto (sin el ID de la BD)
        return Created(nameof(CrearPresupuesto), nuevo);
    }

    // POST /api/Presupuesto/{id}/ProductoDetalle
    [HttpPost("{id}/ProductoDetalle")]
    public IActionResult AgregarProductoAlPresupuesto(int id, [FromBody] PresupuestosDetalle detalle)
    {
        // Validamos que el presupuesto exista
        var presupuesto = _presupuestoRepository.MostrarPresupuestoid(id);
        if (presupuesto.idPresupuesto == -1) // Asumiendo que tu repo devuelve uno por defecto si no lo encuentra
            return NotFound($"No se encontró el presupuesto con ID {id}.");

        // Validamos que el producto exista
        var producto = _productoRepository.DetallesProductos(detalle.Producto.idProducto);
        if (producto == null)
            return NotFound($"No se encontró el producto con ID {detalle.Producto.idProducto}.");

        // Usar el repositorio para insertar el detalle en la BD
        _presupuestoRepository.InsertarDetalle(id, detalle.Producto.idProducto, detalle.Cantidad);

        return Ok("Producto agregado al presupuesto");
    }

    // GET /api/Presupuesto/{id}
    [HttpGet("{id}")]
    public ActionResult<Presupuesto> ObtenerPorId(int id)
    {
        var presupuesto = _presupuestoRepository.MostrarPresupuestoid(id);
        if (presupuesto.idPresupuesto == -1) // Asumiendo que tu repo devuelve uno por defecto si no lo encuentra
            return NotFound($"No se encontró el presupuesto con ID {id}.");

        return Ok(presupuesto);
    }

    // GET /api/Presupuesto
    [HttpGet]
    public ActionResult<IEnumerable<Presupuesto>> Listar()
    {
        return Ok(_presupuestoRepository.Listar());
    }

    // DELETE /api/Presupuesto/{id}
    [HttpDelete("{id}")]
    public IActionResult EliminarPresupuesto(int id)
    {
        _presupuestoRepository.Eliminar(id);
        return NoContent();
    }
}