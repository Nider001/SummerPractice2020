using SSU.ThreeLayer.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SSU.ThreeLayer.WebPublicLogic.Models
{
    public class UserVM : User
    {
        public UserVM() : base("123456789")
        {

        }

        public UserVM(string password) : base(password)
        {
        }

        public UserVM(string login, string password, string name, DateTime dateOfBirth, string info) : base(login, password, name, dateOfBirth, info)
        {
        }

        public UserVM(int id, string login, string name, DateTime dateOfBirth, string info, bool isAdmin) : base(id, login, name, dateOfBirth, info, isAdmin)
        {
        }

        [DataType(DataType.Date)]
        public new DateTime DateOfBirth
        {
            get
            {
                return dateOfBirth;
            }
            set
            {
                if (value.Date > DateTime.Now.Date)
                {
                    dateOfBirth = DateTime.Now.Date;
                }
                else
                {
                    dateOfBirth = value.Date;
                }
            }
        }

        public override string ToString()
        {
            return $"{Id} {(IsAdmin ? " Admin" : "Basic")} {Login} {Password} {Name} {DateOfBirth.ToShortDateString()} {Info}";
        }
    }
}