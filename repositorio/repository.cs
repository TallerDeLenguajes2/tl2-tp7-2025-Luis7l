

using Microsoft.Data.Sqlite;
using tl2_tp7_2025_Luis7l.MODELS;
public class ProductoRepository{
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
        int id=(int)command.ExecuteScalar();
        return id;
        


    }

}

