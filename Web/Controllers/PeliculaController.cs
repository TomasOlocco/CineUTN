using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web.Models;
using Web.Repos;
using Web.ViewModel;

namespace Web.Controllers
{
    public class PeliculaController : Controller
    {
        private readonly CineUTNContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PeliculaController(CineUTNContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
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
        //public async Task<IActionResult> Create([Bind("Id,Descripcion,ImagemPelicula,Duracion,Clasificacion,GeneroRefId,TipoRefId,SubtituloRefId,FechaEstreno,FechaRegistro")] Pelicula pelicula)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(pelicula);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["GeneroRefId"] = new SelectList(_context.Generos, "Id", "Id", pelicula.GeneroRefId);
        //    ViewData["SubtituloRefId"] = new SelectList(_context.Subtitulo, "Id", "Descripcion", pelicula.SubtituloRefId);
        //    ViewData["TipoRefId"] = new SelectList(_context.Tipo, "Id", "Descripcion", pelicula.TipoRefId);
        //    return View(pelicula);
        //}
        public async Task<IActionResult> Create(PeliculaViewModel model)
        {
            string uniqueFileName = UploadedFile(model);
            if (ModelState.IsValid)
            {
                Pelicula pelicula = new Pelicula()
                {
                    ImagemPelicula = uniqueFileName,
                    Clasificacion = model.Clasificacion,
                    Descripcion = model.Descripcion,
                    Duracion = model.Duracion,
                    FechaEstreno = model.FechaEstreno,
                    FechaRegistro = model.FechaRegistro,
                    GeneroRefId = model.GeneroRefId,
                    TipoRefId = model.TipoRefId,
                    SubtituloRefId = model.SubtituloRefId,
                };
                _context.Add(pelicula);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GeneroRefId"] = new SelectList(_context.Generos, "Id", "Descripcion", model.GeneroRefId);
            ViewData["TipoRefId"] = new SelectList(_context.Tipo, "Id", "Descripcion", model.TipoRefId);
            ViewData["SubtituloRefId"] = new SelectList(_context.Subtitulo, "Id", "Descripcion", model.SubtituloRefId);
            return View(model);
        }

        // GET: Pelicula/Edit/5
        public async Task<IActionResult> Edit(int id, PeliculaViewModel model)
        {

            string uniqueFileName = UploadedFile(model);
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var pelicula = await _context.Pelicula.FindAsync(id);


                    pelicula.ImagemPelicula = uniqueFileName;
                    pelicula.Clasificacion = model.Clasificacion;
                    pelicula.Descripcion = model.Descripcion;
                    pelicula.Duracion = model.Duracion;
                    pelicula.FechaEstreno = model.FechaEstreno;
                    pelicula.FechaRegistro = model.FechaRegistro;
                    pelicula.GeneroRefId = model.GeneroRefId;
                    pelicula.TipoRefId = model.TipoRefId;
                    pelicula.SubtituloRefId = model.SubtituloRefId;

                    _context.Update(pelicula);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PeliculaExists(model.Id))
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
            ViewData["GeneroRefId"] = new SelectList(_context.Generos, "Id", "Descripcion", model.GeneroRefId);
            ViewData["TipoRefId"] = new SelectList(_context.Tipo, "Id", "Descripcion", model.TipoRefId);
            ViewData["SubtituloRefId"] = new SelectList(_context.Subtitulo, "Id", "Descripcion", model.SubtituloRefId);
            return View(model);
        }

        private string UploadedFile(PeliculaViewModel model)
        {
            string uniqueFileName = null;

            if (model.Imagem != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Imagem.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Imagem.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
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
