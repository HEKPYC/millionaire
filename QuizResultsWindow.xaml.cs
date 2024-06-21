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
using System.Windows.Threading;

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
        string Time_Spent;
        string Avarage_Time;
        ResultScore Score;
        public QuizResultsWindow(string topic, List<Questions> quiz, User user, int mark, int right_answers, List<int> users_answers, string timerValue, string avarageTime)
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
            Time_Spent = timerValue;
            Avarage_Time = avarageTime;
            List<ResultScore> results = dataBase.Read_ResultScoreFromDataBase();

            bool check = false;

            foreach (ResultScore score in results)
            {
                if (questionsUser.Login.Equals(score.Username))
                {
                    Score = score;
                    check = true;
                }
            }

            if (check == false)
            {
                dataBase.Add_ResultScoreToDataBase(questionsUser.Login, mark);
            }
            else
            {
                dataBase.Update_ResultScoreInDataBase(Score, new ResultScore { Username = questionsUser.Login, Score = mark });
            }

            void AddToRank(string text)
            {
                _rankBuilder.AppendLine(text);
                Ranking.Text = _rankBuilder.ToString();
            }
            List<ResultScore> users_results = dataBase.Read_ResultScoreFromDataBase();

            users_results = users_results.OrderByDescending(x => x.Score).ToList();
            for (int i = 0; i < users_results.Count; i++)
            {
                if (i == 10)
                {
                    break;
                }

                AddToRank(users_results[i].Username + " " + users_results[i].Score + "\n");
            }


            AddToText("Вікторина була пройдена за темою: " + Topic + "\n");
            AddToText("Всього затрачено часу: " + Time_Spent + "\n");
            AddToText("В середньому на кожне питання затрачено часу: " + Avarage_Time + "\n");
            AddToText("Всього правильних відповідей: " + Right_answers + "/" + Quiz.Count + "\n");
            AddToText("Набрано балів: " + Mark + "\n");
            int question_index = 0;
            foreach (var question in Quiz)
            {
                AddToText(question.TextQuestion + "(Рівень складності цього питання: " + question.DifficultyLevel + " )" + "\n");
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

        private void Restart_Quiz_Click(object sender, RoutedEventArgs e)
        {
            QuestionsMainWindow questionsWindow = new QuestionsMainWindow(questionsUser);
            questionsWindow.Show();
            this.Close();
        }
    }
}
