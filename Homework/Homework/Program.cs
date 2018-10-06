using System;
using System.Collections.Generic;

namespace Homework
{

    enum Options
    {
        Balance,
        CashIn,
        CashOut,
        AddUser,
        ShowUsers,
        Unblock,
        Delete,
        ResetPassword,
        Logout
    }

    struct User
    {
        public string login;
        public string password;
        public double balance;
        public byte role;
        public bool blocked;
        public bool visited;

        public enum Roles
        {
            Admin,
            User
        }

        public User(string l, byte r = (byte)Roles.User)
        {
            login = l;
            password = l.ToLower();
            role = r;
            blocked = false;
            balance = (r == (byte)Roles.Admin) ? 0 : 100.0;
            visited = (r == (byte)Roles.Admin) ? true : false;
        }
    }

    class Program
    {
        static List<User> users = new List<User> { };
        static User profile;
        const char currency = '$';

        static void Main(string[] args)
        {
            users.Add(new User("Admin", (byte)User.Roles.Admin));
            bool isLogin = false;
            bool isMenu;

            while (!isLogin)
            {
                Console.Clear();
                profile = Login();
                isLogin = true;
                isMenu = true;
                List<Options> options = ShowOptions();

                while (isMenu)
                {
                    Console.WriteLine("Выберите номер операции:");

                    for (int i = 0; i < options.Count; i++)
                    {
                        Console.WriteLine($"{i + 1} - {options[i]}");
                    }

                    if (!Byte.TryParse(Console.ReadLine(), out byte choice) || choice == 0 || choice > options.Count + 1)
                    {
                        Console.WriteLine("Такой операции нет в системе");
                        continue;
                    }

                    switch (options[choice - 1])
                    {
                        case Options.AddUser:
                            AddUser();
                            break;

                        case Options.Delete:
                            DeleteUser();
                            break;

                        case Options.ShowUsers:
                            ShowUsers();
                            break;

                        case Options.Unblock:
                            UnblockUser();
                            break;

                        case Options.ResetPassword:
                            ResetPassword();
                            break;

                        case Options.Balance:
                            GetBalance();
                            break;

                        case Options.CashIn:
                            AddCash();
                            break;

                        case Options.CashOut:
                            CashOut();
                            break;

                        case Options.Logout:
                            if (!profile.visited)
                            {
                                SetVisited();
                            }
                            isLogin = false;
                            isMenu = false;
                            break;
                    }
                }
            }

            Console.ReadKey();
        }

        /// <summary>
        /// Users authorization
        /// </summary>
        /// <param name="users">Users of the system</param>
        /// <returns>Authorized User Data</returns>
        public static User Login()
        {
            User[] user = new User[1];
            bool isUser = false;
            const byte maxTry = 3;

            while (!isUser)
            {
                Console.Write("Введите логин: ");
                string login = Console.ReadLine();

                for (int i = 0; i < users.Count; i++)
                {
                    if (login == users[i].login && !users[i].blocked)
                    {
                        for (int j = 1; j <= maxTry; j++)
                        {
                            Console.Write("Введите пароль: ");
                            string password = Console.ReadLine();

                            if (users[i].password == password)
                            {
                                user[0] = users[i];
                                isUser = true;
                                break;
                            }
                            else
                            {
                                if (users[i].role != (byte)User.Roles.Admin)
                                {
                                    if (j == maxTry)
                                    {
                                        User blockedUser = users[i];
                                        blockedUser.blocked = true;
                                        users[i] = blockedUser;

                                        isUser = false;
                                    }
                                    Console.WriteLine($"Не правильный пароль. У вас осталось {maxTry - j} попыток!");
                                }
                                else
                                {
                                    Console.WriteLine($"Не правильный пароль!");
                                }
                            }
                        }

                        break;
                    }
                    else if (login == users[i].login && users[i].blocked)
                    {
                        Console.WriteLine("Пользователь заблокирован!");
                        break;
                    }
                    else if (users.Count == i + 1)
                    {
                        Console.WriteLine("Такого пользователя нет в системе!");

                    }
                }
            }

            return user[0];

        }

        /// <summary>
        /// Options for the selection menu 
        /// </summary>
        /// <returns>Parameters for the authorized user role</returns>
        public static List<Options> ShowOptions()
        {
            List<Options> options = new List<Options> { };

            switch (profile.role)
            {
                case (byte)User.Roles.Admin:
                    options.Add(Options.AddUser);
                    options.Add(Options.Unblock);
                    options.Add(Options.ShowUsers);
                    options.Add(Options.Delete);
                    options.Add(Options.Logout);
                    break;

                case (byte)User.Roles.User:
                    options.Add(Options.Balance);
                    options.Add(Options.CashIn);
                    options.Add(Options.CashOut);
                    if (!profile.visited)
                    {
                        options.Add(Options.ResetPassword);
                    }
                    options.Add(Options.Logout);
                    break;
            }
            return options;
        }

        /// <summary>
        /// Adding a new user
        /// </summary>
        public static void AddUser()
        {
            Console.Write("Введите имя нового пользователя: ");
            string newUser = Console.ReadLine();
            users.Add(new User(newUser));
            Console.WriteLine($"Пользователь {newUser} успешно добавлен!");
        }

        /// <summary>
        /// Delete user
        /// </summary>
        public static void DeleteUser()
        {
            Console.Write("Введите имя пользователя которого хотите удалить: ");
            string deleteUser = Console.ReadLine();
            for (int i = 0; i < users.Count; i++)
            {
                if (users[i].login == deleteUser)
                {
                    users.Remove(users[i]);
                    Console.WriteLine($"Пользователь {deleteUser} успешно удален!");
                    break;

                }
                else if (i == users.Count - 1)
                {
                    Console.WriteLine("Такого пользователя нет в системе!");
                }
            }
        }

        /// <summary>
        /// Show list of users
        /// </summary>
        public static void ShowUsers()
        {
            for (int i = 0; i < users.Count; i++)
            {
                if (users[i].role == (byte)User.Roles.User)
                    Console.WriteLine($"name = {users[i].login}, blocked = {users[i].blocked}, balance = {users[i].balance}");
            }
        }

        /// <summary>
        /// Unblock User
        /// </summary>
        public static void UnblockUser()
        {
            Console.Write("Введите имя пользователя которого хотите разблокировать: ");
            string unblockUser = Console.ReadLine();
            for (int i = 0; i < users.Count; i++)
            {
                if (users[i].login == unblockUser)
                {
                    User unblock = users[i];
                    unblock.blocked = false;
                    users[i] = unblock;
                    Console.WriteLine($"Пользователь {unblockUser} разблоктрован!");
                    break;
                }
                else if (i == users.Count - 1)
                {
                    Console.WriteLine("Такого пользователя нет в системе!");
                }
            }
        }

        /// <summary>
        /// View user balance
        /// </summary>
        public static void GetBalance()
        {
            Console.WriteLine($"{profile.balance}{currency}");
        }

        /// <summary>
        /// Cash out the user's cash
        /// </summary>
        public static void CashOut()
        {
            Console.Write("Введите сумму: ");
            double cash = Convert.ToDouble(Console.ReadLine());

            if (profile.balance >= cash)
            {
                profile.balance -= cash;

                for (int i = 0; i < users.Count; i++)
                {
                    if (users[i].login == profile.login)
                    {
                        users[i] = profile;
                        break;
                    }
                }
                Console.WriteLine("Средства успешно обналичены!");
            } else
            {
                Console.WriteLine("Не достаточно средств на счету!");
            }
        }

        /// <summary>
        /// Cash in the user's cash
        /// </summary>
        public static void AddCash()
        {
            Console.Write("Введите сумму: ");
            profile.balance += Convert.ToDouble(Console.ReadLine());

            for (int i = 0; i < users.Count; i++)
            {
                if (users[i].login == profile.login)
                {
                    users[i] = profile;
                    break;
                }
            }

            Console.WriteLine("Средства успешно добавлены!");
        }

        /// <summary>
        /// Reset the password
        /// </summary>
        public static void ResetPassword()
        {
            Console.Write("Введите новый пароль: ");
            profile.password = Console.ReadLine();

            for (int i = 0; i < users.Count; i++)
            {
                if (users[i].login == profile.login)
                {
                    users[i] = profile;
                    break;
                }
            }

            Console.WriteLine("Пароль успешно изменен!");
        }

        /// <summary>
        /// Mark user visit
        /// </summary>
        public static void SetVisited()
        {
            for (int i = 0; i < users.Count; i++)
            {
                if (users[i].login == profile.login)
                {
                    User activeUser = users[i];
                    activeUser.visited = true;
                    users[i] = activeUser;
                    break;
                }
            }
        }
    }
}
