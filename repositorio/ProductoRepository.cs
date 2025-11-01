

using Microsoft.Data.Sqlite;
using Models;
namespace Persistence;

public class ProductoRepository
{
    private string connectionString = "Data Source = DB/Tienda.db";

    public int Alta(Producto producto)
    {
        using var connection = new SqliteConnection(connectionString);
        connection.Open();
        var sql = "INSERT INTO Productos (Descripcion,precio) VALUES (@Descripcion,@precio);";
        using var command = new SqliteCommand(sql, connection);
        command.Parameters.AddWithValue("@Descripcion", producto.descripcion);
        command.Parameters.AddWithValue("@precio", producto.precio);
        command.ExecuteNonQuery();
        int id = (int)command.ExecuteScalar();
        return id;
    }

    public void actualizarProducto(int id, Producto producto)
    {
        using var connection = new SqliteConnection(connectionString);
        connection.Open();
        var sql = "UPDATE Productos SET Descripcion = @Descripcion,precio=@precio WHERE idProducto=@id;";
        using var command = new SqliteCommand(sql, connection);
        command.Parameters.AddWithValue("@Descripcion", producto.descripcion);
        command.Parameters.AddWithValue("@precio", producto.precio);
        command.Parameters.AddWithValue("@id", producto.idProducto);
        command.ExecuteNonQuery();
    }
    public void eliminarProducto(int id)
    {
        using var connection = new SqliteConnection(connectionString);
        connection.Open();
        var sql = "DELETE FROM Productos WHERE @idProducto=@id;";
        using var command = new SqliteCommand(sql, connection);
        command.Parameters.AddWithValue("@id", id);
        command.ExecuteNonQuery();
    }
    public List<Producto> ListarProductos()
    {
        List<Producto> productos = new List<Producto>();
        using var connection = new SqliteConnection(connectionString);
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
    public void DetallesProductos(int id){}
}   

