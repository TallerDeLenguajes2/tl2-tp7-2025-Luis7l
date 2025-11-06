using System.Data.SqlTypes;
using Microsoft.Data.Sqlite;
using Models;
namespace Persistence;

public class PresupuestoRepository
{
    private string connectionstring = "Data source=db/Tienda_final.db";
    public void DarDeAltaPresupuesto(Presupuesto obj)
    {
        using (var connection = new SqliteConnection(connectionstring))
        {
            connection.Open();
            var sql = @"INSERT INTO Presupuestos (NombreDestinatario,FechaCreacion) VALUES ($NombreDestinatario,$fecha_Creacion)";
            using (var sqlCmd = new SqliteCommand(sql, connection))
            {
                sqlCmd.Parameters.AddWithValue("$NombreDestinatario", obj.NombreDestinatario);
                sqlCmd.Parameters.AddWithValue("$fecha_Creacion", obj.fecha_Creacion);
                sqlCmd.ExecuteNonQuery();
            }
            connection.Close();

        }
    }
    
    public void Modificar(int id, Presupuesto obj)
    {
        using (var connection = new SqliteConnection(connectionstring))
        {
            connection.Open();

            var sqlQuery = @"UPDATE Presupuestos 
                             SET NombreDestinatario=$nombreDestinatario, FechaCreacion=$fechaCreacion 
                             WHERE idPresupuesto=$id";

            using (var sqlCmd = new SqliteCommand(sqlQuery, connection))
            {
                sqlCmd.Parameters.AddWithValue("$nombreDestinatario", obj.NombreDestinatario);
                sqlCmd.Parameters.AddWithValue("$fechaCreacion", obj.fecha_Creacion);
                sqlCmd.Parameters.AddWithValue("$id", obj.idPresupuesto);
                sqlCmd.ExecuteNonQuery();
            }

            connection.Close();
        }
    }

    public List<Presupuesto> Listar()
    {
        var presupuesto = new Dictionary<int, Presupuesto>();
        using (var connection = new SqliteConnection(connectionstring))
        {
            connection.Open();
            var sqlQuery = @"SELECT p.idPresupuesto, p.NombreDestinatario, p.FechaCreacion, pr.idProducto, pr.Descripcion, pr.Precio, pd.Cantidad 
                             FROM Presupuestos p
                             LEFT JOIN PresupuestosDetalle pd USING (idPresupuesto)
                             LEFT JOIN Productos pr USING (idProducto)";
            using (var sqlCmd = new SqliteCommand(sqlQuery, connection))
            using (var sqlReader = sqlCmd.ExecuteReader())
            {
                while (sqlReader.Read())
                {
                    int idPresupuesto = sqlReader.GetInt32(0);
                    if (!presupuesto.TryGetValue(idPresupuesto, out var presupuestos))
                    {
                        presupuestos = generarPresupuesto(sqlReader);

                    }
                    if (!sqlReader.IsDBNull(3))
                    {
                        var producto = new Producto(sqlReader.GetInt32(3), sqlReader.GetString(4), sqlReader.GetFloat(5));

                        var detalle = new PresupuestosDetalle(sqlReader.GetInt32(6), producto);
                        presupuestos.detalle.Add(detalle);
                    }
                    presupuesto.TryAdd(idPresupuesto, presupuestos);
                }

            }
            connection.Close();

        }
        return new List<Presupuesto>(presupuesto.Values);

    }
    public Presupuesto MostrarPresupuestoid(int id)
    {
        Presupuesto presupuestoEncontrado = new Presupuesto();
        using var connection = new SqliteConnection(connectionstring);
        connection.Open();
        var sql = @"SELECT p.idPresupuesto,p.NombreDestinatario,p.fechaCreacion,pr.idProducto,pr.Descripcion,pr.Precio,pd.Cantidad
                  FROM Presupuestos p
                  LEFT JOIN PresupuestosDetalle pd using(idPresupuesto)
                  LEFT JOIN Productos pr using(idProducto)
                  WHERE idPresupuesto=$id";

        using var sqlCmd = new SqliteCommand(sql, connection);
        sqlCmd.Parameters.AddWithValue("$id", id);
        using var sqlReader = sqlCmd.ExecuteReader();
        while (sqlReader.Read())
        {
            if (presupuestoEncontrado.idPresupuesto == -1)
            {
                presupuestoEncontrado = generarPresupuesto(sqlReader);
            }
            if (!sqlReader.IsDBNull(3))
            {
                var producto = new Producto(sqlReader.GetInt32(3), sqlReader.GetString(4), sqlReader.GetFloat(5));
                var detalle = new PresupuestosDetalle(sqlReader.GetInt32(6), producto);
                presupuestoEncontrado.detalle.Add(detalle);
            }
        }

        connection.Close();
        return presupuestoEncontrado;
    }

public void InsertarDetalle(int idPresupuesto, int idProducto, int cantidad)
    {
        using (var connection = new SqliteConnection(connectionstring))
        {
            connection.Open();

            var sqlQuery = @"INSERT INTO PresupuestosDetalle (idPresupuesto, idProducto, Cantidad)
                             VALUES ($idPresupuesto, $idProducto, $cantidad)";
            using (var sqlCmd = new SqliteCommand(sqlQuery, connection))
            {
                sqlCmd.Parameters.AddWithValue("$idPresupuesto", idPresupuesto);
                sqlCmd.Parameters.AddWithValue("$idProducto", idProducto);
                sqlCmd.Parameters.AddWithValue("$cantidad", cantidad);
                sqlCmd.ExecuteNonQuery();
            }

            connection.Close();
        }
    }

    private Presupuesto generarPresupuesto(SqliteDataReader reader)
    {
        try
        {
            return new Presupuesto(reader.GetInt32(0),
                                   reader.GetString(1),
                                   reader.GetString(2));
        }
        catch (Exception)
        {
            return new Presupuesto();
        }
    }
     public void Eliminar(int id)
    {
        using (var connection = new SqliteConnection(connectionstring))
        {
            connection.Open();

            var sqlQuery = @"DELETE FROM Presupuestos WHERE idPresupuesto=$id";
            using (var sqlCmd = new SqliteCommand(sqlQuery, connection))
            {
                sqlCmd.Parameters.AddWithValue("$id", id);
                sqlCmd.ExecuteNonQuery();
            }

            connection.Close();
        }
    }
}