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
    public class FuncionController : Controller
    {
        private readonly CineUTNContext _context;

        public FuncionController(CineUTNContext context)
        {
            _context = context;
        }

        // GET: Funcion
        public async Task<IActionResult> Index()
        {
            var cineUTNContext = _context.Funcion.Include(f => f.Pelicula).Include(f => f.Sala);
            return View(await cineUTNContext.ToListAsync());
        }

        // GET: Funcion/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Funcion == null)
            {
                return NotFound();
            }

            var funcion = await _context.Funcion
                .Include(f => f.Pelicula)
                .Include(f => f.Sala)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (funcion == null)
            {
                return NotFound();
            }

            return View(funcion);
        }

        // GET: Funcion/Create
        public IActionResult Create()
        {
            ViewData["PeliculaRefId"] = new SelectList(_context.Pelicula, "Id", "Id");
            ViewData["SalaRefId"] = new SelectList(_context.Sala, "Id", "Descripcion");
            return View();
        }

        // POST: Funcion/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FechaHoraFuncion,PeliculaRefId,SalaRefId,FechaRegistro")] Funcion funcion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(funcion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PeliculaRefId"] = new SelectList(_context.Pelicula, "Id", "Id", funcion.PeliculaRefId);
            ViewData["SalaRefId"] = new SelectList(_context.Sala, "Id", "Descripcion", funcion.SalaRefId);
            return View(funcion);
        }

        // GET: Funcion/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Funcion == null)
            {
                return NotFound();
            }

            var funcion = await _context.Funcion.FindAsync(id);
            if (funcion == null)
            {
                return NotFound();
            }
            ViewData["PeliculaRefId"] = new SelectList(_context.Pelicula, "Id", "Id", funcion.PeliculaRefId);
            ViewData["SalaRefId"] = new SelectList(_context.Sala, "Id", "Descripcion", funcion.SalaRefId);
            return View(funcion);
        }

        // POST: Funcion/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FechaHoraFuncion,PeliculaRefId,SalaRefId,FechaRegistro")] Funcion funcion)
        {
            if (id != funcion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(funcion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FuncionExists(funcion.Id))
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
            ViewData["PeliculaRefId"] = new SelectList(_context.Pelicula, "Id", "Id", funcion.PeliculaRefId);
            ViewData["SalaRefId"] = new SelectList(_context.Sala, "Id", "Descripcion", funcion.SalaRefId);
            return View(funcion);
        }

        // GET: Funcion/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Funcion == null)
            {
                return NotFound();
            }

            var funcion = await _context.Funcion
                .Include(f => f.Pelicula)
                .Include(f => f.Sala)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (funcion == null)
            {
                return NotFound();
            }

            return View(funcion);
        }

        // POST: Funcion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Funcion == null)
            {
                return Problem("Entity set 'CineUTNContext.Funcion'  is null.");
            }
            var funcion = await _context.Funcion.FindAsync(id);
            if (funcion != null)
            {
                _context.Funcion.Remove(funcion);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FuncionExists(int id)
        {
          return (_context.Funcion?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
