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
    public class LtcController : Controller
    {
        private readonly ProyectoProduccionContext _context;

        public LtcController(ProyectoProduccionContext context)
        {
            _context = context;
        }

        // GET: Ltc
        public async Task<IActionResult> Index()
        {
            return View(await _context.Ltcs.ToListAsync());
        }

        // GET: Ltc/Details/5
        public async Task<IActionResult> Details(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ltc = await _context.Ltcs
                .FirstOrDefaultAsync(m => m.Idltc == id);
            if (ltc == null)
            {
                return NotFound();
            }

            return View(ltc);
        }

        // GET: Ltc/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Ltc/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Idltc,Periodo,Unidades,PeridodosMantenidos,CostoMantenimiento,CostoMantenimientoAcumulado")] Ltc ltc, int numeroPeriodos, string requerimientosBrutos, decimal costoOrdenar, decimal costoMantenimiento)
        {
            
            var results = new List<Ltc>();

            // Convertir la cadena de requerimientos brutos en una lista de enteros
            var unidades = requerimientosBrutos.Split(',').Select(int.Parse).ToList();

            // Inicializamos los valores para el primer período
            var periodo = 0;
            var unidadesPeriodo = unidades[0];
            var periodosMantenidos = 0;
            var costoMantenimientoAcumulado = 0.0m;

            // Iteramos sobre cada período
            for (int i = 0; i < numeroPeriodos; i++)
            {
                // Calculamos el costo de mantenimiento para el período actual
                var costoMantenimientoPeriodo = unidadesPeriodo * periodosMantenidos * costoMantenimiento;

                // Calculamos el costo de mantenimiento acumulado
                costoMantenimientoAcumulado += costoMantenimientoPeriodo;

                // Guardamos los resultados en la lista
                results.Add(new Ltc
                {
                    Periodo = periodo,
                    Unidades = unidadesPeriodo,
                    PeridodosMantenidos = periodosMantenidos,
                    CostoMantenimiento = costoMantenimientoPeriodo,
                    CostoMantenimientoAcumulado = costoMantenimientoAcumulado
                });

                // Si el costo de mantenimiento acumulado supera el costo de ordenar, reiniciamos los valores
                if (costoMantenimientoAcumulado >= costoOrdenar)
                {
                    periodo = 1;
                    periodosMantenidos = 0;
                    costoMantenimientoAcumulado = 0.0m;
                }
                else
                {
                    // Avanzamos al siguiente período
                    periodo++;
                    periodosMantenidos++;
                }

                // Actualizamos las unidades para el próximo período
                if (i < unidades.Count - 1)
                {
                    unidadesPeriodo = unidades[i + 1];
                }
            }

            ViewBag.CostoOrdenar = costoOrdenar;
            ViewBag.NumeroPeriodos = numeroPeriodos;
            ViewBag.RequerimientosBrutos = requerimientosBrutos;
            ViewBag.CostoMantenimiento = costoMantenimiento;

            // Retornamos la vista con los resultados
            return View(results);
        }

        // GET: Ltc/Edit/5
        public async Task<IActionResult> Edit(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ltc = await _context.Ltcs.FindAsync(id);
            if (ltc == null)
            {
                return NotFound();
            }
            return View(ltc);
        }

        // POST: Ltc/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(short id, [Bind("Idltc,Periodo,Unidades,PeridodosMantenidos,CostoMantenimiento,CostoMantenimientoAcumulado")] Ltc ltc)
        {
            if (id != ltc.Idltc)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ltc);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LtcExists(ltc.Idltc))
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
            return View(ltc);
        }

        // GET: Ltc/Delete/5
        public async Task<IActionResult> Delete(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ltc = await _context.Ltcs
                .FirstOrDefaultAsync(m => m.Idltc == id);
            if (ltc == null)
            {
                return NotFound();
            }

            return View(ltc);
        }

        // POST: Ltc/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(short id)
        {
            var ltc = await _context.Ltcs.FindAsync(id);
            if (ltc != null)
            {
                _context.Ltcs.Remove(ltc);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LtcExists(short id)
        {
            return _context.Ltcs.Any(e => e.Idltc == id);
        }
    }
}
