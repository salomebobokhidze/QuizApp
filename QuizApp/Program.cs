using System;
using QuizApp.core.Models;
using QuizApp.core.Services;
using QuizApp.core;

namespace QuizApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var storage = new JsonStorage();
            var accountService = new AccountService(storage);
            var quizManager = new QuizManager(storage);
            User currentUser = null;


            Console.WriteLine("🎮 Welcome to the Quiz Game!");

            bool appRunning = true;
            while (appRunning)
            {
                Console.WriteLine("\n1. Register");
                Console.WriteLine("2. Login");
                Console.WriteLine("3. Exit");
                

                Console.Write("Choose an option: ");
                var input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        RegisterUser(accountService, ref currentUser);
                        break;
                    case "2":
                        LoginUser(accountService, quizManager, ref currentUser);
                        break;
                    case "3":
                        appRunning = false;
                        break;
                    default:
                        Console.WriteLine("Invalid input. Try again.");
                        break;
                }
            }

            Console.WriteLine("Goodbye!");
        }

        static void RegisterUser(AccountService accountService, ref User currentUser)
        {
            Console.Write("Enter username: ");
            var username = Console.ReadLine();
            Console.Write("Enter password: ");
            var password = Console.ReadLine();

            try
            {
                accountService.Register(username, password);
                currentUser = accountService.Authenticate(username, password); // Automatically log in the user after registration
                Console.WriteLine("Registration successful!");
                Console.WriteLine($"Welcome, {currentUser.Username}!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
            }
        }
        static void LoginUser(AccountService accountService, QuizManager quizManager, ref User currentUser)
        {
            Console.Write("Enter username: ");
            var username = Console.ReadLine();
            Console.Write("Enter password: ");
            var password = Console.ReadLine();

            try
            {
                currentUser = accountService.Authenticate(username, password);
                Console.WriteLine($"Welcome back, {currentUser.Username}!");

                bool userMenu = true;
                while (userMenu)
                {
                    Console.WriteLine("\n1. Create Quiz");
                    Console.WriteLine("2. Solve Quiz");
                    Console.WriteLine("3. View My Quizzes");
                    Console.WriteLine("4. View Leaderboard");
                    Console.WriteLine("5. Edit Your Quiz");
                    Console.WriteLine("6. Delete Your Quiz");
                    Console.WriteLine("7. Logout");
                    Console.Write("Choose an option: ");
                    var choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            quizManager.CreateQuiz(currentUser.Username);
                            break;
                        case "2":
                            quizManager.SolveQuiz(currentUser);
                            break;
                        case "3":
                            quizManager.ViewUserQuizzes(currentUser.Username);
                            break;
                        case "4":
                            accountService.ShowLeaderboard();
                            break;
                        case "5":
                            quizManager.EditQuiz(currentUser);
                            break;
                        case "6":
                            quizManager.DeleteQuiz(currentUser);
                            break;
                        case "7":
                            userMenu = false;
                            break;
                        default:
                            Console.WriteLine("Invalid choice. Try again.");
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
            }
        }
    }
}
