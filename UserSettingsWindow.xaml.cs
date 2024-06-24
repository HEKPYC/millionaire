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

            List<ResultScore> results = dataBase.Read_ResultScoreFromDataBase();
            for (int i = 0; i < results.Count; i++)
            {
                if(user.Login == results[i].Username)
                {
                    maxScoreLabel.Content = results[i].Score;
                }
                else if (i == results.Count - 1)
                {
                    maxScoreLabel.Content = "0";
                }
            }

            if (user.Status == "admin")
            {
                maxScoreLabel.Visibility = Visibility.Hidden;
                textLabel.Visibility = Visibility.Hidden;
            }
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
    }
}
