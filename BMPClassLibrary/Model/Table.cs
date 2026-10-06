using System;
using System.Collections.Generic;
using System.Text;

namespace BMPClassLibrary.Model
{
    public class Table
    {
        public int TableId { get; set; }
        public int Capacity { get; set; }  
        public DateTime TimeSlot { get; set; }
        public bool IsAvailable { get; set; }

		public override string ToString()
		{
			return $"TableId: {TableId}, Capacity: {Capacity}, TimeSlot: {TimeSlot}, IsAvailable: {IsAvailable}";
		}
	}
}
