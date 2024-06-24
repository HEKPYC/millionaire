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
            questionsListBox.ItemsSource = dataBase.Read_QuestionsFromDataBase((string)topicsComboBox.SelectedValue);
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            AddChangeQuestionWindow changeWindow = new AddChangeQuestionWindow("add");
            changeWindow.ShowDialog();


            questionsListBox.ItemsSource = dataBase.Read_QuestionsFromDataBase((string)topicsComboBox.SelectedValue);

            List<string> topics = dataBase.Read_TopicFromDataBase();
            topicsComboBox.ItemsSource = topics;


        }

        private void changeButton_Click(object sender, RoutedEventArgs e)
        {
            if (questionsListBox.SelectedIndex != -1)
            {
                AddChangeQuestionWindow changeWindow = new AddChangeQuestionWindow("change", (Questions)questionsListBox.SelectedItem);
                changeWindow.ShowDialog();
                questionsListBox.ItemsSource = dataBase.Read_QuestionsFromDataBase((string)topicsComboBox.SelectedValue);
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
