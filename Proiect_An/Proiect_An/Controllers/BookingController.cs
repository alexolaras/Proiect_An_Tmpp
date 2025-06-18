using Microsoft.AspNetCore.Mvc;
using Proiect_An.Models;
using Proiect_An.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Proiect_An.Models.DesignPatterns.Singleton;
using static Proiect_An.Models.Enums.RoomService;
using Proiect_An.Models.DesignPatterns.Adapter;
using Proiect_An.Models.DesignPatterns.Observer;

namespace Proiect_An.Controllers
{
    public class BookingController : Controller
    {
        private readonly MyAppContext _context;
        private readonly BookingNotifier _notifier;

        public BookingController(MyAppContext context)
        {   
            _context = context;
            _notifier = new BookingNotifier();
            _notifier.Attach(new LoggerObserver());
            _notifier.Attach(new EmailObserver());
        }   

        [HttpGet]
        public async Task<IActionResult> Create(int? id)
        {
            var guests = await _context.Guests.ToListAsync();
            ViewBag.Guests = guests;
            ViewBag.ServiceTypes = Enum.GetValues(typeof(RoomServiceType)).Cast<RoomServiceType>().ToList();

            var booking = new Booking();
            if (id.HasValue)
                booking.RoomId = id.Value;
            return View(booking);
        }


        [HttpPost]
        public async Task<IActionResult> Create([Bind("GuestId,CheckIn,CheckOut,RoomId")] Booking booking, string[] SelectedServices)
        {
            booking.Services = SelectedServices != null ? string.Join(",", SelectedServices) : "";

            var room = await _context.Rooms.FindAsync(booking.RoomId);
            booking.TotalPrice = BookingDecoratorHelper.CalculateTotal(room,booking.CheckIn,booking.CheckOut, SelectedServices ?? Array.Empty<string>());



            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            booking.Guest = await _context.Guests.FirstOrDefaultAsync(g => g.Id == booking.GuestId);
            _notifier.Notify(booking);

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
        public async Task<IActionResult> Edit(int id, [Bind("Id,GuestId,CheckIn,CheckOut,RoomId")] Booking booking, string[] SelectedServices)
        {
            booking.Services = SelectedServices != null ? string.Join(",", SelectedServices) : "";

            var room = await _context.Rooms.FindAsync(booking.RoomId);
            booking.TotalPrice = BookingDecoratorHelper.CalculateTotal(room, booking.CheckIn, booking.CheckOut, SelectedServices ?? Array.Empty<string>());

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
