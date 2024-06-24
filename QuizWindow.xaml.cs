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
    /// Логика взаимодействия для QuizWindow.xaml
    /// </summary>
    public partial class QuizWindow : Window
    {
        string Topic;
        DataBase dataBase;
        int question_index = 0;
        User questionsUser;
        List<Questions> Quiz;
        List<Answers> answers;
        List<int> users_answers;
        int mark = 0;
        int right_answers = 0;
        DispatcherTimer _timer;
        DateTime _startTime;
        TimeSpan _totalTime;
        public QuizWindow(string topic, List<Questions> quiz, User user)
        {
            this.ResizeMode = ResizeMode.NoResize;
            InitializeComponent();
            users_answers = new List<int>();
            dataBase = new DataBase();
            Topic = topic;
            Quiz = quiz;
            questionsUser = user;
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += Timer_Tick;
            _totalTime = TimeSpan.Zero;
            _startTime = DateTime.Now;
            _timer.Start();
            Question.Text = Quiz[question_index].TextQuestion;
            answers = dataBase.Read_AnswersFromDataBase(Quiz[question_index].Id);
            Answer1_Button.Content = answers[0].AnswerText;
            Answer2_Button.Content = answers[1].AnswerText;
            Answer3_Button.Content = answers[2].AnswerText;
            Answer4_Button.Content = answers[3].AnswerText;
            Confirm_Button.IsEnabled = false;
        }
        void Timer_Tick(object sender, EventArgs e)
        {
            var elapsed = DateTime.Now - _startTime;
            TimerTextBlock.Text = elapsed.ToString(@"hh\:mm\:ss");
            _totalTime = elapsed;
        }
        private void Confirm_Button_Click(object sender, RoutedEventArgs e)
        {
            if (Answer1_Button.IsEnabled == false)
            {
                
                if (Quiz[question_index].RightAnswer == answers[0].NumberAnswer)
                {
                    if (Quiz[question_index].DifficultyLevel == "Складний")
                    {
                        mark += 3;
                        right_answers++;
                    }
                    else if (Quiz[question_index].DifficultyLevel == "Середній")
                    {
                        mark += 2;
                        right_answers++;
                    }
                    else
                    {
                        mark += 1;
                        right_answers++;
                    }
                }
                users_answers.Add(answers[0].NumberAnswer);
            }
            else if (Answer2_Button.IsEnabled == false)
            {
                if (Quiz[question_index].RightAnswer == answers[1].NumberAnswer)
                {
                    if (Quiz[question_index].DifficultyLevel == "Складний")
                    {
                        mark += 3;
                        right_answers++;
                    }
                    else if (Quiz[question_index].DifficultyLevel == "Середній")
                    {
                        mark += 2;
                        right_answers++;
                    }
                    else
                    {
                        mark += 1;
                        right_answers++;
                    }
                }
                users_answers.Add(answers[1].NumberAnswer);
            }
            else if (Answer3_Button.IsEnabled == false)
            {
                if (Quiz[question_index].RightAnswer == answers[2].NumberAnswer)
                {
                    if (Quiz[question_index].DifficultyLevel == "Складний")
                    {
                        mark += 3;
                        right_answers++;
                    }
                    else if (Quiz[question_index].DifficultyLevel == "Середній")
                    {
                        mark += 2;
                        right_answers++;
                    }
                    else
                    {
                        mark += 1;
                        right_answers++;
                    }
                }
                
                users_answers.Add(answers[2].NumberAnswer);
            }
            else if (Answer4_Button.IsEnabled == false)
            {
                if (Quiz[question_index].RightAnswer == answers[3].NumberAnswer)
                {
                    if (Quiz[question_index].DifficultyLevel == "Складний")
                    {
                        mark += 3;
                        right_answers++;
                    }
                    else if (Quiz[question_index].DifficultyLevel == "Середній")
                    {
                        mark += 2;
                        right_answers++;
                    }
                    else
                    {
                        mark += 1;
                        right_answers++;
                    }
                }
                
                users_answers.Add(answers[3].NumberAnswer);
            }
            if (question_index==2)
            {
            var averageTimePerQuestion = new TimeSpan(_totalTime.Ticks / Quiz.Count);
            
            QuizResultsWindow resultsWindow = new QuizResultsWindow(Topic, Quiz, questionsUser, mark, right_answers, users_answers, TimerTextBlock.Text, averageTimePerQuestion.ToString());
            resultsWindow.Show();
            this.Close();
            }
            else
            {
                question_index++;
                Question.Text = Quiz[question_index].TextQuestion;
                answers = dataBase.Read_AnswersFromDataBase(Quiz[question_index].Id);
                Answer1_Button.Content = answers[0].AnswerText;
                Answer2_Button.Content = answers[1].AnswerText;
                Answer3_Button.Content = answers[2].AnswerText;
                Answer4_Button.Content = answers[3].AnswerText;
                Confirm_Button.IsEnabled = false;
                Answer1_Button.IsEnabled = true;
                Answer2_Button.IsEnabled = true;
                Answer3_Button.IsEnabled = true;
                Answer4_Button.IsEnabled = true;
            }
        }

        private void Answer1_Button_Click(object sender, RoutedEventArgs e)
        {
            Answer1_Button.IsEnabled = false;
            Answer2_Button.IsEnabled = true;
            Answer3_Button.IsEnabled = true;
            Answer4_Button.IsEnabled = true;

            Confirm_Button.IsEnabled = true;
        }

        private void Answer2_Button_Click(object sender, RoutedEventArgs e)
        {
            Answer2_Button.IsEnabled = false;
            Answer1_Button.IsEnabled = true;
            Answer3_Button.IsEnabled = true;
            Answer4_Button.IsEnabled = true;
            Confirm_Button.IsEnabled = true;
        }

        private void Answer3_Button_Click(object sender, RoutedEventArgs e)
        {
            Answer3_Button.IsEnabled = false;
            Answer1_Button.IsEnabled = true;
            Answer2_Button.IsEnabled = true;
            Answer4_Button.IsEnabled = true;
            Confirm_Button.IsEnabled = true;
        }

        private void Answer4_Button_Click(object sender, RoutedEventArgs e)
        {
            Answer2_Button.IsEnabled = true;
            Answer3_Button.IsEnabled = true;
            Answer1_Button.IsEnabled = true;
            Answer4_Button.IsEnabled = false;
            Confirm_Button.IsEnabled = true;
        }
    }
}
