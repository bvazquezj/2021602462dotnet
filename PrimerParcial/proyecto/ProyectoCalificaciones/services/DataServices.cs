using System;
using System.Collections;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using ProyectoCalificaciones.model;

namespace ProyectoCalificaciones.Services;
public class DataServices
{
    public required DataSet dts;
    public DataServices()
    {
        dts = new DataSet();
        dts.ReadXml("./data/AlumnData.xml");
        DataColumn col1 = new DataColumn("Promedio");

        dts.Tables[0].Columns.Add(col1);

        for (int i = 0; i < dts.Tables[0].Rows.Count; i++)
        {
            DataRow row = dts.Tables[0].Rows[i];
            if (row["parcial1"] == null && row["parcial2"] == null && row["parcial3"] == null)
                continue;

            row["Promedio"] = (int.Parse(row["parcial1"].ToString()) + int.Parse(row["parcial2"].ToString()) + int.Parse(row["parcial3"].ToString())) / 3;

            if (row["ext"].ToString() == "np" || row["ext"] == null)
                continue;

            if (int.Parse(row["Promedio"].ToString()) < int.Parse(row["ext"].ToString()))
            {
                row["Promedio"] = row["ext"];
            }
        }
    }
    public DataSet GetDataSet()
    {
        return dts;
    }
    public List<Object> GetJsonData()
    {
        var list = new List<Object>();
        for (int i = 0; i < dts.Tables[0].Rows.Count; i++)
        {
            list.Add(
                new
                {
                    id = dts.Tables[0].Rows[i]["id"],
                    nombre = dts.Tables[0].Rows[i]["Nombre"],
                    apellidos = dts.Tables[0].Rows[i]["Apellidos"],
                    parcial1 = dts.Tables[0].Rows[i]["Parcial1"],
                    parcial2 = dts.Tables[0].Rows[i]["Parcial2"],
                    parcial3 = dts.Tables[0].Rows[i]["Parcial3"],
                    ext = dts.Tables[0].Rows[i]["Ext"],
                    promedio = dts.Tables[0].Rows[i]["Promedio"]
                }
            );
        }
        return list;
    }

    public double GetAproGnral()
    {
        var list = new ArrayList();
        int promedio = 0;
        
        int i = 0;
        for (i = 0; i < dts.Tables[0].Rows.Count; i++)
        {
            promedio += int.Parse(dts.Tables[0].Rows[i]["Promedio"].ToString()??"0");
        }

        promedio = promedio / i;
        
        return promedio;
    }

    public List<Personas> GetBestAlumns()
    {
        var alumnos = GetJsonData();

        alumnos.Sort((a, b) =>
        {
            dynamic alumnoA = a;
            dynamic alumnoB = b;
            return int.Parse(alumnoB.promedio.ToString()).CompareTo(int.Parse(alumnoA.promedio.ToString()));
        });

        var top5Alumnos = alumnos.Take(5).ToList();

        var result = new List<Personas>();
        foreach (var alumno in top5Alumnos)
        {
            result.Add(new Personas
            {
                nombre = ((dynamic)alumno).nombre,
                promedio = double.Parse(((dynamic)alumno).promedio.ToString())
            });
        }

        return result;
    }

    public List<Personas> GetTop5PeoresAlumnos()
    {
        var alumnos = GetJsonData();

        alumnos.Sort((a, b) =>
        {
            dynamic alumnoA = a;
            dynamic alumnoB = b;
            return int.Parse(alumnoA.promedio.ToString()).CompareTo(int.Parse(alumnoB.promedio.ToString()));
        });

        var bottom5Alumnos = alumnos.Take(5).ToList();

        var result = new List<Personas>();
        foreach (var alumno in bottom5Alumnos)
        {
            result.Add(new Personas
            {
                nombre = ((dynamic)alumno).nombre,
                promedio = double.Parse(((dynamic)alumno).promedio.ToString())
            });
        }

        return result;
    }

    public Porsentaje GetPorcentajeAprobadosReprobados()
    {
        var alumnos = GetJsonData();
        int aprobados = 0;
        int reprobados = 0;

        foreach (var alumno in alumnos)
        {
            if (double.TryParse(((dynamic)alumno).promedio.ToString(), out double promedio) && promedio >= 6.0)
            {
                aprobados++;
            }
            else
            {
                reprobados++;
            }
        }

        int total = aprobados + reprobados;
        double porcentajeAprobados = (total > 0) ? (aprobados / (double)total) * 100 : 0;
        double porcentajeReprobados = 100 - porcentajeAprobados;

    return new Porsentaje
    {
        Aprobados = porcentajeAprobados,
        Reprobados = porcentajeReprobados
    };
    }

    public double GetModaCalificaciones()
    {
        var alumnos = GetJsonData();
        var calificaciones = new List<int>();

        foreach (var alumno in alumnos)
        {
            if (int.TryParse(((dynamic)alumno).promedio.ToString(), out int promedio))
            {
                calificaciones.Add(promedio);
            }
        }

        var grouped = calificaciones.GroupBy(c => c)
                                   .OrderByDescending(g => g.Count())
                                   .FirstOrDefault();

        return grouped?.Key ?? 0;
    }
}