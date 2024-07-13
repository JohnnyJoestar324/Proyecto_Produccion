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
    public class JitKanbanController : Controller
    {
        private readonly ProyectoProduccionContext _context;

        public JitKanbanController(ProyectoProduccionContext context)
        {
            _context = context;
        }

        // GET: JitKanban
        public async Task<IActionResult> Index()
        {
            return View(await _context.JitKanbans.ToListAsync());
        }

        // GET: JitKanban/Details/5
        public async Task<IActionResult> Details(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jitKanban = await _context.JitKanbans
                .FirstOrDefaultAsync(m => m.IdJitKanban == id);
            if (jitKanban == null)
            {
                return NotFound();
            }

            return View(jitKanban);
        }

        // GET: JitKanban/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: JitKanban/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdJitKanban,Demanda,TiempoVuelta,TamañoRecipiente,N,InventarioMaximo")] JitKanban jitKanban)
        {
            if (ModelState.IsValid)
            {
                int DM = jitKanban.Demanda;
                int TR = jitKanban.TamañoRecipiente;
                decimal TV = jitKanban.TiempoVuelta;
                decimal cantidadRecipiente = jitKanban.N;
                
                //Calculo de los recipientes

                
                cantidadRecipiente = (DM * TV) / (60 * TR);

                //calculo inventario acumulado
                //decimal Maximo = cantidadRecipiente * TR;
                jitKanban.InventarioMaximo = cantidadRecipiente * TR;
                

                _context.Add(jitKanban);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }
            return View(jitKanban);
        }

        // GET: JitKanban/Edit/5
        public async Task<IActionResult> Edit(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jitKanban = await _context.JitKanbans.FindAsync(id);
            if (jitKanban == null)
            {
                return NotFound();
            }
            return View(jitKanban);
        }

        // POST: JitKanban/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(short id, [Bind("IdJitKanban,Demanda,TiempoVuelta,TamañoRecipiente,N,InventarioMaximo")] JitKanban jitKanban)
        {
            if (id != jitKanban.IdJitKanban)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jitKanban);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JitKanbanExists(jitKanban.IdJitKanban))
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
            return View(jitKanban);
        }

        // GET: JitKanban/Delete/5
        public async Task<IActionResult> Delete(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jitKanban = await _context.JitKanbans
                .FirstOrDefaultAsync(m => m.IdJitKanban == id);
            if (jitKanban == null)
            {
                return NotFound();
            }

            return View(jitKanban);
        }

        // POST: JitKanban/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(short id)
        {
            var jitKanban = await _context.JitKanbans.FindAsync(id);
            if (jitKanban != null)
            {
                _context.JitKanbans.Remove(jitKanban);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JitKanbanExists(short id)
        {
            return _context.JitKanbans.Any(e => e.IdJitKanban == id);
        }
    }
}
