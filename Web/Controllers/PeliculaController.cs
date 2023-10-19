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
    public class PeliculaController : Controller
    {
        private readonly CineUTNContext _context;

        public PeliculaController(CineUTNContext context)
        {
            _context = context;
        }

        // GET: Pelicula
        public async Task<IActionResult> Index()
        {
            var cineUTNContext = _context.Pelicula.Include(p => p.Genero).Include(p => p.Subtitulo).Include(p => p.Tipo);
            return View(await cineUTNContext.ToListAsync());
        }

        // GET: Pelicula/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Pelicula == null)
            {
                return NotFound();
            }

            var pelicula = await _context.Pelicula
                .Include(p => p.Genero)
                .Include(p => p.Subtitulo)
                .Include(p => p.Tipo)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pelicula == null)
            {
                return NotFound();
            }

            return View(pelicula);
        }

        // GET: Pelicula/Create
        public IActionResult Create()
        {
            ViewData["GeneroRefId"] = new SelectList(_context.Generos, "Id", "Id");
            ViewData["SubtituloRefId"] = new SelectList(_context.Subtitulo, "Id", "Descripcion");
            ViewData["TipoRefId"] = new SelectList(_context.Tipo, "Id", "Descripcion");
            return View();
        }

        // POST: Pelicula/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Descripcion,ImagemPelicula,Duracion,Clasificacion,GeneroRefId,TipoRefId,SubtituloRefId,FechaEstreno,FechaRegistro")] Pelicula pelicula)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pelicula);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GeneroRefId"] = new SelectList(_context.Generos, "Id", "Id", pelicula.GeneroRefId);
            ViewData["SubtituloRefId"] = new SelectList(_context.Subtitulo, "Id", "Descripcion", pelicula.SubtituloRefId);
            ViewData["TipoRefId"] = new SelectList(_context.Tipo, "Id", "Descripcion", pelicula.TipoRefId);
            return View(pelicula);
        }

        // GET: Pelicula/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Pelicula == null)
            {
                return NotFound();
            }

            var pelicula = await _context.Pelicula.FindAsync(id);
            if (pelicula == null)
            {
                return NotFound();
            }
            ViewData["GeneroRefId"] = new SelectList(_context.Generos, "Id", "Id", pelicula.GeneroRefId);
            ViewData["SubtituloRefId"] = new SelectList(_context.Subtitulo, "Id", "Descripcion", pelicula.SubtituloRefId);
            ViewData["TipoRefId"] = new SelectList(_context.Tipo, "Id", "Descripcion", pelicula.TipoRefId);
            return View(pelicula);
        }

        // POST: Pelicula/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descripcion,ImagemPelicula,Duracion,Clasificacion,GeneroRefId,TipoRefId,SubtituloRefId,FechaEstreno,FechaRegistro")] Pelicula pelicula)
        {
            if (id != pelicula.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pelicula);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PeliculaExists(pelicula.Id))
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
            ViewData["GeneroRefId"] = new SelectList(_context.Generos, "Id", "Id", pelicula.GeneroRefId);
            ViewData["SubtituloRefId"] = new SelectList(_context.Subtitulo, "Id", "Descripcion", pelicula.SubtituloRefId);
            ViewData["TipoRefId"] = new SelectList(_context.Tipo, "Id", "Descripcion", pelicula.TipoRefId);
            return View(pelicula);
        }

        // GET: Pelicula/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Pelicula == null)
            {
                return NotFound();
            }

            var pelicula = await _context.Pelicula
                .Include(p => p.Genero)
                .Include(p => p.Subtitulo)
                .Include(p => p.Tipo)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pelicula == null)
            {
                return NotFound();
            }

            return View(pelicula);
        }

        // POST: Pelicula/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Pelicula == null)
            {
                return Problem("Entity set 'CineUTNContext.Pelicula'  is null.");
            }
            var pelicula = await _context.Pelicula.FindAsync(id);
            if (pelicula != null)
            {
                _context.Pelicula.Remove(pelicula);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PeliculaExists(int id)
        {
          return (_context.Pelicula?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
