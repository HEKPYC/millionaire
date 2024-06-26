using System;
using System.Collections.Generic;
using System.Data.Entity;
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
    /// Логика взаимодействия для ChangeQuestionsWindow.xaml
    /// </summary>
    public partial class ChangeQuestionsWindow : Window
    {
        DataBase dataBase;
        User user;
        public ChangeQuestionsWindow(User user)
        {
            InitializeComponent();
            this.ResizeMode = ResizeMode.NoResize;
            dataBase = new DataBase();

            List<string> topics = dataBase.Read_TopicFromDataBase();
            topicsComboBox.ItemsSource = topics;
            this.user = user;
        }


        private void topicsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<Questions> questions;

            questions = dataBase.Read_QuestionsFromDataBase((string)topicsComboBox.SelectedValue);

            checkQuestions.Content = "";

            var hardQuestion = questions.Where(q => q.DifficultyLevel == "Складний");
            var middleQuestion = questions.Where(q => q.DifficultyLevel == "Середній");
            var easyQuestion = questions.Where(q => q.DifficultyLevel == "Легкий");

            if (questions.Count < 15 && (easyQuestion.Count() != 7 || middleQuestion.Count() != 5 || hardQuestion.Count() != 3))
            {
                checkQuestions.Content = $"Залишилось додати питань: легких: {7 - easyQuestion.Count()} сердніх: {5 - middleQuestion.Count()} складних: {3 - hardQuestion.Count()}";

            }
            questionsListBox.ItemsSource = questions;
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            AddChangeQuestionWindow changeWindow = new AddChangeQuestionWindow(user, "add");
            changeWindow.Show();
            this.Close();

            questionsListBox.ItemsSource = dataBase.Read_QuestionsFromDataBase((string)topicsComboBox.SelectedValue);

            List<string> topics = dataBase.Read_TopicFromDataBase();
            topicsComboBox.ItemsSource = topics;


        }

        private void changeButton_Click(object sender, RoutedEventArgs e)
        {
            if (questionsListBox.SelectedIndex != -1)
            {
                AddChangeQuestionWindow changeWindow = new AddChangeQuestionWindow(user, "change", (Questions)questionsListBox.SelectedItem);
                changeWindow.Show();
                questionsListBox.ItemsSource = dataBase.Read_QuestionsFromDataBase((string)topicsComboBox.SelectedValue);
                this.Close();
            }
            else
            {
                MessageBox.Show("Питання не було виділено");
            }
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (questionsListBox.SelectedIndex != -1)
            {
                dataBase.Delete_QuestionFromDataBase(((Questions)questionsListBox.SelectedItem).Id);
                questionsListBox.ItemsSource = dataBase.Read_QuestionsFromDataBase((string)topicsComboBox.SelectedValue);

                List<string> topics = dataBase.Read_TopicFromDataBase();
                topicsComboBox.ItemsSource = topics;
            }
            else
            {
                MessageBox.Show("Питання не було виділено");
            }
        }

        private void settingsButton_Click(object sender, RoutedEventArgs e)
        {
            UserSettingsWindow userSWindow = new UserSettingsWindow(user);
            userSWindow.ShowDialog();
            user = userSWindow.user;
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            EntranceWindow en = new EntranceWindow();
            en.Show();
            this.Close();
        }
    }
}
