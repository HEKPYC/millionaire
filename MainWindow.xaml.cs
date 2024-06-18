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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QuizProgram
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DataBase dataBase;
        User mainUser;
        public MainWindow(User user)
        {
            InitializeComponent();
            dataBase = new DataBase();
            mainUser = user;

            AdminLabel.Content = user.Name + " " + user.Surname;
            
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            dataBase.Add_QuestionToDataBase(questionText.Text, topicNameText.Text, difficultyLevelText.Text, int.Parse(rightAnswerText.Text));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            List<Questions> questions;

            questions = dataBase.Read_QuestionsFromDataBase("History");

            questionsList.ItemsSource = questions;
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            Questions questions = (Questions)questionsList.SelectedItem;

            dataBase.Delete_QuestionFromDataBase(questions.Id);
        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            Questions currentQuestion = (Questions)questionsList.SelectedItem;

            var updateQuestion = new Questions
            {
                Id = currentQuestion.Id,
                TextQuestion = questionText.Text,
                TopicName = topicNameText.Text,
                DifficultyLevel = difficultyLevelText.Text,
                RightAnswer = int.Parse(rightAnswerText.Text)
            };

            dataBase.Update_QuestionInDataBase(currentQuestion, updateQuestion);
        }

        private void AddAnswerButton_Click(object sender, RoutedEventArgs e)
        {
            Questions currentQuestion = (Questions)questionsList.SelectedItem;

            dataBase.Add_AnswerToDataBase(currentQuestion.Id, 1, answer1Text.Text);
            dataBase.Add_AnswerToDataBase(currentQuestion.Id, 2, answer2Text.Text);
            dataBase.Add_AnswerToDataBase(currentQuestion.Id, 3, answer3Text.Text);
            dataBase.Add_AnswerToDataBase(currentQuestion.Id, 4, answer4Text.Text);
        }

        private void AnswersButton_Click(object sender, RoutedEventArgs e)
        {
            List<Answers> answers;

            Questions currentQuestion = (Questions)questionsList.SelectedItem;

            answers = dataBase.Read_AnswersFromDataBase(currentQuestion.Id);

            answersList.ItemsSource = answers;
        }

        private void UpdateAnswerButton_Click(object sender, RoutedEventArgs e)
        {
            Answers currentAnswer = (Answers)answersList.SelectedItem;

            Update_AnswerCheck(currentAnswer, answer1Text);
            Update_AnswerCheck(currentAnswer, answer2Text);
            Update_AnswerCheck(currentAnswer, answer3Text);
            Update_AnswerCheck(currentAnswer, answer4Text);
        }

        private void Update_AnswerCheck(Answers currentAnswer, TextBox textBox)
        {
            if (!string.IsNullOrWhiteSpace(textBox.Text))
            {
                var updateAnswer = new Answers
                {
                    QuestionId = currentAnswer.QuestionId,
                    NumberAnswer = currentAnswer.NumberAnswer,
                    AnswerText = textBox.Text
                };

                dataBase.Update_AnswerInDataBase(currentAnswer, updateAnswer);
            }
        }

        private void InformationWindowButton_Click(object sender, RoutedEventArgs e)
        {
            InformationWindow informationWindow = new InformationWindow();
            informationWindow.Show();
        }

        private void RegistrationWindowButton_Click(object sender, RoutedEventArgs e)
        {
            RegistrationWindow registrationWindow = new RegistrationWindow();
            registrationWindow.Show();
        }

        private void ShowTopicsButton_Click(object sender, RoutedEventArgs e)
        {
            List<string> topics = dataBase.Read_TopicFromDataBase();
            topicsComboBox.ItemsSource = topics;
        }
    }
}
