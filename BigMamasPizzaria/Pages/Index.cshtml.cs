using BMPClassLibrary.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BigMamasPizzaria.Pages.Bookings;

public class IndexModel : PageModel
{
    private readonly BookingRepository _bookings;

    public IndexModel(BookingRepository bookings) => _bookings = bookings;

    // TODO: public List<Booking> Bookings { get; private set; } = new();

    public void OnGet()
    {
        // TODO: Bookings = _bookings.GetAll();
    }

    public IActionResult OnPostConfirm(int id)
    {
        // TODO: set status to Confirmed
        TempData["Message"] = $"Booking {id} confirmed.";
        return RedirectToPage();
    }

    public IActionResult OnPostCancel(int id)
    {
        // TODO: set status to Cancelled
        TempData["Message"] = $"Booking {id} cancelled.";
        return RedirectToPage();
    }
}