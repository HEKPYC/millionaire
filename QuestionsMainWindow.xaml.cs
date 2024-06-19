using System;
using System.Collections;
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
    /// Логика взаимодействия для QuestionsMainWindow.xaml
    /// </summary>
    public partial class QuestionsMainWindow : Window
    {
        DataBase dataBase;
        User questionsUser;
        string topic;
        public QuestionsMainWindow(User user)
        {
            InitializeComponent();
            questionsUser = user;
            userLabel.Content = user.Name + " " + user.Surname;
        }

        private void Start_Quiz_Button_Click(object sender, RoutedEventArgs e)
        {
            List<Questions> Quiz = dataBase.Read_QuestionsFromDataBase(topic);
            int hard = 0, medium = 0, easy = 0;
            foreach (var question in Quiz)
            {
                switch (question.DifficultyLevel)
                {
                    case "Складний":
                        hard++;
                        break;
                    case "Середній":
                        medium++;
                        break;
                    case "Легкий":
                        easy++;
                        break;
                    default:
                        break;
                }
            }
            if(hard>=3 && medium>=5 && easy>=7 && Quiz.Count >= 15)
            {
                Random rand = new Random();
                while (Quiz.Count > 15)
                {
                    int randomIndex = rand.Next(Quiz.Count);
                    if (Quiz[randomIndex].DifficultyLevel == "Складний" && hard == 3)
                    {
                        continue;
                    }
                    else if (Quiz[randomIndex].DifficultyLevel == "Середній" && medium == 5)
                    {
                        continue;
                    }
                    else if (Quiz[randomIndex].DifficultyLevel == "Легкий" && easy == 7)
                    {
                        continue;
                    }
                    else
                    {
                        if (Quiz[randomIndex].DifficultyLevel == "Складний")
                            hard--;
                        else if (Quiz[randomIndex].DifficultyLevel == "Середній")
                            medium--;
                        else if (Quiz[randomIndex].DifficultyLevel == "Легкий")
                            easy--;
                        Quiz.RemoveAt(randomIndex);
                    }
                }
                QuizWindow quizWindow = new QuizWindow(topic, Quiz, questionsUser);
                quizWindow.Show();
                Close();
            }
            else
            {
                Start_Quiz_Button.Content = "Неможливо запустити вікторину за цією тематикою(замало питань).";
                Start_Quiz_Button.IsEnabled = false;
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<string> topics = dataBase.Read_TopicFromDataBase();
            ComboBox.Items.Clear();
            foreach (string item in topics)
            {
                ComboBox.Items.Add(item);
            }
            topic = ComboBox.SelectedItem as string;
            Start_Quiz_Button.IsEnabled = true;
        }

    }
}
