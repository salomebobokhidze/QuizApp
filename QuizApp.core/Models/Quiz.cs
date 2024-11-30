using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuizApp.core.Services;

namespace QuizApp.core.Models
{
    
        public class Quiz
        {
            public int Id { get; set; }
        public string Creator { get; set; } = String.Empty;
            public List<Question> Questions { get; set; }
        }
    }
