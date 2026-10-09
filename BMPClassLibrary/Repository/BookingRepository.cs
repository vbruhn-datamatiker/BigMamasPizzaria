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

		public bool IsTableAvailable(int tableId, DateTime bookingDate)
		{
			var bookingsForTable = GetBookingsByTableAndDate(tableId, bookingDate);
			return bookingsForTable.Count == 0;
		}

		public List<Booking> GetBookingsByDate(DateTime date)
		{
			return _bookings.Where(b => b.BookingDate.Date == date.Date && b.Status == "Confirmed").ToList();
		}

		public List<Booking> GetActiveBookings()
		{
			return _bookings.Where(b => b.BookingDate >= DateTime.Now && b.Status == "Confirmed").ToList();
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
