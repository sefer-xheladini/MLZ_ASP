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
    public class RentsController : Controller
    {
        private readonly Booking2Context _context;

        public RentsController(Booking2Context context)
        {
            _context = context;
        }

        // GET: Rents
        public async Task<IActionResult> Index()
        {
            var booking2Context = _context.Rent.Include(r => r.Building).Include(r => r.User);
            return View(await booking2Context.ToListAsync());
        }

        // GET: Rents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rent = await _context.Rent
                .Include(r => r.Building)
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.RentId == id);
            if (rent == null)
            {
                return NotFound();
            }

            return View(rent);
        }

        // GET: Rents/Create
        public IActionResult Create()
        {
            ViewData["BuildingId"] = new SelectList(_context.Building, "BuildingId", "BuildingId");
            ViewData["UserId"] = new SelectList(_context.User, "UserId", "UserId");
            return View();
        }

        // POST: Rents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RentId,BuildingId,UserId,From,To,PricePerDays,GuestWasHere")] Rent rent)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BuildingId"] = new SelectList(_context.Building, "BuildingId", "BuildingId", rent.BuildingId);
            ViewData["UserId"] = new SelectList(_context.User, "UserId", "UserId", rent.UserId);
            return View(rent);
        }

        // GET: Rents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var building = await _context.Building
                .Include(b => b.BuildingType)
                .Include(b => b.User)
                .Include(b => b.ImageList)
                .FirstOrDefaultAsync(m => m.BuildingId == id);
            ViewData["Building"] = building;
            ViewData["BuildingId"] = new SelectList(_context.Building, "BuildingId", "BuildingId");
            ViewData["UserId"] = new SelectList(_context.User, "UserId", "UserId");
            return View();
        }

        // POST: Rents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RentId,BuildingId,UserId,From,To,PricePerDays,GuestWasHere")] Rent rent)
        {
            rent.GuestWasHere = true;
            if(rent.From > rent.To)
            {
                DateTime temp = rent.From;
                rent.From = rent.To;
                rent.To = temp;
            }
            rent.BuildingId = id;
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RentExists(rent.RentId))
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
            ViewData["BuildingId"] = new SelectList(_context.Building, "BuildingId", "BuildingId", rent.BuildingId);
            ViewData["UserId"] = new SelectList(_context.User, "UserId", "UserId", rent.UserId);
            return View("Views/Buildings/Index.cshtml");
        }

        // GET: Rents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rent = await _context.Rent
                .Include(r => r.Building)
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.RentId == id);
            if (rent == null)
            {
                return NotFound();
            }

            return View(rent);
        }

        // POST: Rents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rent = await _context.Rent.FindAsync(id);
            _context.Rent.Remove(rent);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> CheckAvailability(DateTime fromDate, DateTime toDate)
        {
            if (fromDate > toDate)
            {
                DateTime temp = fromDate;
                fromDate = toDate;
                toDate = temp;
            }
            List<DateTime> allRequestedDates = new List<DateTime>();
            List<DateTime> reservatedDates = new List<DateTime>();
            for (DateTime date = fromDate; date <= toDate; date = date.AddDays(1))
            {
                allRequestedDates.Add(date);
            }

            List<Rent> rents = _context.Rent.ToList();
            foreach(var rent in rents)
            {
                for (DateTime date = rent.From; date <= rent.To; date = date.AddDays(1))
                {
                    reservatedDates.Add(date);
                }
            }
            List<DateTime> duplicates = reservatedDates.Intersect(allRequestedDates).ToList();
            if(duplicates.Count == 0)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }

        }
        

        private bool RentExists(int id)
        {
            return _context.Rent.Any(e => e.RentId == id);
        }
    }
}
