using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuizApp.core.Services;

namespace QuizApp.core.Models
{
        public class Question
        {
            public string Text { get; set; }=String.Empty;
        public List<string> Options { get; set; } 
            public int CorrectOption { get; set; }
        }
    }
