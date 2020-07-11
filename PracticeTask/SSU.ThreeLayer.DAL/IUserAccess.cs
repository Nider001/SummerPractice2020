using System.Collections;
using System.Collections.Generic;
using SSU.ThreeLayer.Entities;

namespace SSU.ThreeLayer.DAL
{
    public interface IUserAccess
    {
        void AddUser(User user);
        void DeleteUser(int index);

        List<User> GetAllUsers();

        User GetUser(int index);
        User GetUser(string login);
    }
}
