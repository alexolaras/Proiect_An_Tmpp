using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proiect_An.Data;
using Proiect_An.Models;
using System.Net.NetworkInformation;

namespace Proiect_An.Controllers
{
    public class RoomController : Controller
    {
        private readonly MyAppContext _context;

        public RoomController(MyAppContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var item = await _context.Rooms.ToListAsync();
            return View(item);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id, Type, PricePerNight, BedType, IsAvailable")] Room item)
        {
            if (ModelState.IsValid)
            {
                _context.Rooms.Add(item);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            else
            {
                return View(item);
            }
        }
        public async Task<IActionResult> Edit(int id)
        {
            var item = await _context.Rooms.FirstOrDefaultAsync(x => x.Id == id);
            return View(item);  
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id, Type, PricePerNight, BedType, IsAvailable")] Room item)
        {
            if (ModelState.IsValid)
            {
                _context.Update(item);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(item);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.Rooms.FirstOrDefaultAsync(x => x.Id == id);
            return View(item);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var item = await _context.Rooms.FindAsync(id);
            if(item != null)
            {
                _context.Rooms.Remove(item);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
    }
}
