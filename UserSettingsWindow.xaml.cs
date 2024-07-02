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
    /// Логика взаимодействия для UserSettingsWindow.xaml
    /// </summary>
    public partial class UserSettingsWindow : Window
    {
        DataBase dataBase;
        string topic;
        int maxScore;
        public User user;
        public UserSettingsWindow(User _user)
        {
            InitializeComponent();
            this.ResizeMode = ResizeMode.NoResize;
            dataBase = new DataBase();
            user = _user;

            NameText.Text = user.Name;
            SurnameText.Text = user.Surname;
            LoginText.Text = user.Login;
            PasswordText.Text = user.Password;
            StatusText.Text = user.Status;
            StatusText.IsEnabled = false;

            if (user.Status == "admin")
            {
                maxScoreLabel.Visibility = Visibility.Hidden;
                textLabel.Visibility = Visibility.Hidden;
                Subject.Visibility = Visibility.Hidden;
                scoreComboBox.Visibility = Visibility.Hidden;
            }

            List<string> topics = dataBase.Read_TopicFromDataBase();
            scoreComboBox.ItemsSource = topics;
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            if (NameText.Text.Length > 1 && SurnameText.Text.Length > 2 && LoginText.Text.Length > 4 && PasswordText.Text.Length > 7)
            {
                User tempUser = new User
                {
                    Surname = SurnameText.Text,
                    Name = NameText.Text,
                    Login = LoginText.Text,
                    Password = PasswordText.Text,
                    Status = StatusText.Text
                };
                dataBase.Update_UserInDataBase(user, tempUser);

                user = tempUser;
            }
            else
            {
                MessageBox.Show("Не всі поля заповнені");
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            topic = scoreComboBox.SelectedItem as string;
            List<ResultScore> results = dataBase.Read_ResultScoreFromDataBase(topic);
            ResultScore Score;
            bool check = false;
            foreach (ResultScore score in results)
            {
                if (user.Login.Equals(score.Username))
                {
                    maxScore = score.Score;
                    check = true;
                    break;
                }
            }
            if (check == false)
            {
                maxScoreLabel.Content = "0";
            }
            else
            {
                maxScoreLabel.Content = maxScore.ToString();
            }
            
        }
    }
}
