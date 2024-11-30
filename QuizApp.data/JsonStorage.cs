
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace QuizApp.data
{
    
    public class JsonStorage
    {
        private const string FilePath = "quizapp_data.json";

        public List<User> Users { get; set; } = new List<User>();
        public List<Quiz> Quizzes { get; set; } = new List<Quiz>();

        public void LoadData()
        {
            if (!File.Exists(FilePath)) return;

            var jsonData = File.ReadAllText(FilePath);
            var data = JsonSerializer.Deserialize<JsonStorage>(jsonData);

            if (data != null)
            {
                Users = data.Users ?? new List<User>();
                Quizzes = data.Quizzes ?? new List<Quiz>();
            }
        }

        public void SaveData()
        {
            var jsonData = JsonSerializer.Serialize(this);
            File.WriteAllText(FilePath, jsonData);
        }
    }
}
