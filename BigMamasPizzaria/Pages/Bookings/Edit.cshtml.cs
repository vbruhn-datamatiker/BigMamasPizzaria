using BMPClassLibrary.Repository;
using BMPClassLibrary.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace BigMamasPizzaria.Pages.Bookings;

public class EditModel : PageModel
{
	private readonly BookingRepository _bookingRepository;
	private readonly TableRepository _tableRepository;

	public EditModel(BookingRepository bookingRepository, TableRepository tableRepository)
	{
		_bookingRepository = bookingRepository;
		_tableRepository = tableRepository;
	}

	[BindProperty]
	public BookingEditForm Booking { get; set; } = new();

	public List<Table> AvailableTables { get; set; } = new();

	public IActionResult OnGet(int id)
	{
		var booking = _bookingRepository.GetById(id);
		if (booking == null)
		{
			return NotFound();
		}

		Booking = new BookingEditForm
		{
			BookingId = booking.BookingId,
			Name = booking.Name,
			Phone = booking.Phone,
			BookingDate = booking.BookingDate,
			GuestCount = booking.GuestCount,
			SelectedTableId = booking.TableId,
			Status = booking.Status
		};

		LoadAvailableTables();
		return Page();
	}

	public IActionResult OnPost()
	{
		if (!ModelState.IsValid)
		{
			LoadAvailableTables();
			return Page();
		}

		var booking = _bookingRepository.GetById(Booking.BookingId);
		if (booking == null)
		{
			return NotFound();
		}

		// Verify selected table
		var selectedTable = _tableRepository.GetById(Booking.SelectedTableId);
		if (selectedTable == null)
		{
			ModelState.AddModelError(string.Empty, "Selected table not found.");
			LoadAvailableTables();
			return Page();
		}

		if (selectedTable.Capacity < Booking.GuestCount)
		{
			ModelState.AddModelError(string.Empty, "Selected table capacity is not sufficient for guest count.");
			LoadAvailableTables();
			return Page();
		}

		// Tilføjet: Udskiftet med dette check
        // Check if table is available in the 2-hour slot (excluding current booking)
        if (Booking.Status == "Confirmed" &&
            !_bookingRepository.IsTableAvailable(Booking.SelectedTableId, Booking.BookingDate, Booking.BookingId))
        {
            ModelState.AddModelError(string.Empty,
                $"Table {Booking.SelectedTableId} is already booked between " +
                $"{Booking.BookingDate:HH:mm} and {Booking.BookingDate.AddHours(2):HH:mm}.");
            LoadAvailableTables();
            return Page();
        }


        // Update booking
        var updatedBooking = new Booking
		{
			BookingId = Booking.BookingId,
			Name = Booking.Name,
			Phone = Booking.Phone,
			BookingDate = Booking.BookingDate,
			GuestCount = Booking.GuestCount,
			TableId = Booking.SelectedTableId,
			Status = Booking.Status,
			UserId = booking.UserId,
			CreatedAt = booking.CreatedAt
		};

		_bookingRepository.UpdateBooking(Booking.BookingId, updatedBooking);

		TempData["Message"] = "Booking updated successfully!";
		return RedirectToPage("Index");
	}

	private void LoadAvailableTables()
	{
		var allTables = _tableRepository.ListAllTable();

		// Show all tables that can fit the guests, including the one currently reserved
		AvailableTables = allTables
			.Where(t => t.Capacity >= Booking.GuestCount)
			.OrderBy(t => t.Capacity)
			.ToList();
	}
}

public class BookingEditForm
{
	public int BookingId { get; set; }

	[Required(ErrorMessage = "Name is required")]
	[StringLength(100)]
	public string Name { get; set; } = string.Empty;

	[Required(ErrorMessage = "Phone is required")]
	[Phone(ErrorMessage = "Please provide a valid phone number")]
	public string Phone { get; set; } = string.Empty;

	[Required(ErrorMessage = "Booking date is required")]
	public DateTime BookingDate { get; set; }

	[Required(ErrorMessage = "Guest count is required")]
	[Range(1, 20, ErrorMessage = "Guest count must be between 1 and 20")]
	public int GuestCount { get; set; }

	[Required(ErrorMessage = "Table is required")]
	public int SelectedTableId { get; set; }

	public string Status { get; set; } = "Confirmed";
}
