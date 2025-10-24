

using Microsoft.Data.Sqlite;
using tl2_tp7_2025_Luis7l.MODELS;
public class ProductoRepository
{
    private string connectionString = "Data Source = DB/Tienda.db";

    public int Alta(Producto producto)
    {



        using var connection = new SqliteConnection(connectionString);
        connection.Open();
        var sql = "INSERT INTO Productos (Descripcion,precio) VALUES            (@descripcion,@precio);";
        using var command = new SqliteCommand(sql, connection);
        command.Parameters.AddWithValue("@descripcion", producto.descripcion);
        command.Parameters.AddWithValue("@precio", producto.precio);
        command.ExecuteNonQuery();
        int id = (int)command.ExecuteScalar();
        return id;
    }

    public void actualizarProducto(int id, Producto producto)
    {
        using var connection = new SqliteConnection(connectionString);
        connection.Open();
        var sql = "UPDATE Productos SET descripcion = @descripcion,precio=@precio WHERE idProducto=@id;";
        using var command = new SqliteCommand(sql, connection);
        command.Parameters.AddWithValue("@descripcion", producto.descripcion);
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
    public List <Producto> ListarProductos()
    {
        List<Producto> productos = new List<Producto>() ;
        using var connection = new SqliteCommand(connectionString);
    }
}   

