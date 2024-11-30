using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuizApp.core.Services;

namespace QuizApp.core.Models
{
    public class User
    {
        public string Username { get; set; }=String.Empty;
        public string Password { get; set; }= String.Empty;
        public int HighScore { get; set; }
        public List<int> CreatedQuizIds { get; set; } = new List<int>();
    }
}

