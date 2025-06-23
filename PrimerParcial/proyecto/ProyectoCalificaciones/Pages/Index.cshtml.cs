using System.Data;
using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProyectoCalificaciones.Services;

namespace ProyectoCalificaciones.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    public readonly DataServices _dataServices;
    public DataSet dts;
    public List<object> data;
    public List<object> data2;

    public IndexModel(ILogger<IndexModel> logger, DataServices dataServices)
    {
        _logger = logger;
        _dataServices = dataServices;
        data = _dataServices.GetJsonData();
        dts = dataServices.GetDataSet();
    
        data2 = new List<object>();
        data2.Add(
            new {
                promedio = _dataServices.GetAproGnral(),
                mejores = _dataServices.GetBestAlumns(),
                peores = _dataServices.GetTop5PeoresAlumnos(),
                porcentaje = _dataServices.GetPorcentajeAprobadosReprobados(),
                moda = _dataServices.GetModaCalificaciones()
            }
        ); 
    }


    public void OnGet()
    {

    }

}
