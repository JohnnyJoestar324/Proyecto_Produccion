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
    public class MantenimientoCorrectivoController : Controller
    {
        private readonly ProyectoProduccionContext _context;

        public MantenimientoCorrectivoController(ProyectoProduccionContext context)
        {
            _context = context;
        }

        // GET: MantenimientoCorrectivo
        public async Task<IActionResult> Index()
        {
            return View(await _context.MantenimientoCorrectivos.ToListAsync());
        }

        // GET: MantenimientoCorrectivo/Details/5
        public async Task<IActionResult> Details(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mantenimientoCorrectivo = await _context.MantenimientoCorrectivos
                .FirstOrDefaultAsync(m => m.Idmantenimiento == id);
            if (mantenimientoCorrectivo == null)
            {
                return NotFound();
            }

            return View(mantenimientoCorrectivo);
        }

        // GET: MantenimientoCorrectivo/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MantenimientoCorrectivo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Idmantenimiento,HorasTrabajo,Mtbf,NumeroFallas,DuracionTarea,CostoHoraTrabajo,Repuestos,TareasAdicionales,RetrasoLogistico,CostoUnitario,CostoFallaUnica,Resultado")] MantenimientoCorrectivo mantenimientoCorrectivo)
        {
            if (ModelState.IsValid)
            {
                // Calcular el número de fallas si no se conoce explícitamente
                if (mantenimientoCorrectivo.NumeroFallas == null)
                {
                    if (mantenimientoCorrectivo.HorasTrabajo.HasValue && mantenimientoCorrectivo.Mtbf.HasValue)
                    {
                        double horasTrabajo = mantenimientoCorrectivo.HorasTrabajo.Value;
                        double mtbf = mantenimientoCorrectivo.Mtbf.Value;
                        mantenimientoCorrectivo.NumeroFallas = (int)Math.Round(horasTrabajo / mtbf);
                    }
                    else
                    {
                        // Manejar el caso donde no se puede calcular el número de fallas
                        ModelState.AddModelError("NumeroFallas", "Debe especificar el número de fallas o proporcionar Horas de Trabajo y MTBF.");
                        return View(mantenimientoCorrectivo);
                    }
                }

                // Calcular el costo de mantenimiento correctivo
                double costoMantenimientoCorrectivo = (double)(mantenimientoCorrectivo.NumeroFallas.Value * ((((mantenimientoCorrectivo.DuracionTarea + mantenimientoCorrectivo.RetrasoLogistico) * (double)mantenimientoCorrectivo.CostoHoraTrabajo) + (double)mantenimientoCorrectivo.Repuestos + (double)mantenimientoCorrectivo.TareasAdicionales) + ((mantenimientoCorrectivo.DuracionTarea * (double)mantenimientoCorrectivo.CostoUnitario) + (double)mantenimientoCorrectivo.CostoFallaUnica)));

                // Guardar el resultado en la entidad antes de guardarla en la base de datos
                mantenimientoCorrectivo.Resultado = costoMantenimientoCorrectivo;

                // Agregar la entidad al contexto y guardar los cambios
                _context.Add(mantenimientoCorrectivo);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(mantenimientoCorrectivo);
        }

        // GET: MantenimientoCorrectivo/Edit/5
        public async Task<IActionResult> Edit(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mantenimientoCorrectivo = await _context.MantenimientoCorrectivos.FindAsync(id);
            if (mantenimientoCorrectivo == null)
            {
                return NotFound();
            }
            return View(mantenimientoCorrectivo);
        }

        // POST: MantenimientoCorrectivo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(short id, [Bind("Idmantenimiento,HorasTrabajo,Mtbf,NumeroFallas,DuracionTarea,CostoHoraTrabajo,Repuestos,TareasAdicionales,RetrasoLogistico,CostoUnitario,CostoFallaUnica,Resultado")] MantenimientoCorrectivo mantenimientoCorrectivo)
        {
            if (id != mantenimientoCorrectivo.Idmantenimiento)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mantenimientoCorrectivo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MantenimientoCorrectivoExists(mantenimientoCorrectivo.Idmantenimiento))
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
            return View(mantenimientoCorrectivo);
        }

        // GET: MantenimientoCorrectivo/Delete/5
        public async Task<IActionResult> Delete(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mantenimientoCorrectivo = await _context.MantenimientoCorrectivos
                .FirstOrDefaultAsync(m => m.Idmantenimiento == id);
            if (mantenimientoCorrectivo == null)
            {
                return NotFound();
            }

            return View(mantenimientoCorrectivo);
        }

        // POST: MantenimientoCorrectivo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(short id)
        {
            var mantenimientoCorrectivo = await _context.MantenimientoCorrectivos.FindAsync(id);
            if (mantenimientoCorrectivo != null)
            {
                _context.MantenimientoCorrectivos.Remove(mantenimientoCorrectivo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MantenimientoCorrectivoExists(short id)
        {
            return _context.MantenimientoCorrectivos.Any(e => e.Idmantenimiento == id);
        }
    }
}
