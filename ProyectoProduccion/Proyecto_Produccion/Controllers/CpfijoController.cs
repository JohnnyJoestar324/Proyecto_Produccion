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
    public class CpfijoController : Controller
    {
        private readonly ProyectoProduccionContext _context;

        public CpfijoController(ProyectoProduccionContext context)
        {
            _context = context;
        }

        // GET: Cpfijo
        public async Task<IActionResult> Index()
        {
            return View(await _context.Cpfijos.ToListAsync());
        }

        // GET: Cpfijo/Details/5
        public async Task<IActionResult> Details(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cpfijo = await _context.Cpfijos
                .FirstOrDefaultAsync(m => m.Idcpfijo == id);
            if (cpfijo == null)
            {
                return NotFound();
            }

            return View(cpfijo);
        }

        // GET: Cpfijo/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cpfijo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Idcpfijo,Demanda,InventarioPedido,InventarioSeguridad,Resultdado")] Cpfijo cpfijo)
        {
            if (ModelState.IsValid)
            {

                int D = cpfijo.Demanda;

                int Q = cpfijo.InventarioPedido;

                int SS = cpfijo.InventarioSeguridad;

                //Calcular Costo Promedio por Fijo
                int I = (Q / 2) + SS;

                //Calculo de retacion de inventario
                int RotacionInventario = D / I;

                cpfijo.Resultdado = $"El costo promedio fijo es de: {I}." +
                    $" La rotacion de inventario es: {RotacionInventario}.";



                _context.Add(cpfijo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cpfijo);
        }

        // GET: Cpfijo/Edit/5
        public async Task<IActionResult> Edit(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cpfijo = await _context.Cpfijos.FindAsync(id);
            if (cpfijo == null)
            {
                return NotFound();
            }
            return View(cpfijo);
        }

        // POST: Cpfijo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(short id, [Bind("Idcpfijo,Demanda,InventarioPedido,InventarioSeguridad,Resultdado")] Cpfijo cpfijo)
        {
            if (id != cpfijo.Idcpfijo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cpfijo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CpfijoExists(cpfijo.Idcpfijo))
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
            return View(cpfijo);
        }

        // GET: Cpfijo/Delete/5
        public async Task<IActionResult> Delete(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cpfijo = await _context.Cpfijos
                .FirstOrDefaultAsync(m => m.Idcpfijo == id);
            if (cpfijo == null)
            {
                return NotFound();
            }

            return View(cpfijo);
        }

        // POST: Cpfijo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(short id)
        {
            var cpfijo = await _context.Cpfijos.FindAsync(id);
            if (cpfijo != null)
            {
                _context.Cpfijos.Remove(cpfijo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CpfijoExists(short id)
        {
            return _context.Cpfijos.Any(e => e.Idcpfijo == id);
        }
    }
}
