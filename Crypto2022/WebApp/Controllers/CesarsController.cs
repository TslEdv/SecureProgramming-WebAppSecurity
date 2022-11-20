using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Domain;

namespace WebApp.Controllers
{
    [Authorize]
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
            var applicationDbContext =
                _context
                    .Cesars
                    .Where(c => c.AppUserId == GetLoggedInUserId())
                    .Include(c => c.AppUser);


            return View(await applicationDbContext.ToListAsync());
        }

        public string GetLoggedInUserId()
        {
            return User.Claims.First(cm =>
                cm.Type == ClaimTypes.NameIdentifier).Value;
        }

        // GET: Cesars/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Cesars == null)
            {
                return NotFound();
            }

            var isOwned = await _context.Cesars.AnyAsync(c => c.Id == id && c.AppUserId == GetLoggedInUserId());
            if (!isOwned)
            {
                return NotFound();
            }
            
            var cesar = await _context.Cesars
                .Include(c => c.AppUser)
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
        public async Task<IActionResult> Create(Cesar cesar)
        {
            cesar.AppUserId = GetLoggedInUserId();
            var encryptedText = Helpers.CesarHelper.CesarEncodeByteShift(cesar.PlainText, cesar.ShiftAmount);
            cesar.CipherText = encryptedText;
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

            var cesar = await _context.Cesars
                .SingleOrDefaultAsync(c => c.AppUserId == GetLoggedInUserId() && c.Id == id);
            
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
        public async Task<IActionResult> Edit(int id,
            Cesar cesar)
        {
            if (id != cesar.Id)
            {
                return NotFound();
            }
            
            var isOwned = await _context.Cesars.AnyAsync(c => c.Id == id && c.AppUserId == GetLoggedInUserId());
            if (!isOwned)
            {
                return NotFound();
            }
            cesar.AppUserId = GetLoggedInUserId();
            var encryptedText = Helpers.CesarHelper.CesarEncodeByteShift(cesar.PlainText, cesar.ShiftAmount);
            cesar.CipherText = encryptedText;
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
                .Include(c => c.AppUser)
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
            var isOwned = await _context.Cesars.AnyAsync(c => c.Id == id && c.AppUserId == GetLoggedInUserId());
            if (!isOwned)
            {
                return NotFound();
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