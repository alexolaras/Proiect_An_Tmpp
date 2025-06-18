using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proiect_An.Data;
using Proiect_An.Models;
using Proiect_An.Models.DesignPatterns.FactoryMethod;
using Proiect_An.Models.DesignPatterns.Singleton;

namespace Proiect_An.Controllers
{
    public class RoomController : Controller
    {
        private readonly MyAppContext _context;
        private readonly IRoomFactory _roomFactory;

        public RoomController(MyAppContext context, IRoomFactory roomFactory)
        {
            _context = context;
            _roomFactory = roomFactory;
        }

        public async Task<IActionResult> Index()
        {
            var rooms = await _context.Rooms.ToListAsync();
            return View(rooms);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(int id, [Bind("Id, Type, PricePerNight, BedType, IsAvailable")] Room item)
        {
            if (ModelState.IsValid)
            {
                var room = _roomFactory.CreateRoom(item.Type, item.PricePerNight, item.BedType, item.IsAvailable);
                _context.Rooms.Add(room);
                await _context.SaveChangesAsync();

                Logger.Instance.Log("A new room with the id: " + room.Id + ", was created");

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
            if (item != null)
            {
                _context.Rooms.Remove(item);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
    }
}
