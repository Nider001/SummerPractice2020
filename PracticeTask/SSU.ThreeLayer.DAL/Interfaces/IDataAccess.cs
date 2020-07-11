using SSU.ThreeLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSU.ThreeLayer.DAL
{
    public interface IDataAccess : IShopAccess, IUserAccess
    {
        User GetCurrentUser();

        void ChangeCurrentUserPassword(string newPassword);
        void ChangeCurrentUserName(string newName);
        void ChangeCurrentUserInfo(string newInfo);
        void ChangeCurrentUserDateOfBirth(System.DateTime newDate);

        bool LogIn(string login, string password);
    }
}
