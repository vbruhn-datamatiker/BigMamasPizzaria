using BMPClassLibrary.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BigMamasPizzaria.Pages.Bookings;

public class CreateModel : PageModel
{
    private readonly BookingRepository _bookings;

    public CreateModel(BookingRepository bookings) => _bookings = bookings;

    [BindProperty] public string Name { get; set; } = "";
    [BindProperty] public string Phone { get; set; } = "";
    [BindProperty] public DateTime Start { get; set; } = DateTime.Today.AddHours(18);
    [BindProperty] public DateTime End { get; set; } = DateTime.Today.AddHours(20);
    [BindProperty] public int GuestCount { get; set; } = 2;

    public void OnGet() { }

    public IActionResult OnPost()
    {
        if (End <= Start)
            ModelState.AddModelError(string.Empty, "End must be after start.");
        if (!ModelState.IsValid) return Page();

        // TODO: build a Booking from the fields above and add it via _bookings
        TempData["Message"] = "Booking requested – awaiting confirmation.";
        return RedirectToPage("Index");
    }
}