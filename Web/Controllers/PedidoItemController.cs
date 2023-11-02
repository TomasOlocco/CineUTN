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
    public class PedidoItemController : Controller
    {
        private readonly CineUTNContext _context;

        public PedidoItemController(CineUTNContext context)
        {
            _context = context;
        }

        // GET: PedidoItem
        public async Task<IActionResult> Index()
        {
              return _context.PedidoItem != null ? 
                          View(await _context.PedidoItem.ToListAsync()) :
                          Problem("Entity set 'CineUTNContext.PedidoItem'  is null.");
        }

        // GET: PedidoItem/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PedidoItem == null)
            {
                return NotFound();
            }

            var pedidoItem = await _context.PedidoItem
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pedidoItem == null)
            {
                return NotFound();
            }

            return View(pedidoItem);
        }

        // GET: PedidoItem/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PedidoItem/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProductName,Quantity")] PedidoItem pedidoItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pedidoItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pedidoItem);
        }

        // GET: PedidoItem/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PedidoItem == null)
            {
                return NotFound();
            }

            var pedidoItem = await _context.PedidoItem.FindAsync(id);
            if (pedidoItem == null)
            {
                return NotFound();
            }
            return View(pedidoItem);
        }

        // POST: PedidoItem/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProductName,Quantity")] PedidoItem pedidoItem)
        {
            if (id != pedidoItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pedidoItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PedidoItemExists(pedidoItem.Id))
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
            return View(pedidoItem);
        }

        // GET: PedidoItem/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PedidoItem == null)
            {
                return NotFound();
            }

            var pedidoItem = await _context.PedidoItem
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pedidoItem == null)
            {
                return NotFound();
            }

            return View(pedidoItem);
        }

        // POST: PedidoItem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PedidoItem == null)
            {
                return Problem("Entity set 'CineUTNContext.PedidoItem'  is null.");
            }
            var pedidoItem = await _context.PedidoItem.FindAsync(id);
            if (pedidoItem != null)
            {
                _context.PedidoItem.Remove(pedidoItem);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PedidoItemExists(int id)
        {
          return (_context.PedidoItem?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
