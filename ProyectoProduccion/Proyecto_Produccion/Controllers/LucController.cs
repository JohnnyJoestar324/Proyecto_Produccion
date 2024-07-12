using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Proyecto_Produccion.Models;

namespace Proyecto_Produccion.Controllers
{
    public class LucController : Controller
    {
        private readonly ProyectoProduccionContext _context;

        public LucController(ProyectoProduccionContext context)
        {
            _context = context;
        }

        // GET: Luc
        public async Task<IActionResult> Index()
        {
            return View(await _context.Lucs.ToListAsync());
        }

        // GET: Luc/Details/5
        public async Task<IActionResult> Details(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var luc = await _context.Lucs
                .FirstOrDefaultAsync(m => m.Idluc == id);
            if (luc == null)
            {
                return NotFound();
            }

            return View(luc);
        }

        // GET: Luc/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Luc/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Idluc,RequerimientoBruto,Periodo,CostoMantenimiento,CostoDeOrdenar,CostoTotal,CostoTotalU")] Luc luc)
        {
            if (ModelState.IsValid)
            {

                var results = new List<Luc>();

                // Convertir la cadena de requerimientos brutos en una lista de enteros
                var unidades = Enumerable.Repeat(luc.RequerimientoBruto, luc.Periodo).ToList();

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
                for (int i = 0; i < luc.Periodo; i++)
                {

                    // Obtenemos el valor de la fila actual
                    var periodoActual = unidades[i];

                    // Sumamos el valor actual con el valor de la fila anterior, si existe


                    // Calculamos el costo de mantenimiento para el período actual
                    var costoMantenimientoPeriodo = luc.CostoMantenimiento;



                    var sumaConAnterior = (i + 1) + sumaAnterior;
                    sumaAnterior = sumaConAnterior;
                    var sumaConAnteriorRequerimiento = unidadesPeriodo + sumaAnteriorRequerimiento;
                    sumaAnteriorRequerimiento = sumaConAnteriorRequerimiento;

                    if (primeraIteracion)
                    {
                        sumaAnteriorK = 0; // Primera multiplicación con cero
                        primeraIteracion = false; // Desactivar la bandera después de la primera iteración
                        costoTotalAnterior = luc.CostoDeOrdenar;

                    }

                    else
                    {
                        var SumaConAnteriorK = luc.CostoMantenimiento * sumaConAnterior * ((i + 1) - 1);
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
                        CostoDeOrdenar = luc.CostoDeOrdenar,
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



                _context.Add(luc);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(luc);
        }

        // GET: Luc/Edit/5
        public async Task<IActionResult> Edit(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var luc = await _context.Lucs.FindAsync(id);
            if (luc == null)
            {
                return NotFound();
            }
            return View(luc);
        }

        // POST: Luc/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(short id, [Bind("Idluc,RequerimientoBruto,Periodo,CostoMantenimiento,CostoDeOrdenar,CostoTotal,CostoTotalU")] Luc luc)
        {
            if (id != luc.Idluc)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(luc);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LucExists(luc.Idluc))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(luc);
        }

        // GET: Luc/Delete/5
        public async Task<IActionResult> Delete(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var luc = await _context.Lucs
                .FirstOrDefaultAsync(m => m.Idluc == id);
            if (luc == null)
            {
                return NotFound();
            }

            return View(luc);
        }

        // POST: Luc/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(short id)
        {
            var luc = await _context.Lucs.FindAsync(id);
            if (luc != null)
            {
                _context.Lucs.Remove(luc);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LucExists(short id)
        {
            return _context.Lucs.Any(e => e.Idluc == id);
        }
    }
}
