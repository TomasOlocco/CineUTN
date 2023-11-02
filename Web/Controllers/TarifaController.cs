using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web.Models;
using Web.Repos;

namespace Web.Controllers
{
    public class TarifaController : Controller
    {
        private readonly CineUTNContext _context;

        public TarifaController(CineUTNContext context)
        {
            _context = context;
        }

        // GET: Tarifa
        public async Task<IActionResult> Index()
        {
            var cineUTNContext = _context.Tarifa.Include(t => t.ListaPrecio);
            return View(await cineUTNContext.ToListAsync());
        }

        // GET: Tarifa/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Tarifa == null)
            {
                return NotFound();
            }

            var tarifa = await _context.Tarifa
                .Include(t => t.ListaPrecio)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tarifa == null)
            {
                return NotFound();
            }

            return View(tarifa);
        }

        // GET: Tarifa/Create
        public IActionResult Create()
        {
            ViewData["ListaPrecioRefId"] = new SelectList(_context.ListaPrecio, "Id", "Descripcion");
            return View();
        }

        // POST: Tarifa/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Descripcion,PorcentajeDescuento,ListaPrecioRefId,TarifaPrecio,FechaRegistro")] Tarifa tarifa)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tarifa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ListaPrecioRefId"] = new SelectList(_context.ListaPrecio, "Id", "Descripcion", tarifa.ListaPrecioRefId);
            return View(tarifa);
        }

        // GET: Tarifa/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Tarifa == null)
            {
                return NotFound();
            }

            var tarifa = await _context.Tarifa.FindAsync(id);
            if (tarifa == null)
            {
                return NotFound();
            }
            ViewData["ListaPrecioRefId"] = new SelectList(_context.ListaPrecio, "Id", "Descripcion", tarifa.ListaPrecioRefId);
            return View(tarifa);
        }

        // POST: Tarifa/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descripcion,PorcentajeDescuento,ListaPrecioRefId,TarifaPrecio,FechaRegistro")] Tarifa tarifa)
        {
            if (id != tarifa.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tarifa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TarifaExists(tarifa.Id))
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
            ViewData["ListaPrecioRefId"] = new SelectList(_context.ListaPrecio, "Id", "Descripcion", tarifa.ListaPrecioRefId);
            return View(tarifa);
        }

        // GET: Tarifa/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Tarifa == null)
            {
                return NotFound();
            }

            var tarifa = await _context.Tarifa
                .Include(t => t.ListaPrecio)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tarifa == null)
            {
                return NotFound();
            }

            return View(tarifa);
        }

        // POST: Tarifa/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Tarifa == null)
            {
                return Problem("Entity set 'CineUTNContext.Tarifa'  is null.");
            }
            var tarifa = await _context.Tarifa.FindAsync(id);
            if (tarifa != null)
            {
                _context.Tarifa.Remove(tarifa);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TarifaExists(int id)
        {
          return (_context.Tarifa?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
