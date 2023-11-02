﻿using System;
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
    public class ListaPrecioController : Controller
    {
        private readonly CineUTNContext _context;

        public ListaPrecioController(CineUTNContext context)
        {
            _context = context;
        }

        // GET: ListaPrecio
        public async Task<IActionResult> Index()
        {
            var cineUTNContext = _context.ListaPrecio.Include(l => l.CondicionPago);
            return View(await cineUTNContext.ToListAsync());
        }

        // GET: ListaPrecio/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ListaPrecio == null)
            {
                return NotFound();
            }

            var listaPrecio = await _context.ListaPrecio
                .Include(l => l.CondicionPago)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (listaPrecio == null)
            {
                return NotFound();
            }

            return View(listaPrecio);
        }

        // GET: ListaPrecio/Create
        public IActionResult Create()
        {
            ViewData["CondicionPagoRefId"] = new SelectList(_context.CondicionPago, "Id", "Id");
            return View();
        }

        // POST: ListaPrecio/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Descripcion,FechaHasta,CondicionPagoRefId,Precio,FechaRegistro")] ListaPrecio listaPrecio)
        {
            if (ModelState.IsValid)
            {
                _context.Add(listaPrecio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CondicionPagoRefId"] = new SelectList(_context.CondicionPago, "Id", "Id", listaPrecio.CondicionPagoRefId);
            return View(listaPrecio);
        }

        // GET: ListaPrecio/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ListaPrecio == null)
            {
                return NotFound();
            }

            var listaPrecio = await _context.ListaPrecio.FindAsync(id);
            if (listaPrecio == null)
            {
                return NotFound();
            }
            ViewData["CondicionPagoRefId"] = new SelectList(_context.CondicionPago, "Id", "Id", listaPrecio.CondicionPagoRefId);
            return View(listaPrecio);
        }

        // POST: ListaPrecio/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descripcion,FechaHasta,CondicionPagoRefId,Precio,FechaRegistro")] ListaPrecio listaPrecio)
        {
            if (id != listaPrecio.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(listaPrecio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ListaPrecioExists(listaPrecio.Id))
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
            ViewData["CondicionPagoRefId"] = new SelectList(_context.CondicionPago, "Id", "Id", listaPrecio.CondicionPagoRefId);
            return View(listaPrecio);
        }

        // GET: ListaPrecio/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ListaPrecio == null)
            {
                return NotFound();
            }

            var listaPrecio = await _context.ListaPrecio
                .Include(l => l.CondicionPago)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (listaPrecio == null)
            {
                return NotFound();
            }

            return View(listaPrecio);
        }

        // POST: ListaPrecio/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ListaPrecio == null)
            {
                return Problem("Entity set 'CineUTNContext.ListaPrecio'  is null.");
            }
            var listaPrecio = await _context.ListaPrecio.FindAsync(id);
            if (listaPrecio != null)
            {
                _context.ListaPrecio.Remove(listaPrecio);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ListaPrecioExists(int id)
        {
          return (_context.ListaPrecio?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
