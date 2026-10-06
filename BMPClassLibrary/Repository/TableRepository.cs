using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using BMPClassLibrary.Model;

namespace BMPClassLibrary.Repository
{
	public class TableRepository
	{
		private List<Table> _tables;

		public TableRepository()
		{
			_tables = new List<Table>();
		}

		public TableRepository(List<Table> tables)
		{
			_tables = tables;
		}

		public List<Table> Tables
		{
			get { return _tables; }
			set { _tables = value; }
		}

		public void AddTable(Table newTable)
		{
			_tables.Add(newTable);
		}

		public Table GetById(int tableId)
		{
			foreach (Table table in _tables)
			{
				if (table.TableId == tableId)
				{
					return table;
				}
			}
			return null;
		}

		public void Remove(int tableIdToRemove)
		{
			_tables.Remove(GetById(tableIdToRemove));
		}

		public List<Table> ListAllTable()
		{
		return new List<Table>(_tables);
		}

		public Table UpdateTable(int id, Table upDateTable)
		{
			Table chosenTable = GetById(id);
			if(chosenTable != null)
			{
		chosenTable.TableId = id;
		chosenTable.Capacity = upDateTable.Capacity;
		chosenTable.IsAvailable = upDateTable.IsAvailable;
		chosenTable.TimeSlot = upDateTable.TimeSlot;
			}
			return chosenTable;
		}
		
	}
}
