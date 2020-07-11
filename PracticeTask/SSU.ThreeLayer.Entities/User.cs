using System;

namespace SSU.ThreeLayer.Entities
{
    public class User
    {
        private const string protectedPassword = "***";
        private const string superSpecialStr = "You-shall-fear-me";
        private const string notSoSpecialStr = "Admin of this database";

        public static int PasswordMinLength { get; } = protectedPassword.Length + 1;

        public int Id { get; private set; } = 0;
        public string Login { get; set; }

        private string password;
        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                if (value.Length < PasswordMinLength || value == protectedPassword)
                {
                    throw new ArgumentException("Invalid password.");
                }
                else
                {
                    password = value;
                }
            }
        }

        public bool IsPasswordKnown
        {
            get
            {
                return !password.Equals(protectedPassword);
            }
        }

        public string Name { get; set; }

        private DateTime dateOfBirth;
        public DateTime DateOfBirth
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

        public string Info { get; set; }
        public bool IsAdmin { get; private set; }

        public User(string login, string password, string name, DateTime dateOfBirth, string info)
        {
            Login = login;
            Password = password;
            Name = name;
            DateOfBirth = dateOfBirth;
            Info = info;
            
            if (Info == superSpecialStr)
            {
                IsAdmin = true;
                Info = notSoSpecialStr;
            }
        }

        public User(int id, string login, string name, DateTime dateOfBirth, string info, bool isAdmin)
        {
            Id = id;
            Login = login;
            password = protectedPassword; //evade checks
            Name = name;
            DateOfBirth = dateOfBirth;
            Info = info;
            IsAdmin = isAdmin;
        }

        public override string ToString()
        {
            return "User № " + Id + (IsAdmin ? " (admin)" : "") + Environment.NewLine + "   Login: " + Login + Environment.NewLine + "   Name: " + Name
                    + Environment.NewLine + "   Password: " + Password + Environment.NewLine + "   Date of birth: " + DateOfBirth.ToShortDateString() + Environment.NewLine + "   Info: " + Info;
        }
    }
}
