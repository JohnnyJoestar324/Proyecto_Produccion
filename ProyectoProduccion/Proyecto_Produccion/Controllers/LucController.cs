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
        public async Task<IActionResult> Create([Bind("Idluc,Idproducto,RequerimientoBruto,Periodo,CostoMantenimiento,CostoDeOrdenar,CostoTotal,CostoTotalU")] Luc luc)
        {
            if (ModelState.IsValid)
            {
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
        public async Task<IActionResult> Edit(short id, [Bind("Idluc,Idproducto,RequerimientoBruto,Periodo,CostoMantenimiento,CostoDeOrdenar,CostoTotal,CostoTotalU")] Luc luc)
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
