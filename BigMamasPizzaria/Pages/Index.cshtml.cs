using BMPClassLibrary.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BigMamasPizzaria.Pages;

public class IndexModel : PageModel
{
	private readonly BookingRepository _bookings;

	public IndexModel(BookingRepository bookings) => _bookings = bookings;

	public void OnGet()
	{
	}
}
