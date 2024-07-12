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
    public class EoqController : Controller
    {
        private readonly ProyectoProduccionContext _context;

        public EoqController(ProyectoProduccionContext context)
        {
            _context = context;
        }

        // GET: Eoq
        public async Task<IActionResult> Index()
        {
            return View(await _context.Eoqs.ToListAsync());
        }

        // GET: Eoq/Details/5
        public async Task<IActionResult> Details(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eoq = await _context.Eoqs
                .FirstOrDefaultAsync(m => m.Ideoq == id);
            if (eoq == null)
            {
                return NotFound();
            }

            return View(eoq);
        }

        // GET: Eoq/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Eoq/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Ideoq,Demanda,CostoPedido,CostoMantenimiento,PeriodoSeleccionado,Resultado")] Eoq eoq)
        {
            if (ModelState.IsValid)
            {
                // Calcular EOQ
                double D = eoq.Demanda;
                decimal S = eoq.CostoPedido;
                decimal H = eoq.CostoMantenimiento;
                double periodo = eoq.PeriodoSeleccionado;

                double EOQ = Math.Sqrt((2 * D * periodo * (double)S) / (double)H);
                eoq.Resultado = (decimal)Math.Round(EOQ);

                // Guardar en la base de datos
                _context.Add(eoq);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(eoq);
        }

        // GET: Eoq/Edit/5
        public async Task<IActionResult> Edit(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eoq = await _context.Eoqs.FindAsync(id);
            if (eoq == null)
            {
                return NotFound();
            }
            return View(eoq);
        }

        // POST: Eoq/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(short id, [Bind("Ideoq,Demanda,CostoPedido,CostoMantenimiento,PeriodoSeleccionado,Resultado")] Eoq eoq)
        {
            if (id != eoq.Ideoq)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eoq);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EoqExists(eoq.Ideoq))
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
            return View(eoq);
        }

        // GET: Eoq/Delete/5
        public async Task<IActionResult> Delete(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eoq = await _context.Eoqs
                .FirstOrDefaultAsync(m => m.Ideoq == id);
            if (eoq == null)
            {
                return NotFound();
            }

            return View(eoq);
        }

        // POST: Eoq/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(short id)
        {
            var eoq = await _context.Eoqs.FindAsync(id);
            if (eoq != null)
            {
                _context.Eoqs.Remove(eoq);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EoqExists(short id)
        {
            return _context.Eoqs.Any(e => e.Ideoq == id);
        }
    }
}
