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
    public class RSAsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RSAsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: RSAs
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.RSAs
                .Where(r => r.AppUserId == GetLoggedInUserId())
                .Include(r => r.AppUser);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: RSAs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.RSAs == null)
            {
                return NotFound();
            }

            var isOwned = await _context.RSAs.AnyAsync(c => c.Id == id && c.AppUserId == GetLoggedInUserId());
            if (!isOwned)
            {
                return NotFound();
            }
            
            var rSA = await _context.RSAs
                .Include(r => r.AppUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rSA == null)
            {
                return NotFound();
            }

            return View(rSA);
        }

        // GET: RSAs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RSAs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RSA rSA)
        {
            rSA.AppUserId = GetLoggedInUserId();
            rSA.PrimeP = Helpers.RsaHelper.CheckPrimesP(rSA.PrimeP, rSA.PrimeQ);
            rSA.PrimeQ = Helpers.RsaHelper.CheckPrimesQ(rSA.PrimeP, rSA.PrimeQ);
            rSA.CipherText = Helpers.RsaHelper.RsaEncryption(rSA.PlainText, rSA.PrimeP, rSA.PrimeQ);
            if (ModelState.IsValid)
            {
                _context.Add(rSA);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(rSA);
        }

        // GET: RSAs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.RSAs == null)
            {
                return NotFound();
            }

            var isOwned = await _context.RSAs.AnyAsync(c => c.Id == id && c.AppUserId == GetLoggedInUserId());
            if (!isOwned)
            {
                return NotFound();
            }
            
            var rSA = await _context.RSAs.FindAsync(id);
            if (rSA == null)
            {
                return NotFound();
            }

            return View(rSA);
        }

        // POST: RSAs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, RSA rSA)
        {
            if (id != rSA.Id)
            {
                return NotFound();
            }

            var isOwned = await _context.RSAs.AnyAsync(c => c.Id == id && c.AppUserId == GetLoggedInUserId());
            if (!isOwned)
            {
                return NotFound();
            }

            rSA.AppUserId = GetLoggedInUserId();
            rSA.PrimeP = Helpers.RsaHelper.CheckPrimesP(rSA.PrimeP, rSA.PrimeQ);
            rSA.PrimeQ = Helpers.RsaHelper.CheckPrimesQ(rSA.PrimeP, rSA.PrimeQ);
            rSA.CipherText = Helpers.RsaHelper.RsaEncryption(rSA.PlainText, rSA.PrimeP, rSA.PrimeQ);
            
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rSA);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RSAExists(rSA.Id))
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
            return View(rSA);
        }

        // GET: RSAs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.RSAs == null)
            {
                return NotFound();
            }

            var isOwned = await _context.RSAs.AnyAsync(c => c.Id == id && c.AppUserId == GetLoggedInUserId());
            if (!isOwned)
            {
                return NotFound();
            }
            
            var rSA = await _context.RSAs
                .Include(r => r.AppUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rSA == null)
            {
                return NotFound();
            }

            return View(rSA);
        }

        // POST: RSAs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.RSAs == null)
            {
                return Problem("Entity set 'ApplicationDbContext.RSAs'  is null.");
            }
            
            var isOwned = await _context.RSAs.AnyAsync(c => c.Id == id && c.AppUserId == GetLoggedInUserId());
            if (!isOwned)
            {
                return NotFound();
            }
            
            var rSA = await _context.RSAs.FindAsync(id);
            if (rSA != null)
            {
                _context.RSAs.Remove(rSA);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RSAExists(int id)
        {
          return (_context.RSAs?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        
        public string GetLoggedInUserId()
        {
            return User.Claims.First(cm =>
                cm.Type == ClaimTypes.NameIdentifier).Value;
        }
    }
}
