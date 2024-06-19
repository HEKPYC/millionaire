using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace QuizProgram
{
    /// <summary>
    /// Логика взаимодействия для QuizResultsWindow.xaml
    /// </summary>
    public partial class QuizResultsWindow : Window
    {
        string Topic;
        DataBase dataBase;
        User questionsUser;
        List<Questions> Quiz;
        List<int> Users_answers;
        int Mark;
        int Right_answers;
        StringBuilder _textBuilder;
        StringBuilder _rankBuilder;
        public QuizResultsWindow(string topic, List<Questions> quiz, User user, int mark, int right_answers, List<int> users_answers)
        {
            InitializeComponent();
            dataBase = new DataBase();
            Topic = topic;
            Quiz = quiz;
            Users_answers = users_answers;
            questionsUser = user;
            Mark = mark;
            Right_answers = right_answers;
            _textBuilder = new StringBuilder();
            _rankBuilder = new StringBuilder();

            void AddToRank(string text)
            {
                _rankBuilder.AppendLine(text);
                Ranking.Text = _textBuilder.ToString();
            }
            List<User> users = dataBase.Read_UserFromDataBase(questionsUser.Status);
            users = users.OrderByDescending(x => x.Id).ToList();
            for (int i = 0; i < users.Count; i++)
            {
                if (i == 10)
                {
                    break;
                }
                AddToRank(users[i].Name + " " + users[i].Surname + users[i].Login);
            }


            AddToText("Вікторина була пройдена за темою: " + Topic + "\n");
            AddToText("Всього правильних відповідей: " + Right_answers + "/" + Quiz.Count + "\n");
            AddToText("Набрано балів: " + Mark + "\n");
            int question_index = 0;
            foreach (var question in Quiz)
            {
                AddToText(question.TextQuestion + "(Це " + question.DifficultyLevel + " питання)" + "\n");
                List<Answers> answers = dataBase.Read_AnswersFromDataBase(question.Id);
                AddToText("Варіанти відповідей:\n");
                int index = 1;
                foreach (var answer in answers)
                {
                    AddToText(index + ") " + answer.AnswerText + "\n");
                    index++;
                }
                AddToText("Правильна відповідь: " + answers[Quiz[question_index].RightAnswer - 1].AnswerText + "\n");
                AddToText("Ваша відповідь: " + answers[Users_answers[question_index] - 1].AnswerText + "\n");
                question_index++;
            }

        }
        private void AddToText(string text)
        {
            _textBuilder.AppendLine(text);
            Results.Text = _textBuilder.ToString();
        }
    }
}
