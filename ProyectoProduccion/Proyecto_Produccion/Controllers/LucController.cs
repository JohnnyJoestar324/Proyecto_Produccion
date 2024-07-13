using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Configuration;
using Microsoft.AspNetCore.Authorization;
using Proyecto_Produccion.Models;

namespace Proyecto_Produccion.Controllers
{
    [Authorize]

    public class LucController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CalculateLUC(int Periodo, string RequerimientoBruto, decimal costoDeOrdenar, decimal costoMantenimiento)
        {
            var results = new List<Luc>();

            // Convertir la cadena de requerimientos brutos en una lista de enteros
            var unidades = RequerimientoBruto.Split(',').Select(int.Parse).ToList();

            // Inicializamos los valores para el primer período
            var periodo = 0;
            var unidadesPeriodo = unidades[0];

            bool primeraIteracion = true; // Bandera para identificar la primera iteración
            int sumaAnterior = 0;
            int sumaAnteriorRequerimiento = 0;
            decimal sumaAnteriorK = 0;
            decimal costoTotal;
            decimal costoTotalAnterior = 0; // Variable para almacenar el costoTotal anterior



            // Iteramos sobre cada período
            for (int i = 0; i < Periodo; i++)
            {

                // Obtenemos el valor de la fila actual
                var periodoActual = unidades[i];

                // Sumamos el valor actual con el valor de la fila anterior, si existe


                // Calculamos el costo de mantenimiento para el período actual
                var costoMantenimientoPeriodo = costoMantenimiento;



                var sumaConAnterior = (i + 1) + sumaAnterior;
                sumaAnterior = sumaConAnterior;
                var sumaConAnteriorRequerimiento = unidadesPeriodo + sumaAnteriorRequerimiento;
                sumaAnteriorRequerimiento = sumaConAnteriorRequerimiento;

                if (primeraIteracion)
                {
                    sumaAnteriorK = 0; // Primera multiplicación con cero
                    primeraIteracion = false; // Desactivar la bandera después de la primera iteración
                    costoTotalAnterior = costoDeOrdenar;

                }

                else
                {
                    var SumaConAnteriorK = costoMantenimiento * sumaConAnterior * ((i + 1) - 1);
                    sumaAnteriorK = SumaConAnteriorK;
                    costoTotal = costoTotalAnterior + sumaAnteriorK; // Sumar costoTotalAnterior con sumaAnteriorK
                    costoTotalAnterior = costoTotal; // Actualizar costoTotalAnterior
                }




                // costoTotal = item.costoDeOrdenar + sumaAnteriorK;
                var costoTotalUnidades = costoTotalAnterior / sumaConAnteriorRequerimiento;



                // Guardamos los resultados en la lista
                results.Add(new Luc
                {
                    Periodo = sumaConAnterior,
                    RequerimientoBruto = sumaConAnteriorRequerimiento,
                    CostoDeOrdenar = costoDeOrdenar,
                    CostoMantenimiento = sumaAnteriorK,
                    CostoTotal = costoTotalAnterior,
                    CostoTotalU = costoTotalUnidades
                }); ;

                // Actualizamos las unidades para el próximo período
                if (i < unidades.Count - 1)
                {
                    unidadesPeriodo = unidades[i + 1];
                }
            }
            ViewBag.CostoOrdenar = costoDeOrdenar;
            // Retornamos la vista con los resultados
            return View("Index", results);
        }
    }
}
