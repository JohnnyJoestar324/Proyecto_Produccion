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
    public class CantidadKanbanesController : Controller
    {
        private readonly ProyectoProduccionContext _context;

        public CantidadKanbanesController(ProyectoProduccionContext context)
        {
            _context = context;
        }

        // GET: CantidadKanbanes
        public async Task<IActionResult> Index()
        {
            return View(await _context.CantidadKanbanes.ToListAsync());
        }

        // GET: CantidadKanbanes/Details/5
        public async Task<IActionResult> Details(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cantidadKanbane = await _context.CantidadKanbanes
                .FirstOrDefaultAsync(m => m.IdcantidadKanbanes == id);
            if (cantidadKanbane == null)
            {
                return NotFound();
            }

            return View(cantidadKanbane);
        }

        // GET: CantidadKanbanes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CantidadKanbanes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdcantidadKanbanes,OfertaDpi,DemandaDpi,Demanda,Retraso,TiempoDeEntrega,StockDeSeguridad,CantidadAlmacenamiento,Resultado")] CantidadKanbane cantidadKanbane)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cantidadKanbane);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cantidadKanbane);
        }

        // GET: CantidadKanbanes/Edit/5
        public async Task<IActionResult> Edit(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cantidadKanbane = await _context.CantidadKanbanes.FindAsync(id);
            if (cantidadKanbane == null)
            {
                return NotFound();
            }
            return View(cantidadKanbane);
        }

        // POST: CantidadKanbanes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(short id, [Bind("IdcantidadKanbanes,OfertaDpi,DemandaDpi,Demanda,Retraso,TiempoDeEntrega,StockDeSeguridad,CantidadAlmacenamiento,Resultado")] CantidadKanbane cantidadKanbane)
        {
            if (id != cantidadKanbane.IdcantidadKanbanes)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cantidadKanbane);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CantidadKanbaneExists(cantidadKanbane.IdcantidadKanbanes))
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
            return View(cantidadKanbane);
        }

        // GET: CantidadKanbanes/Delete/5
        public async Task<IActionResult> Delete(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cantidadKanbane = await _context.CantidadKanbanes
                .FirstOrDefaultAsync(m => m.IdcantidadKanbanes == id);
            if (cantidadKanbane == null)
            {
                return NotFound();
            }

            return View(cantidadKanbane);
        }

        // POST: CantidadKanbanes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(short id)
        {
            var cantidadKanbane = await _context.CantidadKanbanes.FindAsync(id);
            if (cantidadKanbane != null)
            {
                _context.CantidadKanbanes.Remove(cantidadKanbane);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CantidadKanbaneExists(short id)
        {
            return _context.CantidadKanbanes.Any(e => e.IdcantidadKanbanes == id);
        }
    }
}
