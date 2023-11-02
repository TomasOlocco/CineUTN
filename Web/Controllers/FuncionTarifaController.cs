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
    public class FuncionTarifaController : Controller
    {
        private readonly CineUTNContext _context;

        public FuncionTarifaController(CineUTNContext context)
        {
            _context = context;
        }

        // GET: FuncionTarifa
        public async Task<IActionResult> Index()
        {
            var cineUTNContext = _context.FuncionTarifa.Include(f => f.Tarifa);
            return View(await cineUTNContext.ToListAsync());
        }

        // GET: FuncionTarifa/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.FuncionTarifa == null)
            {
                return NotFound();
            }

            var funcionTarifa = await _context.FuncionTarifa
                .Include(f => f.Tarifa)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (funcionTarifa == null)
            {
                return NotFound();
            }

            return View(funcionTarifa);
        }

        // GET: FuncionTarifa/Create
        public IActionResult Create()
        {
            ViewData["TarifaRefId"] = new SelectList(_context.Tarifa, "Id", "Descripcion");
            return View();
        }

        // POST: FuncionTarifa/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TarifaRefId,Created")] FuncionTarifa funcionTarifa)
        {
            if (ModelState.IsValid)
            {
                _context.Add(funcionTarifa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TarifaRefId"] = new SelectList(_context.Tarifa, "Id", "Descripcion", funcionTarifa.TarifaRefId);
            return View(funcionTarifa);
        }

        // GET: FuncionTarifa/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.FuncionTarifa == null)
            {
                return NotFound();
            }

            var funcionTarifa = await _context.FuncionTarifa.FindAsync(id);
            if (funcionTarifa == null)
            {
                return NotFound();
            }
            ViewData["TarifaRefId"] = new SelectList(_context.Tarifa, "Id", "Descripcion", funcionTarifa.TarifaRefId);
            return View(funcionTarifa);
        }

        // POST: FuncionTarifa/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TarifaRefId,Created")] FuncionTarifa funcionTarifa)
        {
            if (id != funcionTarifa.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(funcionTarifa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FuncionTarifaExists(funcionTarifa.Id))
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
            ViewData["TarifaRefId"] = new SelectList(_context.Tarifa, "Id", "Descripcion", funcionTarifa.TarifaRefId);
            return View(funcionTarifa);
        }

        // GET: FuncionTarifa/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.FuncionTarifa == null)
            {
                return NotFound();
            }

            var funcionTarifa = await _context.FuncionTarifa
                .Include(f => f.Tarifa)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (funcionTarifa == null)
            {
                return NotFound();
            }

            return View(funcionTarifa);
        }

        // POST: FuncionTarifa/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.FuncionTarifa == null)
            {
                return Problem("Entity set 'CineUTNContext.FuncionTarifa'  is null.");
            }
            var funcionTarifa = await _context.FuncionTarifa.FindAsync(id);
            if (funcionTarifa != null)
            {
                _context.FuncionTarifa.Remove(funcionTarifa);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FuncionTarifaExists(int id)
        {
          return (_context.FuncionTarifa?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
