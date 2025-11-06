

using Microsoft.Data.Sqlite;
using Models;
namespace Persistence;

public class ProductoRepository
{
  private string connectionstring = "Data source=db/Tienda_final.db";
    public int Alta(Producto producto)
    {
        using var connection = new SqliteConnection(connectionstring);
        connection.Open();
        var sql = "INSERT INTO Productos (Descripcion,precio) VALUES (@descrip,@precio);";
        using var command = new SqliteCommand(sql, connection);
        command.Parameters.AddWithValue("@descrip", producto.descripcion);
        command.Parameters.AddWithValue("@precio", producto.precio);
        command.ExecuteNonQuery();

        command.CommandText = "SELECT last_insert_rowid()";
        int id = Convert.ToInt32(command.ExecuteScalar());
        return id;
    }
    public void actualizarProducto(int id, Producto producto)
    {
        using var connection = new SqliteConnection(connectionstring);
        connection.Open();
        var sql = "UPDATE Productos SET Descripcion = @descripcion,precio=@precio WHERE idProducto=@idProducto;";
        using var command = new SqliteCommand(sql, connection);
        command.Parameters.AddWithValue("@descripcion", producto.descripcion);
        command.Parameters.AddWithValue("@precio", producto.precio);
        command.Parameters.AddWithValue("@idProducto", producto.idProducto);
        command.ExecuteNonQuery();
    }
    public void eliminarProducto(int id)
    {
        using var connection = new SqliteConnection(connectionstring);
        connection.Open();

        var sql = "DELETE FROM Productos WHERE idProducto=@id;";
        using var command = new SqliteCommand(sql, connection);
        command.Parameters.AddWithValue("@id", id);
        command.ExecuteNonQuery();
    }

    public List<Producto> ListarProductos()
    {
        List<Producto> productos = new List<Producto>();
        using var connection = new SqliteConnection(connectionstring);
        connection.Open();
        var sql = "SELECT IdProducto,Descripcion,precio FROM PRODUCTOS;";
        using var command = new SqliteCommand(sql, connection);
        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            productos.Add(new Producto
            {
                idProducto = reader.GetInt32(0),
                descripcion = reader.GetString(1),
                precio = reader.GetFloat(2)
            });
        }
        return productos;
    }
    // Reemplaza el método vacío: public void DetallesProductos(int id){} CON ESTE:
    public Producto DetallesProductos(int id)
    {
        Producto producto = null; // Inicia como nulo
        using var connection = new SqliteConnection(connectionstring);
        connection.Open();
        var sql = "SELECT IdProducto, Descripcion, precio FROM PRODUCTOS WHERE IdProducto = @id;";
        using var command = new SqliteCommand(sql, connection);
        command.Parameters.AddWithValue("@id", id);
        using var reader = command.ExecuteReader();

        if (reader.Read()) // Usa 'if' en lugar de 'while' porque solo esperas un resultado
        {
            producto = new Producto
            {
                idProducto = reader.GetInt32(0),
                descripcion = reader.GetString(1),
                precio = reader.GetFloat(2)
            };
        }
        return producto; // Devuelve el producto (o null si no se encontró)
    }
}

