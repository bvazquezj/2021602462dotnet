using System.Data;
using CrudInventario.Entity;
using MySql.Data.MySqlClient;
using Mysqlx.Crud;

namespace CrudInventario.utils;

public class SqlConnector
{
    private readonly string _connectionString;

    public SqlConnector(string srv, string db, string user, string pass){
        _connectionString = $"Server={srv};Database={db};User Id={user};Password={pass};";
    }

    public DataSet GetDataSet(string sql){
        using (MySqlConnection con = new MySqlConnection(_connectionString)){
            con.Open();
            MySqlDataAdapter da = new MySqlDataAdapter(sql, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }
    }

    public string Insert(String sql, Producto producto)
    {
        using (MySqlConnection con = new MySqlConnection(_connectionString)){
            con.Open();
            
            MySqlCommand cmd = new MySqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@name", producto.Nombre);
            cmd.Parameters.AddWithValue("@stock", producto.Stock);
            cmd.Parameters.AddWithValue("@price", producto.Precio);
            cmd.Parameters.AddWithValue("@descript", producto.Descripcion);

            cmd.ExecuteNonQuery();
        }
        return "insercion realizada con exito";
    }
}