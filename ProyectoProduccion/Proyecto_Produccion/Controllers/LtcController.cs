using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Proyecto_Produccion.Models;

namespace Proyecto_Produccion.Controllers
{
    [Authorize]

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
            ViewBag.NumeroPeriodos = 8; // Número de periodos predeterminado
            return View();
        }


        // POST: Ltc/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(List<int?> requerimientosBrutos, decimal costoOrdenar, decimal costoMantenimiento)
        {
            var results = new List<Ltc>();

            // Filtrar los valores nulos y contar los periodos válidos
            var unidades = requerimientosBrutos.Where(x => x.HasValue).Select(x => x.Value).ToList();
            var numeroPeriodos = unidades.Count;

            if (numeroPeriodos == 0)
            {
                ModelState.AddModelError(string.Empty, "Debe ingresar al menos un requerimiento bruto.");
                return View(new List<Ltc>());
            }

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
                    Periodo = i + 1,  // Iniciamos los períodos desde 1
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
            ViewBag.NumeroPeriodos = 12; // Fijamos el número máximo de periodos a 12
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
