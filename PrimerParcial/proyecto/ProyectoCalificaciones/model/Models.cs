
namespace ProyectoCalificaciones.model;

public class Estadisticas{
    public double promedio { get; set; }
    public List<Personas> mejores { get; set; }
    public List<Personas> peores { get; set; }
    public Porsentaje porcentaje { get; set; }
    public int moda { get; set; }
}

public class Personas{
    public string nombre { get; set; }
    public double promedio { get; set; }
}

public class Porsentaje{
    public double Aprobados { get; set; }
    public double Reprobados { get; set; }
}

 