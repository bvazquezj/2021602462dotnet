
using System.Data;
using MySql.Data.MySqlClient;

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
}