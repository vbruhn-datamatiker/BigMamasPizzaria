using System;
using System.Collections.Generic;
using System.Text;

namespace BMPClassLibrary.Model
{
	public class Booking
	{
		public int BookingId { get; set; }
		public string Name { get; set; } = string.Empty;
		public string Phone { get; set; } = string.Empty;
		public DateTime BookingDate { get; set; }
		public int GuestCount { get; set; }
		public int TableId { get; set; }
		public int? UserId { get; set; }
		public DateTime CreatedAt { get; set; } = DateTime.Now;
		public string Status { get; set; } = "Confirmed"; // Confirmed, Cancelled, Completed

		public override string ToString()
		{
			return $"BookingId: {BookingId}, Name: {Name}, Phone: {Phone}, BookingDate: {BookingDate}, GuestCount: {GuestCount}, TableId: {TableId}";
		}
	}
}
