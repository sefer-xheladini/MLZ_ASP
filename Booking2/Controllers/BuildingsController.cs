using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Booking2.Models;

namespace Booking2.Controllers
{
    public class BuildingsController : Controller
    {
        private readonly Booking2Context _context;

        public BuildingsController(Booking2Context context)
        {
            _context = context;
        }

        // GET: Buildings
        public async Task<IActionResult> Index()
        {
            var booking2Context = _context.Building.Include(b => b.BuildingType).Include(b => b.User).Include(b => b.ImageList);
            return View(await booking2Context.ToListAsync());
        }

        // GET: Buildings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var building = await _context.Building
                .Include(b => b.BuildingType)
                .Include(b => b.User)
                .Include(b => b.ImageList)
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
            ViewData["BuildingTypeId"] = new SelectList(_context.Set<BuildingType>(), "BuildingTypeId", "BuildingTypeId");
            ViewData["UserId"] = new SelectList(_context.User, "UserId", "UserId");
            return View();
        }

        // POST: Buildings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BuildingId,Title,City,ZipCode,BedCount,Condition,PricePerDay,HasWlan,WlanPrice,HasParking,ParkingPrice,Space,HasBalkony,IsActive,BuildingTypeId,UserId")] Building building, List<Microsoft.AspNetCore.Http.IFormFile> files)
        {
            if (ModelState.IsValid)
            {
                if (files != null)
                {
                    foreach (var file in files.Where(f => f.Length > 0))
                    {
                        using (var stream = file.OpenReadStream())
                        {
                            using (var br = new System.IO.BinaryReader(stream))
                            {
                                var buildingImage = new Image();
                                buildingImage.Content = br.ReadBytes((int)file.Length);
                                buildingImage.ContentType = file.ContentType;

                                if (building.ImageList == null)
                                    building.ImageList = new List<Image>();
                                building.ImageList.Add(buildingImage);
                            }
                        }
                    }
                }
                _context.Add(building);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BuildingTypeId"] = new SelectList(_context.Set<BuildingType>(), "BuildingTypeId", "BuildingTypeId", building.BuildingTypeId);
            ViewData["UserId"] = new SelectList(_context.User, "UserId", "UserId", building.UserId);
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
            ViewData["BuildingTypeId"] = new SelectList(_context.Set<BuildingType>(), "BuildingTypeId", "BuildingTypeId", building.BuildingTypeId);
            ViewData["UserId"] = new SelectList(_context.User, "UserId", "UserId", building.UserId);
            return View(building);
        }

        // POST: Buildings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BuildingId,Title,City,ZipCode,BedCount,Condition,PricePerDay,HasWlan,WlanPrice,HasParking,ParkingPrice,Space,HasBalkony,IsActive,BuildingTypeId,UserId")] Building building)
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
            ViewData["BuildingTypeId"] = new SelectList(_context.Set<BuildingType>(), "BuildingTypeId", "BuildingTypeId", building.BuildingTypeId);
            ViewData["UserId"] = new SelectList(_context.User, "UserId", "UserId", building.UserId);
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
                .Include(b => b.BuildingType)
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
