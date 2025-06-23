using System.Data;

public class Program
{

    public static DataSet generarDatos()
    {
        DataSet dts = new DataSet("Base");
        DataTable tbl = new DataTable();
        DataColumn col1 = new DataColumn("id");
        DataColumn col2 = new DataColumn("nombre");
        DataColumn col3 = new DataColumn("edad");
        DataColumn col4 = new DataColumn("telefono");

        tbl.Columns.Add(col1);
        tbl.Columns.Add(col2);
        tbl.Columns.Add(col3);
        tbl.Columns.Add(col4);

        DataRow row = tbl.NewRow();
        row["id"] = 1;
        row["nombre"] = "Juan";
        row["edad"] = 25;
        row["telefono"] = "123456789";
        tbl.Rows.Add(row);

        row = tbl.NewRow();
        row["id"] = 2;
        row["nombre"] = "Pedro";
        row["edad"] = 30;
        row["telefono"] = "987654321";
        tbl.Rows.Add(row);
        dts.Tables.Add(tbl);
        return dts;
    }
    public static void Main()
    {
        DataSet datos = generarDatos();
        for (int i = 0; i < datos.Tables[0].Rows.Count; i++)
        {
            Console.Write(datos.Tables[0].Rows[i]["id"] + "-");
            Console.Write(datos.Tables[0].Rows[i]["nombre"] + "-");
            Console.Write(datos.Tables[0].Rows[i]["edad"] + "-");
            Console.Write(datos.Tables[0].Rows[i]["telefono"]);
            Console.WriteLine();
        }
    }
}
