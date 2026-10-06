using System;
using System.Collections.Generic;
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
				if (booking.bookingId == bookingId)
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
				chosenBooking.phone= upDateBooking.phone;
				chosenBooking.name = upDateBooking.name;
				chosenBooking.bookingDate = upDateBooking.bookingDate;
				chosenBooking.bookingId = upDateBooking.bookingId;
			}
			return chosenBooking;
		}
	}

}
