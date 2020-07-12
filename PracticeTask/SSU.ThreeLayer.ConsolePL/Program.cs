using SSU.ThreeLayer.BLL;
using SSU.ThreeLayer.Common;
using SSU.ThreeLayer.Entities;
using System;
using System.Collections.Generic;
using System.IO;

namespace SSU.ThreeLayer.ConsolePL
{
    class Program
    {
        static void Main(string[] args)
        {
            IBusinessLogic data = DependencyResolver.BusinessLogic;

            if (!TryToLogIn(data))
            {
                return;
            }

            MainMenu(data);
        }

        static bool TryToLogIn(IBusinessLogic data)
        {
            int action = -1;

            while (action != 0)
            {
                try
                {
                    Console.Write("You have to log in first:" + Environment.NewLine);
                    Console.Write("1. Log in" + Environment.NewLine);
                    Console.Write("2. Create Account" + Environment.NewLine);
                    Console.Write("0. Exit" + Environment.NewLine);
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
                                Console.WriteLine("Success. Welcome, " + data.GetCurrentUser().Name);
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

            return false;
        }

        static void ProfileMenu(IBusinessLogic data)
        {
            User user = data.GetCurrentUser();

            int action = -1;

            while (action != 0)
            {
                try
                {
                    Console.Write("------------------------------------------------" + Environment.NewLine);
                    Console.Write("Login: " + user.Login + Environment.NewLine);
                    Console.Write("Name: " + user.Name + Environment.NewLine);
                    Console.Write("Date of birth: " + user.DateOfBirth.ToShortDateString() + Environment.NewLine);
                    Console.Write("Information: " + user.Info + Environment.NewLine);
                    Console.Write("------------------------------------------------" + Environment.NewLine);
                    Console.Write("Choose action:" + Environment.NewLine);
                    Console.Write("1. Profile: Change password" + Environment.NewLine);
                    Console.Write("2. Profile: Change name" + Environment.NewLine);
                    Console.Write("3. Profile: Change info" + Environment.NewLine);
                    Console.Write("4. Profile: Change date of birth" + Environment.NewLine);
                    Console.Write("5. Profile: Delete this profile" + Environment.NewLine);
                    Console.Write("0. Return" + Environment.NewLine);
                    action = int.Parse(Console.ReadLine());

                    Console.Clear();

                    switch (action)
                    {
                        case 1:
                            {
                                Console.Write("Enter new password (at least {0} symbols): ", User.PasswordMinLength);
                                data.ChangeCurrentUserPassword(Console.ReadLine());
                                Console.WriteLine("Success. Password has been updated.");
                                Console.ReadKey();
                            }
                            break;

                        case 2:
                            {
                                Console.Write("Enter new name: ");
                                data.ChangeCurrentUserName(Console.ReadLine());
                                Console.WriteLine("Success. Name has been updated.");
                                Console.ReadKey();
                            }
                            break;

                        case 3:
                            {
                                Console.Write("Write something new about yourself: ");
                                data.ChangeCurrentUserInfo(Console.ReadLine());
                                Console.WriteLine("Success. Information has been updated.");
                                Console.ReadKey();
                            }
                            break;

                        case 4:
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
                            break;

                        case 5:
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

        static void MainMenu(IBusinessLogic data)
        {
            int action = -1;

            while (action != 0)
            {
                try
                {
                    Console.Write("Choose action:" + Environment.NewLine);
                    Console.Write("1. Find shop(s) by name" + Environment.NewLine);
                    Console.Write("2. Find all shops by city" + Environment.NewLine);
                    Console.Write("3. Find all shops by city and type" + Environment.NewLine);
                    Console.Write("4. Display all shops in database" + Environment.NewLine);
                    Console.Write("5. Rate a shop or update your rating (locate shop number first by using options above)" + Environment.NewLine);
                    Console.Write("6. Go to profile settings" + Environment.NewLine);

                    if (data.GetCurrentUser().IsAdmin)
                    {
                        Console.Write("7. Admin options..." + Environment.NewLine);
                    }

                    Console.Write("0. Exit" + Environment.NewLine);
                    action = int.Parse(Console.ReadLine());

                    Console.Clear();

                    switch (action)
                    {
                        case 1:
                            {
                                Console.Write("Enter shop(s) name: ");
                                string name = Console.ReadLine();
                                DisplayShops(data, data.FindShopsByName(name));
                                Console.ReadKey();
                            }
                            break;

                        case 2:
                            {
                                Console.Write("Enter city: ");
                                string city = Console.ReadLine();
                                DisplayShops(data, data.FindShopsByCity(city));
                                Console.ReadKey();
                            }
                            break;

                        case 3:
                            {
                                Console.Write("Enter city: ");
                                string city = Console.ReadLine();
                                Console.Write("Enter shop type: ");
                                string type = Console.ReadLine();
                                DisplayShops(data, data.FindShopsByCityAndType(city, type));
                                Console.ReadKey();
                            }
                            break;

                        case 4:
                            {
                                DisplayShops(data, data.GetAllShops());
                                Console.ReadKey();
                            }
                            break;

                        case 5:
                            {
                                Console.Write("Enter database number of the shop: ");
                                int shopId = int.Parse(Console.ReadLine());
                                Console.Write("Enter the rating (1-{0}): ", Shop.MaxRating);
                                int rating = int.Parse(Console.ReadLine());
                                data.RateShop(shopId, rating);
                                Console.WriteLine("Success.");
                                Console.ReadKey();
                            }
                            break;

                        case 6:
                            {
                                ProfileMenu(data);
                            }
                            break;

                        case 7:
                            {
                                AdminMenu(data);
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

        static void AdminMenu(IBusinessLogic data)
        {
            int action = -1;

            while (action != 0)
            {
                try
                {
                    Console.Write("Choose action:" + Environment.NewLine);
                    Console.Write("1. Admin: Add shop" + Environment.NewLine);
                    Console.Write("2. Admin: Remove shop from database" + Environment.NewLine);
                    Console.Write("3. Admin: Remove user from database" + Environment.NewLine);
                    Console.Write("4. Admin: Display all users in the database" + Environment.NewLine);
                    Console.Write("5. Admin: Clear unused addresses" + Environment.NewLine);
                    Console.Write("6. Admin: Clear unused shop types" + Environment.NewLine);
                    Console.Write("0. Return" + Environment.NewLine);
                    action = int.Parse(Console.ReadLine());

                    Console.Clear();

                    switch (action)
                    {
                        case 1:
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
                            break;

                        case 2:
                            {
                                Console.Write("Enter database ID (number) of the shop to remove: ");
                                int index = int.Parse(Console.ReadLine());
                                data.DeleteShop(index);
                                Console.WriteLine("Success. Shop with ID {0} has been removed (if it existed).", index);
                                Console.ReadKey();
                            }
                            break;
                        case 3:
                            {
                                Console.Write("Enter database ID (number) of the user to remove: ");
                                int index = int.Parse(Console.ReadLine());
                                data.DeleteUser(index);
                                Console.WriteLine("Success. User with ID {0} has been removed (if it existed).", index);
                                Console.ReadKey();
                            }
                            break;
                        case 4:
                            {
                                DisplayUsers(data, data.GetAllUsers());
                                Console.ReadKey();
                            }
                            break;
                        case 5:
                            {
                                data.ClearAddresses();
                                Console.WriteLine("Success. Unused addresses have been removed.");
                                Console.ReadKey();
                            }
                            break;
                        case 6:
                            {
                                data.ClearShopTypes();
                                Console.WriteLine("Success. Unused shop types have been removed.");
                                Console.ReadKey();
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


        static void DisplayShops(IBusinessLogic data, List<Shop> shops)
        {
            foreach (Shop shop in shops)
            {
                string rating = data.GetShopRatingByIndex(shop.Id);

                Console.WriteLine(shop.ToString() + Environment.NewLine + String.Format("   Rating: {0}", rating));

                Console.WriteLine();
            }
        }

        static void DisplayUsers(IBusinessLogic data, List<User> users)
        {
            foreach (User user in users)
            {
                Console.WriteLine(user.ToString());

                Console.WriteLine();
            }
        }
    }
}
