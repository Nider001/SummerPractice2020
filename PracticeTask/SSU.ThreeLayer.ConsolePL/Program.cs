using SSU.ThreeLayer.BLL;
using SSU.ThreeLayer.Common;
using SSU.ThreeLayer.Entities;
using System;
using System.IO;

namespace SSU.ThreeLayer.ConsolePL
{
    class Program
    {
        static void Main(string[] args)
        {
            IDatabaseLogic data = DependencyResolver.DatabaseLogic;

            if (!TryToLogIn(data))
            {
                return;
            }

            int action = -1;

            while (action != 0)
            {
                try
                {
                    Console.Write("Choose action:" + Environment.NewLine);
                    Console.Write("1. Show all users" + Environment.NewLine);
                    Console.Write("2. Show all awards" + Environment.NewLine);
                    Console.Write("3. Add user" + Environment.NewLine);
                    Console.Write("4. Add award" + Environment.NewLine);
                    Console.Write("5. Assign award" + Environment.NewLine);
                    Console.Write("6. Remove user" + Environment.NewLine);
                    Console.Write("7. Remove award" + Environment.NewLine);
                    Console.Write("8. Unassign award" + Environment.NewLine);
                    //Console.Write("9. Add data from file" + Environment.NewLine);
                    //Console.Write("10. Save database to file" + Environment.NewLine);
                    Console.Write("0. Exit" + Environment.NewLine);
                    action = int.Parse(Console.ReadLine());

                    Console.Clear();

                    /*switch (action)
                    {
                        case 1:
                            ShowUsers(data);
                            Console.ReadKey();
                            break;

                        case 2:
                            ShowAwards(data);
                            Console.ReadKey();
                            break;

                        case 3:
                            Console.Write("Enter user name: ");
                            string name = Console.ReadLine();
                            Console.Write("Enter date of birth: ");
                            DateTime dateOfBirth = Convert.ToDateTime(Console.ReadLine());

                            data.AddUser(new User(name, dateOfBirth));
                            Console.WriteLine("Success");
                            Console.ReadKey();
                            break;

                        case 4:
                            Console.Write("Enter award title: ");
                            string title = Console.ReadLine();

                            data.AddAward(new Award(title));
                            Console.WriteLine("Success");
                            Console.ReadKey();
                            break;

                        case 5:
                            Console.Write("Enter user №: ");
                            uint userId = uint.Parse(Console.ReadLine());
                            Console.Write("Enter award №: ");
                            uint awardId = uint.Parse(Console.ReadLine());

                            data.AddLinker(userId, awardId);
                            Console.WriteLine("Success");
                            Console.ReadKey();
                            break;

                        case 6:
                            Console.Write("Enter user №: ");

                            data.DeleteUser(uint.Parse(Console.ReadLine()));
                            Console.WriteLine("Success");
                            Console.ReadKey();
                            break;

                        case 7:
                            Console.Write("Enter award №: ");

                            data.DeleteAward(uint.Parse(Console.ReadLine()));
                            Console.WriteLine("Success");
                            Console.ReadKey();
                            break;

                        case 8:
                            Console.Write("Enter user №: ");
                            uint userId_r = uint.Parse(Console.ReadLine());
                            Console.Write("Enter award №: ");
                            uint awardId_r = uint.Parse(Console.ReadLine());

                            data.DeleteLinker(userId_r, awardId_r);
                            Console.WriteLine("Success");
                            Console.ReadKey();
                            break;
                    }*/
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

        static bool TryToLogIn(IDatabaseLogic data)
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
                            Console.Write("Enter new password: ");
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

                            data.AddUser(new User(newLogin, newPassword, newName, new DateTime(yearOfBirth, monthOfBirth, dayOfBirth), newInfo, false));
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

        /*static void ShowUsers(IDatabaseLogic databaseLogic)
        {
            foreach (User user in databaseLogic.GetAllUsers())
            {
                var temp = databaseLogic.GetAwardsByUser(user);
                Console.WriteLine(user.GetStringToShow()); //выводим информацию на экран

                foreach (Award award in temp)
                {
                    Console.WriteLine("   " + award.Title + " (№" + award.Id + ")");
                }

                Console.WriteLine();
            }
        }

        static void ShowAwards(IDatabaseLogic databaseLogic)
        {
            foreach (Award award in databaseLogic.GetAllAwards())
            {
                var temp = databaseLogic.GetUsersByAward(award);
                Console.WriteLine(award.GetStringToShow()); //выводим информацию на экран

                foreach (User user in temp)
                {
                    Console.WriteLine("   " + user.Name + " (№" + user.Id + ")");
                }

                Console.WriteLine();
            }
        }*/
    }
}
