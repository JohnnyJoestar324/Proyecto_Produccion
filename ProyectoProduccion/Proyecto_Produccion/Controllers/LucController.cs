using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult CalculateLUC(string[] requerimientosBrutos, decimal costoDeOrdenar, decimal costoMantenimiento)
        {
            var results = new List<Luc>();

            // Filtrar solo los valores no nulos o vacíos
            var unidades = requerimientosBrutos.Where(r => !string.IsNullOrWhiteSpace(r)).Select(r => int.Parse(r)).ToList();

            // Inicializamos los valores para el primer período
            var periodo = 0;
            var unidadesPeriodo = unidades.Any() ? unidades[0] : 0; // Tomamos el primer valor válido o 0 si no hay ninguno
            bool primeraIteracion = true;
            int sumaAnterior = 0;
            int sumaAnteriorRequerimiento = 0;
            decimal sumaAnteriorK = 0;
            decimal costoTotal = 0;
            decimal costoTotalAnterior = 0;

            // Iteramos sobre cada período
            for (int i = 0; i < unidades.Count; i++)
            {
                var periodoActual = unidades[i];
                var costoMantenimientoPeriodo = costoMantenimiento;

                var sumaConAnterior = (i + 1) + sumaAnterior;
                sumaAnterior = sumaConAnterior;
                var sumaConAnteriorRequerimiento = unidadesPeriodo + sumaAnteriorRequerimiento;
                sumaAnteriorRequerimiento = sumaConAnteriorRequerimiento;

                if (primeraIteracion)
                {
                    sumaAnteriorK = 0;
                    primeraIteracion = false;
                    costoTotalAnterior = costoDeOrdenar;
                }
                else
                {
                    var SumaConAnteriorK = costoMantenimiento * sumaConAnterior * (i + 1 - 1);
                    sumaAnteriorK = SumaConAnteriorK;
                    costoTotal = costoTotalAnterior + sumaAnteriorK;
                    costoTotalAnterior = costoTotal;
                }

                var costoTotalUnidades = costoTotalAnterior / sumaConAnteriorRequerimiento;

                results.Add(new Luc
                {
                    Periodo = sumaConAnterior,
                    RequerimientoBruto = sumaConAnteriorRequerimiento,
                    CostoDeOrdenar = costoDeOrdenar,
                    CostoMantenimiento = sumaAnteriorK,
                    CostoTotal = costoTotalAnterior,
                    CostoTotalU = costoTotalUnidades
                });

                if (i < unidades.Count - 1)
                {
                    unidadesPeriodo = unidades[i + 1];
                }
            }

            ViewBag.CostoOrdenar = costoDeOrdenar;
            ViewBag.CostoMantenimiento = costoMantenimiento;
            ViewBag.Results = results;

            return View("Index", results);
        }
    }
}
