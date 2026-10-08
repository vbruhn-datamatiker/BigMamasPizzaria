using System;
using System.Collections.Generic;
using System.Text;
using BMPClassLibrary.Model;

namespace BMPClassLibrary.Repository
{
	public class AdminRepository
	{
		private List<Admin> _admins;

		public AdminRepository()
		{
			_admins = new List<Admin>();
		}
		public AdminRepository(List<Admin> admins)
		{
			_admins = admins;
		}

		public List<Admin> admins
		{
			get { return _admins; }
			set { _admins = value; }
		}

		public void AddAdmin(Admin admin)
		{
			_admins.Add(admin);
		}
		public Admin GetById(int id)
		{
			foreach (Admin a in _admins)
			{
				if (a.AdminId == id)
				{
					return a;
				}
			}
			return null;
		}
		public void RemoveAdmin(int id)
		{
			_admins.Remove(GetById(id));
		}

		public Admin AdminUpdate(int id, Admin updatedAdmin)
		{
			Admin chosenAdmin = GetById(id);
			if (chosenAdmin != null)
			{
				chosenAdmin.FirstName = updatedAdmin.FirstName;
				chosenAdmin.LastName = updatedAdmin.LastName;
				chosenAdmin.Email = updatedAdmin.Email;
				chosenAdmin.AdminPassword = updatedAdmin.AdminPassword;
			}
			return chosenAdmin;
		}

		public List<Admin> ListAllAdmins()
		{
			return new List<Admin>(_admins);
		}
	}
}


