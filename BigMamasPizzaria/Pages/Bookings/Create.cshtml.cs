using BMPClassLibrary.Repository;
using BMPClassLibrary.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace BigMamasPizzaria.Pages.Bookings;

public class CreateModel : PageModel
{
    private readonly BookingRepository _bookingRepository;
    private readonly TableRepository _tableRepository;

    public CreateModel(BookingRepository bookingRepository, TableRepository tableRepository)
    {
        _bookingRepository = bookingRepository;
        _tableRepository = tableRepository;
    }

    [BindProperty]
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; } = "";

    [BindProperty]
    [Required(ErrorMessage = "Phone is required")]
    [Phone(ErrorMessage = "Please provide a valid phone number")]
    public string Phone { get; set; } = "";

    [BindProperty]
    [Required(ErrorMessage = "Booking date is required")]
    public DateTime BookingDate { get; set; } = DateTime.Today.AddDays(1).AddHours(18);

    [BindProperty]
    [Required(ErrorMessage = "Guest count is required")]
    [Range(1, 20, ErrorMessage = "Guest count must be between 1 and 20")]
    public int GuestCount { get; set; } = 2;

    [BindProperty]
    [Required(ErrorMessage = "Table is required")]
    public int SelectedTableId { get; set; }

    public List<Table> AvailableTables { get; set; } = new();

    public IActionResult OnGet()
    {
        LoadAvailableTables();
        return Page();
    }

    public IActionResult OnPost()
    {
        // Validate end time is after start time shouldn't apply to our booking model
        if (!ModelState.IsValid)
        {
            LoadAvailableTables();
            return Page();
        }

        // Verify selected table exists and is available
        var selectedTable = _tableRepository.GetById(SelectedTableId);
        if (selectedTable == null)
        {
            ModelState.AddModelError(string.Empty, "Selected table not found.");
            LoadAvailableTables();
            return Page();
        }

        if (selectedTable.Capacity < GuestCount)
        {
            ModelState.AddModelError(string.Empty, "Selected table capacity is not sufficient for guest count.");
            LoadAvailableTables();
            return Page();
        }

        if (!_bookingRepository.IsTableAvailable(SelectedTableId, BookingDate))
        {
            // Tilføjet: Error message indeholder nu også timeslot
            ModelState.AddModelError(string.Empty,
                $"Table {SelectedTableId} is already booked between " +
                $"{BookingDate:HH:mm} and {BookingDate.AddHours(2):HH:mm}. Please choose another time or table.");

            LoadAvailableTables();
            return Page();
        }

        // Create booking with next available ID
        int nextId = _bookingRepository.ListAllBookings().Count > 0
            ? _bookingRepository.ListAllBookings().Max(b => b.BookingId) + 1
            : 1;

        var booking = new Booking
        {
            BookingId = nextId,
            Name = Name,
            Phone = Phone,
            BookingDate = BookingDate,
            GuestCount = GuestCount,
            TableId = SelectedTableId,
            Status = "Confirmed"
        };

        _bookingRepository.AddBooking(booking);

        TempData["Message"] = "Booking confirmed successfully!";
        return RedirectToPage("Index");
    }

    // Handler, only reloads the tables
    public IActionResult OnPostFindTables()
    {
        // Only looking up tables - don't show "Name is required" etc.
        ModelState.Clear();

        LoadAvailableTables();

        // If the chosen table isn't free at the new time, clear the choice
        if (!AvailableTables.Any(t => t.TableId == SelectedTableId))
        {
            SelectedTableId = 0;
        }

        return Page();
    }


    private void LoadAvailableTables()
    {
        var allTables = _tableRepository.ListAllTable();

        // Filter tables: capacity must be >= guest count, and must be available at selected time
        AvailableTables = allTables
            .Where(t => t.Capacity >= GuestCount && _bookingRepository.IsTableAvailable(t.TableId, BookingDate))
            .ToList();
    }
}