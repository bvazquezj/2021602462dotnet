
using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProyectoCalificaciones.Services;

namespace ProyectoCalificaciones.Pages;

public class AlumnModel : PageModel
{
    private readonly ILogger<AlumnModel> _logger;
    public readonly DataServices _dataServices;
    public DataSet dts;
    public List<object> data;

    public AlumnModel(ILogger<AlumnModel> logger, DataServices dataServices)
    {
        _logger = logger;
        _dataServices = dataServices;
        data = _dataServices.GetJsonData();
        dts = dataServices.GetDataSet();
    }

    public void OnGet()
    {
    }

    public JsonResult OnGetJsonData()
    {
        return new JsonResult(data);
    }
}

