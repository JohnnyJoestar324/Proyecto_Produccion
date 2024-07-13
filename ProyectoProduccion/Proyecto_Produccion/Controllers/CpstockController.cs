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
    public class CpstockController : Controller
    {
        private readonly ProyectoProduccionContext _context;

        public CpstockController(ProyectoProduccionContext context)
        {
            _context = context;
        }

        // GET: Cpstock
        public async Task<IActionResult> Index()
        {
            return View(await _context.Cpstocks.ToListAsync());
        }

        // GET: Cpstock/Details/5
        public async Task<IActionResult> Details(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cpstock = await _context.Cpstocks
                .FirstOrDefaultAsync(m => m.Idcpstock == id);
            if (cpstock == null)
            {
                return NotFound();
            }

            return View(cpstock);
        }

        // GET: Cpstock/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cpstock/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Idcpstock,Demanda,TiempoReposo,InventarioSeguridad,Resultado")] Cpstock cpstock)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cpstock);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cpstock);
        }

        // GET: Cpstock/Edit/5
        public async Task<IActionResult> Edit(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cpstock = await _context.Cpstocks.FindAsync(id);
            if (cpstock == null)
            {
                return NotFound();
            }
            return View(cpstock);
        }

        // POST: Cpstock/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(short id, [Bind("Idcpstock,Demanda,TiempoReposo,InventarioSeguridad,Resultado")] Cpstock cpstock)
        {
            if (id != cpstock.Idcpstock)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cpstock);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CpstockExists(cpstock.Idcpstock))
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
            return View(cpstock);
        }

        // GET: Cpstock/Delete/5
        public async Task<IActionResult> Delete(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cpstock = await _context.Cpstocks
                .FirstOrDefaultAsync(m => m.Idcpstock == id);
            if (cpstock == null)
            {
                return NotFound();
            }

            return View(cpstock);
        }

        // POST: Cpstock/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(short id)
        {
            var cpstock = await _context.Cpstocks.FindAsync(id);
            if (cpstock != null)
            {
                _context.Cpstocks.Remove(cpstock);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CpstockExists(short id)
        {
            return _context.Cpstocks.Any(e => e.Idcpstock == id);
        }
    }
}
