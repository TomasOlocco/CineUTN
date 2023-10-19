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
    public class SubtituloController : Controller
    {
        private readonly CineUTNContext _context;

        public SubtituloController(CineUTNContext context)
        {
            _context = context;
        }

        // GET: Subtitulo
        public async Task<IActionResult> Index()
        {
              return _context.Subtitulo != null ? 
                          View(await _context.Subtitulo.ToListAsync()) :
                          Problem("Entity set 'CineUTNContext.Subtitulo'  is null.");
        }

        // GET: Subtitulo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Subtitulo == null)
            {
                return NotFound();
            }

            var subtitulo = await _context.Subtitulo
                .FirstOrDefaultAsync(m => m.Id == id);
            if (subtitulo == null)
            {
                return NotFound();
            }

            return View(subtitulo);
        }

        // GET: Subtitulo/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Subtitulo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Descripcion,FechaRegistro")] Subtitulo subtitulo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(subtitulo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(subtitulo);
        }

        // GET: Subtitulo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Subtitulo == null)
            {
                return NotFound();
            }

            var subtitulo = await _context.Subtitulo.FindAsync(id);
            if (subtitulo == null)
            {
                return NotFound();
            }
            return View(subtitulo);
        }

        // POST: Subtitulo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descripcion,FechaRegistro")] Subtitulo subtitulo)
        {
            if (id != subtitulo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(subtitulo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubtituloExists(subtitulo.Id))
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
            return View(subtitulo);
        }

        // GET: Subtitulo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Subtitulo == null)
            {
                return NotFound();
            }

            var subtitulo = await _context.Subtitulo
                .FirstOrDefaultAsync(m => m.Id == id);
            if (subtitulo == null)
            {
                return NotFound();
            }

            return View(subtitulo);
        }

        // POST: Subtitulo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Subtitulo == null)
            {
                return Problem("Entity set 'CineUTNContext.Subtitulo'  is null.");
            }
            var subtitulo = await _context.Subtitulo.FindAsync(id);
            if (subtitulo != null)
            {
                _context.Subtitulo.Remove(subtitulo);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SubtituloExists(int id)
        {
          return (_context.Subtitulo?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
