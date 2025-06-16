using Microsoft.AspNetCore.Mvc;
using Proiect_An.Models;
using Proiect_An.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Proiect_An.Controllers
{
    public class BookingController : Controller
    {
        private readonly MyAppContext _context;

        public BookingController(MyAppContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Create(int? id)
        {
            var guests = await _context.Guests.ToListAsync();
            ViewBag.Guests = guests;

            var booking = new Booking();
            if (id.HasValue)
                booking.RoomId = id.Value;
            return View(booking);
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("GuestId,CheckIn,CheckOut,RoomId")] Booking booking)
        {
            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var booking = await _context.Bookings
                .Include(b => b.Guest)
                .FirstOrDefaultAsync(b => b.Id == id);
            if (booking == null) return NotFound();

            ViewBag.Guests = await _context.Guests.ToListAsync();
            return View(booking);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,GuestId,CheckIn,CheckOut,RoomId")] Booking booking)
        {
            if (id != booking.Id) return NotFound();

            _context.Update(booking);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var booking = await _context.Bookings
                .Include(b => b.Guest)
                .FirstOrDefaultAsync(b => b.Id == id);
            if (booking == null) return NotFound();

            return View(booking);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Index()
        {
            var bookings = await _context.Bookings
        .Include(b => b.Guest)
        .ToListAsync();
            return View(bookings);
        }
    }
}
