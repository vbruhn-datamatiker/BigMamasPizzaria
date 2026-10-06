using System;
using System.Collections.Generic;
using System.Text;
using BMPClassLibrary.Model;

namespace BMPClassLibrary.Repository
{
    public class UserRepository
    {
        private List<User> _users;


        public UserRepository()
        {
            _users = new List<User>();
        }

        public UserRepository(List<User> users)
        {
            _users = users;
        }

        public List<User> users
        {
            get { return _users; }
            set { _users = value; }
        }

        public void AddUser(User user)
        {
            _users.Add(user);
        }

        public void RemoveUser(int userId)
		{
			_users.Remove(GetById(userId));
		}

        public User GetById(int userId)
		{
			foreach (User user in _users)
			{
				if (user.UserId == userId)
				{
					return user;
				}
			}
			return null;
		}

        public List<User> ListAllUsers()
		{
			return new List<User>(_users);
		}

        public User UpdateUser(int id, User upDateUser)
        { 
        User chosenUser = GetById(id);

            if (chosenUser != null)
            {
                chosenUser.FirstName = upDateUser.FirstName;
                chosenUser.LastName = upDateUser.LastName;
                chosenUser.Email = upDateUser.Email;
                chosenUser.Phone = upDateUser.Phone;
            }
            return chosenUser;

		}
	}
}
