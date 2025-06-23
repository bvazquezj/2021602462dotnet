using System.Data;
using MySql.Data.MySqlClient;

namespace dist;
public class Program{

public class SqlConnector
{
    private readonly string _connectionString;

    public SqlConnector(string srv, string db, string user, string pass)
    {
        _connectionString = $"Server={srv};Database={db};User Id={user};Password={pass};";
    }

    public DataSet GetDataSet(string sql)
    {
        using (MySqlConnection con = new MySqlConnection(_connectionString))
        {
            con.Open();
            MySqlDataAdapter da = new MySqlDataAdapter(sql, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }
    }
}
    public static void Main(string[] args)
    {
        SqlConnector sql = new SqlConnector("xxxx", "xxxx", "xxxx", "xxxx");
        DataSet ds = sql.GetDataSet("SELECT * FROM pruebas;");
        foreach (DataTable dt in ds.Tables)
        {
            foreach (DataRow dr in dt.Rows)
            {
                foreach (DataColumn dc in dt.Columns)
                {
                    Console.WriteLine(dr[dc]);
                }
            }
        }
    }

}