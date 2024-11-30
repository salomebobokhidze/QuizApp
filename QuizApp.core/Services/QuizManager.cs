using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace QuizApp.core.Services
{

    using QuizApp.core.Models;
    public class QuizManager
    {
        private readonly JsonStorage _storage;

        public QuizManager(JsonStorage storage)
        {
            _storage = storage;
        }

        public void CreateQuiz(string username)
        {
            var questions = new List<Question>();
            for (int i = 1; i <= 5; i++)
            {
                Console.Write($"Enter question {i}: ");
                var text = Console.ReadLine();

                var options = new List<string>();
                for (int j = 1; j <= 4; j++)
                {
                    Console.Write($"Option {j}: ");
                    options.Add(Console.ReadLine());
                }

                Console.Write("Correct option number (1-4): ");
                var correct = int.Parse(Console.ReadLine()) - 1;

                questions.Add(new Question { Text = text, Options = options, CorrectOption = correct });
            }

            var quiz = new Quiz { Id = _storage.Quizzes.Count + 1, Creator = username, Questions = questions };
            _storage.Quizzes.Add(quiz);
            _storage.SaveData();
            Console.WriteLine(" Quiz created successfully!");
        }

        public void SolveQuiz(User user)
        {
            var availableQuizzes = _storage.Quizzes.Where(q => q.Creator != user.Username).ToList();
            if (!availableQuizzes.Any())
            {
                Console.WriteLine("No quizzes available.");
                return;
            }

            Console.WriteLine("\nAvailable Quizzes:");
            foreach (var newQuiz in availableQuizzes)
                Console.WriteLine($"Quiz ID: {newQuiz.Id}, Creator: {newQuiz.Creator}");

            Console.Write("Enter Quiz ID to solve: ");
            var quizId = int.Parse(Console.ReadLine());
            var quiz = availableQuizzes.FirstOrDefault(q => q.Id == quizId);

            if (quiz == null)
            {
                Console.WriteLine("Invalid Quiz ID.");
                return;
            }

            var startTime = DateTime.Now;
            var timeLimit = TimeSpan.FromMinutes(2);
            int score = 0;

            foreach (var question in quiz.Questions)
            {
                Console.WriteLine(question.Text);
                for (int i = 0; i < question.Options.Count; i++)
                    Console.WriteLine($"{i + 1}. {question.Options[i]}");

                Console.Write("Your answer: ");
                int answer = int.Parse(Console.ReadLine()) - 1;

                if (DateTime.Now - startTime > timeLimit)
                {
                    Console.WriteLine("Time is up! You failed to complete the quiz.");
                    return;
                }

                if (answer == question.CorrectOption)
                    score += 20;
                else
                    score -= 20;
            }

            user.HighScore = Math.Max(user.HighScore, score);
            _storage.SaveData();
            Console.WriteLine($"You scored: {score}");
        }


        public void ViewUserQuizzes(string username)
        {
            var quizzes = _storage.Quizzes.Where(q => q.Creator == username).ToList();
            if (!quizzes.Any())
            {
                Console.WriteLine("No quizzes created.");
                return;
            }

            Console.WriteLine("\nYour Quizzes:");
            foreach (var quiz in quizzes)
                Console.WriteLine($"Quiz ID: {quiz.Id}");
        }


        public void DeleteQuiz(User user)
        {
            var userQuizzes = _storage.Quizzes.Where(q => q.Creator == user.Username).ToList();
            if (!userQuizzes.Any())
            {
                Console.WriteLine("You have no quizzes to delete.");
                return;
            }

            Console.WriteLine("Your Quizzes:");
            foreach (var quiz in userQuizzes)
                Console.WriteLine($"Quiz ID: {quiz.Id}");

            Console.Write("Enter Quiz ID to delete: ");
            var quizId = int.Parse(Console.ReadLine());
            var quizToDelete = userQuizzes.FirstOrDefault(q => q.Id == quizId);

            if (quizToDelete == null)
            {
                Console.WriteLine("Invalid Quiz ID.");
                return;
            }

            _storage.Quizzes.Remove(quizToDelete);
            _storage.SaveData();
            Console.WriteLine("Quiz deleted successfully.");
        }
        public void EditQuiz(User user)
        {
            var userQuizzes = _storage.Quizzes.Where(q => q.Creator == user.Username).ToList();
            if (!userQuizzes.Any())
            {
                Console.WriteLine("You have no quizzes to edit.");
                return;
            }

            Console.WriteLine("Your Quizzes:");
            foreach (var quiz in userQuizzes)
                Console.WriteLine($"Quiz ID: {quiz.Id}");

            Console.Write("Enter Quiz ID to edit: ");
            var quizId = int.Parse(Console.ReadLine());
            var quizToEdit = userQuizzes.FirstOrDefault(q => q.Id == quizId);

            if (quizToEdit == null)
            {
                Console.WriteLine("Invalid Quiz ID.");
                return;
            }


            Console.WriteLine("Editing questions...");
            foreach (var question in quizToEdit.Questions)
            {
                Console.WriteLine($"Current Question: {question.Text}");
                Console.Write("Enter new question text (leave blank to keep current): ");
                var newText = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newText))
                    question.Text = newText;
            }

            _storage.SaveData();
            Console.WriteLine("Quiz updated successfully.");
        }

    }
}

