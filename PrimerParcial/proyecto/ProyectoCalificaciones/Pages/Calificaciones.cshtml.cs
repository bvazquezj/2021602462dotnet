using System.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProyectoCalificaciones.Services;

namespace ProyectoCalificaciones.Pages;

public class CalificacionesModel : PageModel
{
    private readonly ILogger<CalificacionesModel> _logger;
    public readonly DataServices _dataServices;
    public DataSet dts;
    public List<object> data;

    public CalificacionesModel(ILogger<CalificacionesModel> logger, DataServices dataServices)
    {
        _logger = logger;
        _dataServices = dataServices;
        data = _dataServices.GetJsonData();
        dts = dataServices.GetDataSet();
    }

    public void OnGet()
    {
    }

}

