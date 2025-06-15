using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Proiect_An.Data;
using Proiect_An.Models;

namespace Proiect_An.Controllers
{
    public class BookingController : Controller
    {
        private readonly MyAppContext _context;

        public BookingController(MyAppContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var bookings = _context.Bookings.Include(b => b.Room).ToList();
            return View(bookings);
        }

        public IActionResult Create(int id)
        {
            var room = _context.Rooms.Find(id);
            if (room == null)
            {
                return NotFound();
            }

            var booking = new Booking
            {
                RoomId = room.Id,
            };

            return View(booking);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Booking booking)
        {

            Console.WriteLine("POST Create hit");
            Console.WriteLine($"RoomId: {booking.RoomId}, GuestName: {booking.GuestName}, CheckIn: {booking.CheckIn}, CheckOut: {booking.CheckOut}");

            if (!ModelState.IsValid)
            {
                Console.WriteLine("ModelState is invalid!");
                foreach (var entry in ModelState)
                {
                    foreach (var error in entry.Value.Errors)
                    {
                        Console.WriteLine($"Field '{entry.Key}' error: {error.ErrorMessage}");
                    }
                }
            }
            var room = await _context.Rooms.FindAsync(booking.RoomId);
            if (room == null)
            {
                ModelState.AddModelError("", "Selected room does not exist.");
            }

            if (ModelState.IsValid)
            {
                booking.Room = room;
                _context.Bookings.Add(booking);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            if (room != null)
            {
                ViewBag.RoomType = room.Type;
                ViewBag.RoomPrice = room.PricePerNight;
            }

            return View(booking);
        }
    }
}
