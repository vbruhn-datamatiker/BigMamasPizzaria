using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMPClassLibrary.Model;

namespace BMPClassLibrary.Repository
{
	public class BookingRepository
	{
		private List<Booking> _bookings;
		
		// Duration constant
        public static readonly TimeSpan BookingDuration = TimeSpan.FromHours(2);

        public BookingRepository()
		{
			_bookings = new List<Booking>();
		}

		public BookingRepository(List<Booking> bookings)
		{
			_bookings = bookings;
		}

		public List<Booking> Bookings
		{
			get { return _bookings; }
			set { _bookings = value; }
		}

		public void AddBooking(Booking booking)
		{
			// Tilføjet: AddBooking afviser en booking der allerede eksisterer
			if (booking.Status == "Confirmed" && !IsTableAvailable(booking.TableId, booking.BookingDate))
            {
                throw new InvalidOperationException(
                    $"Table {booking.TableId} is already booked between " +
                    $"{booking.BookingDate:HH:mm} and {(booking.BookingDate + BookingDuration):HH:mm}.");
            }

            _bookings.Add(booking);
		}

		public void RemoveBooking(int bookingId)
		{
			_bookings.Remove(GetById(bookingId));
		}

		public Booking GetById(int bookingId)
		{
			foreach (Booking booking in _bookings)
			{
				if (booking.BookingId == bookingId)
				{
					return booking;
				}
			}
			return null;
		}

		public List<Booking> ListAllBookings()
		{
			return new List<Booking>(_bookings);
		}

		public Booking UpdateBooking(int id, Booking upDateBooking)
		{
			Booking chosenBooking = GetById(id);
			if (chosenBooking != null)
			{
				// Tilføjet: Afviser hvis der allerede eksisterer en booking i tidsrummet
                if (upDateBooking.Status == "Confirmed" && !IsTableAvailable(upDateBooking.TableId, upDateBooking.BookingDate, id))
                {
                    throw new InvalidOperationException(
                        $"Table {upDateBooking.TableId} is not available at {upDateBooking.BookingDate:dd-MM-yyyy HH:mm}.");
                }

                chosenBooking.Phone = upDateBooking.Phone;
				chosenBooking.Name = upDateBooking.Name;
				chosenBooking.BookingDate = upDateBooking.BookingDate;
				chosenBooking.GuestCount = upDateBooking.GuestCount;
				chosenBooking.TableId = upDateBooking.TableId;
				chosenBooking.Status = upDateBooking.Status;
			}
			return chosenBooking;
		}

		public List<Booking> GetBookingsByTableAndDate(int tableId, DateTime bookingDate)
		{
			return _bookings.Where(b => 
				b.TableId == tableId && 
				b.BookingDate.Date == bookingDate.Date &&
				b.Status == "Confirmed").ToList();
		}

		public bool IsTableAvailable(int tableId, DateTime bookingDate, int? excludeBookingId = null)
		{
			// var bookingsForTable = GetBookingsByTableAndDate(tableId, bookingDate);
			// return bookingsForTable.Count == 0;

			// Overlap check - så en booking ikke clasher med sig selv når man opdaterer den
			DateTime newStart = bookingDate;
			DateTime newEnd = bookingDate + BookingDuration;

			return !_bookings.Any(b =>
			b.TableId == tableId &&
			b.Status == "Confirmed" &&
			b.BookingId != excludeBookingId &&
			newStart < b.BookingDate + BookingDuration &&
			b.BookingDate < newEnd);

		}

		public List<Booking> GetBookingsByDate(DateTime date)
		{
			return _bookings.Where(b => b.BookingDate.Date == date.Date && b.Status == "Confirmed").ToList();
		}

		public List<Booking> GetActiveBookings()
		{
			// Opdateret med BookingDuration > DateTime
			return _bookings.Where(b => b.BookingDate + BookingDuration > DateTime.Now && b.Status == "Confirmed").ToList();
		}

		public void CancelBooking(int bookingId)
		{
			var booking = GetById(bookingId);
			if (booking != null)
			{
				booking.Status = "Cancelled";
			}
		}



	}
}
