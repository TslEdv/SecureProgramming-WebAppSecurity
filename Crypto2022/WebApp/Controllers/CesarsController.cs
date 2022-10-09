using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Domain;

namespace WebApp.Controllers
{
    public class CesarsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CesarsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Cesars
        public async Task<IActionResult> Index()
        {
              return _context.Cesars != null ? 
                          View(await _context.Cesars.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Cesars'  is null.");
        }

        // GET: Cesars/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Cesars == null)
            {
                return NotFound();
            }

            var cesar = await _context.Cesars
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cesar == null)
            {
                return NotFound();
            }

            return View(cesar);
        }

        // GET: Cesars/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cesars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ShiftAmount,PlainText,CipherText")] Cesar cesar)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cesar);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cesar);
        }

        // GET: Cesars/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Cesars == null)
            {
                return NotFound();
            }

            var cesar = await _context.Cesars.FindAsync(id);
            if (cesar == null)
            {
                return NotFound();
            }
            return View(cesar);
        }

        // POST: Cesars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ShiftAmount,PlainText,CipherText")] Cesar cesar)
        {
            if (id != cesar.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cesar);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CesarExists(cesar.Id))
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
            return View(cesar);
        }

        // GET: Cesars/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Cesars == null)
            {
                return NotFound();
            }

            var cesar = await _context.Cesars
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cesar == null)
            {
                return NotFound();
            }

            return View(cesar);
        }

        // POST: Cesars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Cesars == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Cesars'  is null.");
            }
            var cesar = await _context.Cesars.FindAsync(id);
            if (cesar != null)
            {
                _context.Cesars.Remove(cesar);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CesarExists(int id)
        {
          return (_context.Cesars?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
