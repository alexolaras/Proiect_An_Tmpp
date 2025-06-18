using Microsoft.AspNetCore.Mvc;
using Proiect_An.Models;
using Proiect_An.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Proiect_An.Models.DesignPatterns.Singleton;

namespace Proiect_An.Controllers
{
    public class GuestController : Controller
    {
        private readonly MyAppContext _context;

        public GuestController(MyAppContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Name,PhoneNumber,Address")] Guest guest)
        {
            _context.Guests.Add(guest);
            await _context.SaveChangesAsync();

            Logger.Instance.Log("New guest with the id: " + guest.Id + ", was added");

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var guest = await _context.Guests.FindAsync(id);
            if (guest == null) return NotFound();

            return View(guest);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,PhoneNumber,Address")] Guest guest)
        {
            if (id != guest.Id) return NotFound();

            _context.Update(guest);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var guest = await _context.Guests.FindAsync(id);
            if (guest == null) return NotFound();

            return View(guest);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var guest = await _context.Guests.FindAsync(id);
            _context.Guests.Remove(guest);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Index()
        {
            var guests = await _context.Guests.ToListAsync();
            return View(guests);
        }
    }
}
