using BMPClassLibrary.Repository;
using BMPClassLibrary.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BigMamasPizzaria.Pages.Bookings;

public class DeleteModel : PageModel
{
	private readonly BookingRepository _bookingRepository;
	private readonly TableRepository _tableRepository;

	public DeleteModel(BookingRepository bookingRepository, TableRepository tableRepository)
	{
		_bookingRepository = bookingRepository;
		_tableRepository = tableRepository;
	}

	public Booking? Booking { get; set; }
	public Table? Table { get; set; }

	public IActionResult OnGet(int id)
	{
		Booking = _bookingRepository.GetById(id);
		if (Booking == null)
		{
			return NotFound();
		}

		Table = _tableRepository.GetById(Booking.TableId);

		return Page();
	}

	public IActionResult OnPost(int id)
	{
		var booking = _bookingRepository.GetById(id);
		if (booking == null)
		{
			return NotFound();
		}

		_bookingRepository.CancelBooking(id);

		TempData["Message"] = "Booking cancelled successfully!";
		return RedirectToPage("Index");
	}
}
