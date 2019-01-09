using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MLZ_Sefer_Xheladini.Models;

namespace MLZ_Sefer_Xheladini.Controllers
{
    public class BuildingsController : Controller
    {
        private readonly MLZ_Sefer_XheladiniContext _context;

        public BuildingsController(MLZ_Sefer_XheladiniContext context)
        {
            _context = context;
        }

        // GET: Buildings
        public async Task<IActionResult> Index()
        {
            var mLZ_Sefer_XheladiniContext = _context.Building.Include(b => b.Image).Include(b => b.User);
            return View(await mLZ_Sefer_XheladiniContext.ToListAsync());
        }

        // GET: Buildings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var building = await _context.Building
                .Include(b => b.Image)
                .Include(b => b.User)
                .FirstOrDefaultAsync(m => m.BuildingId == id);
            if (building == null)
            {
                return NotFound();
            }

            return View(building);
        }

        // GET: Buildings/Create
        public IActionResult Create()
        {
            ViewData["ImageId"] = new SelectList(_context.Set<Image>(), "ImageId", "ImageId");
            ViewData["UserId"] = new SelectList(_context.Set<User>(), "UserId", "UserId");
            return View();
        }

        // POST: Buildings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BuildingId,Title,City,ZipCode,BedCount,Condition,PricePerDay,HasWlan,WlanPrice,HasParking,ParkingPrice,Space,HasBalkony,UserId,IsActive,ImageId")] Building building)
        {
            if (ModelState.IsValid)
            {
                _context.Add(building);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ImageId"] = new SelectList(_context.Set<Image>(), "ImageId", "ImageId", building.ImageId);
            ViewData["UserId"] = new SelectList(_context.Set<User>(), "UserId", "UserId", building.UserId);
            return View(building);
        }

        // GET: Buildings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var building = await _context.Building.FindAsync(id);
            if (building == null)
            {
                return NotFound();
            }
            ViewData["ImageId"] = new SelectList(_context.Set<Image>(), "ImageId", "ImageId", building.ImageId);
            ViewData["UserId"] = new SelectList(_context.Set<User>(), "UserId", "UserId", building.UserId);
            return View(building);
        }

        // POST: Buildings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BuildingId,Title,City,ZipCode,BedCount,Condition,PricePerDay,HasWlan,WlanPrice,HasParking,ParkingPrice,Space,HasBalkony,UserId,IsActive,ImageId")] Building building)
        {
            if (id != building.BuildingId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(building);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BuildingExists(building.BuildingId))
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
            ViewData["ImageId"] = new SelectList(_context.Set<Image>(), "ImageId", "ImageId", building.ImageId);
            ViewData["UserId"] = new SelectList(_context.Set<User>(), "UserId", "UserId", building.UserId);
            return View(building);
        }

        // GET: Buildings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var building = await _context.Building
                .Include(b => b.Image)
                .Include(b => b.User)
                .FirstOrDefaultAsync(m => m.BuildingId == id);
            if (building == null)
            {
                return NotFound();
            }

            return View(building);
        }

        // POST: Buildings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var building = await _context.Building.FindAsync(id);
            _context.Building.Remove(building);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BuildingExists(int id)
        {
            return _context.Building.Any(e => e.BuildingId == id);
        }
    }
}
