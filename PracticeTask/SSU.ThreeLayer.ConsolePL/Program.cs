using SSU.ThreeLayer.BLL;
using SSU.ThreeLayer.Common;
using SSU.ThreeLayer.Entities;
using System;
using System.Collections.Generic;

namespace SSU.ThreeLayer.ConsolePL
{
    class Program
    {
        private static IBusinessLogic data = DependencyResolver.BusinessLogic;

        private static void Main(string[] args)
        {
            if (!TryToLogIn())
            {
                return;
            }

            StartMainMenu();
        }

        private static bool TryToLogIn()
        {
            int action = -1;

            while (action != 0)
            {
                Console.WriteLine("You have to log in first:");
                Console.WriteLine("1. Log in");
                Console.WriteLine("2. Create Account");
                Console.WriteLine("0. Exit");
                action = int.Parse(Console.ReadLine());

                Console.Clear();

                switch (action)
                {
                    case 1:
                        Console.Write("Enter login: ");
                        string login = Console.ReadLine();
                        Console.Write("Enter password: ");
                        string password = Console.ReadLine();

                        if (!data.LogIn(login, password))
                        {
                            Console.WriteLine("Incorrect login or password!");
                            Console.ReadKey();
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Success. Welcome, {0}", data.GetCurrentUser().Name);
                            Console.ReadKey();
                            Console.Clear();
                            return true;
                        }


                    case 2:
                        Console.Write("Enter new login: ");
                        string newLogin = Console.ReadLine();
                        Console.Write("Enter new password (at least {0} symbols): ", User.PasswordMinLength);
                        string newPassword = Console.ReadLine();
                        Console.Write("Enter new name: ");
                        string newName = Console.ReadLine();
                        Console.Write("Enter year of birth: ");
                        int yearOfBirth = int.Parse(Console.ReadLine());
                        Console.Write("Enter month of birth: ");
                        int monthOfBirth = int.Parse(Console.ReadLine());
                        Console.Write("Enter day of birth: ");
                        int dayOfBirth = int.Parse(Console.ReadLine());
                        Console.Write("Write something about yourself (optional): ");
                        string newInfo = Console.ReadLine();

                        data.AddUser(new User(newLogin, newPassword, newName, new DateTime(yearOfBirth, monthOfBirth, dayOfBirth), newInfo));
                        data.LogIn(newLogin, newPassword);

                        Console.WriteLine("New user has been created!");
                        Console.ReadKey();
                        Console.Clear();
                        return true;
                }
                Console.Clear();
            }

            return false;
        }

        private static void StartProfileMenu()
        {
            User user = data.GetCurrentUser();

            int action = -1;

            while (action != 0)
            {
                Console.WriteLine("------------------------------------------------");
                Console.WriteLine("Login: {0}", user.Login);
                Console.WriteLine("Name: {0}", user.Name);
                Console.WriteLine("Date of birth: {0}", user.DateOfBirth.ToShortDateString());
                Console.WriteLine("Information: {0}", user.Info);
                Console.WriteLine("------------------------------------------------");
                Console.WriteLine("Choose action:");
                Console.WriteLine("1. Profile: Change password");
                Console.WriteLine("2. Profile: Change name");
                Console.WriteLine("3. Profile: Change info");
                Console.WriteLine("4. Profile: Change date of birth");
                Console.WriteLine("5. Profile: Delete this profile");
                Console.WriteLine("0. Return");
                action = int.Parse(Console.ReadLine());

                Console.Clear();

                switch (action)
                {
                    case 1:
                        {
                            ChangePassword();
                        }
                        break;

                    case 2:
                        {
                            ChangeName();
                        }
                        break;

                    case 3:
                        {
                            ChangeInfo();
                        }
                        break;

                    case 4:
                        {
                            ChangeYearOfBirth();
                        }
                        break;

                    case 5:
                        {
                            DeleteAccount(user);
                        }
                        break;
                }

                Console.Clear();
            }
        }

        private static void DeleteAccount(User user)
        {
            Console.WriteLine("Are you sure? (Write YES if so.)");
            if (Console.ReadLine() == "YES")
            {
                data.DeleteUser(user.Id);
                Console.WriteLine("Current user has been deleted. Press any key to exit.");
                Console.ReadKey();
                Environment.Exit(0);
            }
        }

        private static void ChangeYearOfBirth()
        {
            Console.Write("Enter new year of birth: ");
            int yearOfBirth = int.Parse(Console.ReadLine());
            Console.Write("Enter new month of birth: ");
            int monthOfBirth = int.Parse(Console.ReadLine());
            Console.Write("Enter new day of birth: ");
            int dayOfBirth = int.Parse(Console.ReadLine());
            data.ChangeCurrentUserDateOfBirth(new DateTime(yearOfBirth, monthOfBirth, dayOfBirth));
            Console.WriteLine("Success. Date of birth has been updated.");
            Console.ReadKey();
        }

        private static void ChangeInfo()
        {
            Console.Write("Write something new about yourself: ");
            data.ChangeCurrentUserInfo(Console.ReadLine());
            Console.WriteLine("Success. Information has been updated.");
            Console.ReadKey();
        }

        private static void ChangeName()
        {
            Console.Write("Enter new name: ");
            data.ChangeCurrentUserName(Console.ReadLine());
            Console.WriteLine("Success. Name has been updated.");
            Console.ReadKey();
        }

        private static void ChangePassword()
        {
            Console.Write("Enter new password (at least {0} symbols): ", User.PasswordMinLength);
            data.ChangeCurrentUserPassword(Console.ReadLine());
            Console.WriteLine("Success. Password has been updated.");
            Console.ReadKey();
        }

        private static void StartMainMenu()
        {
            int action = -1;

            while (action != 0)
            {
                try
                {
                    Console.WriteLine("Choose action:");
                    Console.WriteLine("1. Find shop(s) by name");
                    Console.WriteLine("2. Find all shops by city");
                    Console.WriteLine("3. Find all shops by city and type");
                    Console.WriteLine("4. Display all shops in database");
                    Console.WriteLine("5. Rate a shop or update your rating (locate shop number first by using options above)");
                    Console.WriteLine("6. Go to profile settings");

                    if (data.GetCurrentUser().IsAdmin)
                    {
                        Console.WriteLine("7. Admin options...");
                    }

                    Console.WriteLine("0. Exit");
                    action = int.Parse(Console.ReadLine());

                    Console.Clear();

                    switch (action)
                    {
                        case 1:
                            {
                                PrintShopsByName();
                            }
                            break;

                        case 2:
                            {
                                PrintShopsByCity();
                            }
                            break;

                        case 3:
                            {
                                PrintShopsByCityAndType();
                            }
                            break;

                        case 4:
                            {
                                DisplayShops(data.GetAllShops());
                                Console.ReadKey();
                            }
                            break;

                        case 5:
                            {
                                RateShop();
                            }
                            break;

                        case 6:
                            {
                                StartProfileMenu();
                            }
                            break;

                        case 7:
                            {
                                StartAdminMenu();
                            }
                            break;
                    }
                    Console.Clear();
                }
                catch (FormatException e)
                {
                    Console.Clear();
                    Console.WriteLine(e.Message);
                    Console.ReadKey();
                    Console.Clear();
                    continue;
                }
                catch (ArgumentException e)
                {
                    Console.Clear();
                    Console.WriteLine(e.Message);
                    Console.ReadKey();
                    Console.Clear();
                    continue;
                }
            }
        }

        private static void RateShop()
        {
            Console.Write("Enter database number of the shop: ");
            int shopId = int.Parse(Console.ReadLine());
            Console.Write("Enter the rating (1-{0}): ", Shop.MaxRating);
            int rating = int.Parse(Console.ReadLine());
            data.RateShop(shopId, rating);
            Console.WriteLine("Success.");
            Console.ReadKey();
        }

        private static void PrintShopsByCityAndType()
        {
            Console.Write("Enter city: ");
            string city = Console.ReadLine();
            Console.Write("Enter shop type: ");
            string type = Console.ReadLine();
            DisplayShops(data.FindShopsByCityAndType(city, type));
            Console.ReadKey();
        }

        private static void PrintShopsByCity()
        {
            Console.Write("Enter city: ");
            string city = Console.ReadLine();
            DisplayShops(data.FindShopsByCity(city));
            Console.ReadKey();
        }

        private static void PrintShopsByName()
        {
            Console.Write("Enter shop(s) name: ");
            string name = Console.ReadLine();
            DisplayShops(data.FindShopsByName(name));
            Console.ReadKey();
        }

        private static void StartAdminMenu()
        {
            int action = -1;

            while (action != 0)
            {
                Console.WriteLine("Choose action:");
                Console.WriteLine("1. Admin: Add shop");
                Console.WriteLine("2. Admin: Remove shop from database");
                Console.WriteLine("3. Admin: Remove user from database");
                Console.WriteLine("4. Admin: Display all users in the database");
                Console.WriteLine("5. Admin: Clear unused addresses");
                Console.WriteLine("6. Admin: Clear unused shop types");
                Console.WriteLine("0. Return");
                action = int.Parse(Console.ReadLine());

                Console.Clear();

                switch (action)
                {
                    case 1:
                        {
                            AddShop();
                        }
                        break;

                    case 2:
                        {
                            DeleteShop();
                        }
                        break;
                    case 3:
                        {
                            DeleteUser();
                        }
                        break;
                    case 4:
                        {
                            DisplayUsers(data.GetAllUsers());
                            Console.ReadKey();
                        }
                        break;
                    case 5:
                        {
                            ClearAddresses();
                        }
                        break;
                    case 6:
                        {
                            ClearShopTypes();
                        }
                        break;
                }

                Console.Clear();
            }
        }

        private static void ClearShopTypes()
        {
            data.ClearShopTypes();
            Console.WriteLine("Success. Unused shop types have been removed.");
            Console.ReadKey();
        }

        private static void ClearAddresses()
        {
            data.ClearAddresses();
            Console.WriteLine("Success. Unused addresses have been removed.");
            Console.ReadKey();
        }

        private static void DeleteUser()
        {
            Console.Write("Enter database ID (number) of the user to remove: ");
            int index = int.Parse(Console.ReadLine());
            data.DeleteUser(index);
            Console.WriteLine("Success. User with ID {0} has been removed (if it existed).", index);
            Console.ReadKey();
        }

        private static void DeleteShop()
        {
            Console.Write("Enter database ID (number) of the shop to remove: ");
            int index = int.Parse(Console.ReadLine());
            data.DeleteShop(index);
            Console.WriteLine("Success. Shop with ID {0} has been removed (if it existed).", index);
            Console.ReadKey();
        }

        private static void AddShop()
        {
            Console.Write("Enter shop name: ");
            string name = Console.ReadLine();
            Console.Write("Enter shop type: ");
            string type = Console.ReadLine();
            Console.Write("Enter city (part of the address): ");
            string city = Console.ReadLine();
            Console.Write("Enter street (part of the address): ");
            string street = Console.ReadLine();
            Console.Write("Enter building (part of the address): ");
            string building = Console.ReadLine();
            data.AddShop(new Shop(name, type, city, street, building));
            Console.WriteLine("Success. New shop has been added.");
            Console.ReadKey();
        }

        private static void DisplayShops(List<Shop> shops)
        {
            foreach (Shop shop in shops)
            {
                string rating = data.GetShopRatingByIndex(shop.Id);

                Console.WriteLine(shop.ToString() + Environment.NewLine + String.Format("   Rating: {0}", rating));

                Console.WriteLine();
            }
        }

        private static void DisplayUsers(List<User> users)
        {
            foreach (User user in users)
            {
                Console.WriteLine(user.ToString());

                Console.WriteLine();
            }
        }
    }
}
