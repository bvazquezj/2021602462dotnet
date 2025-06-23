using System.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProyectoCalificaciones.model;
using ProyectoCalificaciones.Services;


namespace ProyectoCalificaciones.Pages;

public class EstadisticModel : PageModel
{
    private readonly ILogger<EstadisticModel> _logger;
    public readonly DataServices _dataServices;
    public DataSet dts;
    public List<object> data;
    public List<Estadisticas> data2;

    public EstadisticModel(ILogger<EstadisticModel> logger, DataServices dataServices)
    {
        _logger = logger;
        _dataServices = dataServices;
        data = _dataServices.GetJsonData();
        dts = dataServices.GetDataSet();
    
        data2 = new List<Estadisticas>();
        data2.Add(new Estadisticas
        {
            promedio = _dataServices.GetAproGnral(),
            mejores = _dataServices.GetBestAlumns(),
            peores = _dataServices.GetTop5PeoresAlumnos(),
            porcentaje = _dataServices.GetPorcentajeAprobadosReprobados(),
            moda = (int)_dataServices.GetModaCalificaciones()
        });
    }

    public void OnGet()
    {
    }

}

