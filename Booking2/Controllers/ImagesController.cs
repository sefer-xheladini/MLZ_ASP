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
    public class ImagesController : Controller
    {
        private readonly Booking2Context _context;

        public ImagesController(Booking2Context context)
        {
            _context = context;
        }

        public IActionResult Image(int id)
        {
            var image = this._context.Image.SingleOrDefault(x => x.ImageId == id);
            if (image == null)
                return null; //TODO: was sollte hier sinnvollerweise zurückgegeben werden?

            //using(var ms = new System.IO.MemoryStream(albumImage.Image))
            //{
            //    return new FileStreamResult(ms, albumImage.ContentType);
            //}

            return new FileContentResult(image.Content, image.ContentType);
        }

        //// GET: Images
        //public async Task<IActionResult> Index()
        //{
        //    var booking2Context = _context.Image.Include(i => i.Building);
        //    return View(await booking2Context.ToListAsync());
        //}

        //// GET: Images/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var image = await _context.Image
        //        .Include(i => i.Building)
        //        .FirstOrDefaultAsync(m => m.ImageId == id);
        //    if (image == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(image);
        //}

        //// GET: Images/Create
        //public IActionResult Create()
        //{
        //    ViewData["BuildingId"] = new SelectList(_context.Building, "BuildingId", "BuildingId");
        //    return View();
        //}

        //// POST: Images/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("ImageId,Name,Description,Content,ContentType,BuildingId")] Image image)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(image);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["BuildingId"] = new SelectList(_context.Building, "BuildingId", "BuildingId", image.BuildingId);
        //    return View(image);
        //}

        //// GET: Images/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var image = await _context.Image.FindAsync(id);
        //    if (image == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["BuildingId"] = new SelectList(_context.Building, "BuildingId", "BuildingId", image.BuildingId);
        //    return View(image);
        //}

        //// POST: Images/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("ImageId,Name,Description,Content,ContentType,BuildingId")] Image image)
        //{
        //    if (id != image.ImageId)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(image);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!ImageExists(image.ImageId))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["BuildingId"] = new SelectList(_context.Building, "BuildingId", "BuildingId", image.BuildingId);
        //    return View(image);
        //}

        //// GET: Images/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var image = await _context.Image
        //        .Include(i => i.Building)
        //        .FirstOrDefaultAsync(m => m.ImageId == id);
        //    if (image == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(image);
        //}

        //// POST: Images/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var image = await _context.Image.FindAsync(id);
        //    _context.Image.Remove(image);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool ImageExists(int id)
        //{
        //    return _context.Image.Any(e => e.ImageId == id);
        //}
    }
}
