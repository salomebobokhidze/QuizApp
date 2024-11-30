using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace QuizApp.core.Services
{
    
    using QuizApp.core.Models;
    public class AccountService
        {
            private readonly JsonStorage _storage;

            public AccountService(JsonStorage storage)
            {
                _storage = storage;
            }

            public void Register(string username, string password)
            {
                if (_storage.Users.Any(u => u.Username == username))
                    throw new Exception("Username is already taken.");
                _storage.Users.Add(new User { Username = username, Password = password });
                _storage.SaveData();
            }

            public User Authenticate(string username, string password)
            {
                return _storage.Users.FirstOrDefault(u => u.Username == username && u.Password == password)
                       ?? throw new Exception("Invalid credentials.");
            }

            public void ShowLeaderboard()
            {
                var topUsers = _storage.Users.OrderByDescending(u => u.HighScore).Take(10).ToList();
                Console.WriteLine("\nLeaderboard:");
                foreach (var user in topUsers)
                    Console.WriteLine($"{user.Username}: {user.HighScore} points");
            }
        }
    }

