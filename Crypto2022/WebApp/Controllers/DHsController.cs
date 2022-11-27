using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Domain;

namespace WebApp.Controllers
{
    [Authorize]
    public class DHsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DHsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DHs
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.DHs
                .Where(d => d.AppUserId == GetLoggedInUserId())
                .Include(d => d.AppUser);
            
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: DHs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.DHs == null)
            {
                return NotFound();
            }

            var isOwned = await _context.DHs.AnyAsync(c => c.Id == id && c.AppUserId == GetLoggedInUserId());
            if (!isOwned)
            {
                return NotFound();
            }
            var dH = await _context.DHs
                .Include(d => d.AppUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dH == null)
            {
                return NotFound();
            }

            return View(dH);
        }

        // GET: DHs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DHs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DH dH)
        {
            dH.AppUserId = GetLoggedInUserId();
            dH.Prime = Helpers.DiffieHellmanHelper.GetPrime(dH.Prime);
            dH.CalculationX = Helpers.DiffieHellmanHelper.ModPow(dH.Base, dH.ValueA, dH.Prime);
            dH.CalculationY = Helpers.DiffieHellmanHelper.ModPow(dH.Base, dH.ValueB, dH.Prime);
            if (ModelState.IsValid)
            {
                _context.Add(dH);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dH);
        }

        // GET: DHs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.DHs == null)
            {
                return NotFound();
            }

            var isOwned = await _context.DHs.AnyAsync(c => c.Id == id && c.AppUserId == GetLoggedInUserId());
            if (!isOwned)
            {
                return NotFound();
            }
            var dH = await _context.DHs.FindAsync(id);
            if (dH == null)
            {
                return NotFound();
            }
            return View(dH);
        }

        // POST: DHs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DH dH)
        {
            if (id != dH.Id)
            {
                return NotFound();
            }

            
            var isOwned = await _context.DHs.AnyAsync(c => c.Id == id && c.AppUserId == GetLoggedInUserId());
            if (!isOwned)
            {
                return NotFound();
            }

            dH.AppUserId = GetLoggedInUserId();
            dH.Prime = Helpers.DiffieHellmanHelper.GetPrime(dH.Prime);
            dH.CalculationX = Helpers.DiffieHellmanHelper.ModPow(dH.Base, dH.ValueA, dH.Prime);
            dH.CalculationY = Helpers.DiffieHellmanHelper.ModPow(dH.Base, dH.ValueB, dH.Prime);
            
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dH);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DHExists(dH.Id))
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
            return View(dH);
        }

        // GET: DHs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.DHs == null)
            {
                return NotFound();
            }
            
            var isOwned = await _context.DHs.AnyAsync(c => c.Id == id && c.AppUserId == GetLoggedInUserId());
            if (!isOwned)
            {
                return NotFound();
            }

            var dH = await _context.DHs
                .Include(d => d.AppUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dH == null)
            {
                return NotFound();
            }

            return View(dH);
        }

        // POST: DHs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.DHs == null)
            {
                return Problem("Entity set 'ApplicationDbContext.DHs'  is null.");
            }
            
            var isOwned = await _context.DHs.AnyAsync(c => c.Id == id && c.AppUserId == GetLoggedInUserId());
            if (!isOwned)
            {
                return NotFound();
            }
            var dH = await _context.DHs.FindAsync(id);
            if (dH != null)
            {
                _context.DHs.Remove(dH);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DHExists(int id)
        {
          return (_context.DHs?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        public string GetLoggedInUserId()
        {
            return User.Claims.First(cm =>
                cm.Type == ClaimTypes.NameIdentifier).Value;
        }
    }
}
