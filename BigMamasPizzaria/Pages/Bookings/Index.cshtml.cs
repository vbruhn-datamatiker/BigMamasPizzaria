using BMPClassLibrary.Repository;
using BMPClassLibrary.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace BigMamasPizzaria.Pages.Bookings;

public class IndexModel : PageModel
{
	private readonly BookingRepository _bookingRepository;
	private readonly TableRepository _tableRepository;

	public IndexModel(BookingRepository bookingRepository, TableRepository tableRepository)
	{
		_bookingRepository = bookingRepository;
		_tableRepository = tableRepository;
	}

	public List<BookingDisplay> Bookings { get; set; } = new();

	[BindProperty(SupportsGet = true)]
	public string? FilterStatus { get; set; }

	[BindProperty(SupportsGet = true)]
	public string? Search { get; set; }

	public void OnGet()
	{
		var allBookings = _bookingRepository.ListAllBookings();

		// Filter by status if specified
		if (!string.IsNullOrWhiteSpace(FilterStatus))
		{
			allBookings = allBookings.Where(b => b.Status == FilterStatus).ToList();
		}

		// Filter by search (name or phone)
		if (!string.IsNullOrWhiteSpace(Search))
		{
			allBookings = allBookings.Where(b =>
				b.Name.Contains(Search, StringComparison.OrdinalIgnoreCase) ||
				b.Phone.Contains(Search, StringComparison.OrdinalIgnoreCase)
			).ToList();
		}

		// Convert to display model
		Bookings = allBookings.Select(b => new BookingDisplay
		{
			BookingId = b.BookingId,
			Name = b.Name,
			Phone = b.Phone,
			BookingDate = b.BookingDate,
			GuestCount = b.GuestCount,
			TableId = b.TableId,
			Status = b.Status,
			CreatedAt = b.CreatedAt
		}).OrderByDescending(b => b.BookingDate).ToList();
	}
}

public class BookingDisplay
{
	public int BookingId { get; set; }
	public string Name { get; set; } = string.Empty;
	public string Phone { get; set; } = string.Empty;
	public DateTime BookingDate { get; set; }
	public int GuestCount { get; set; }
	public int TableId { get; set; }
	public string Status { get; set; } = string.Empty;
	public DateTime CreatedAt { get; set; }
}
