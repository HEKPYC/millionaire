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
    /// Логика взаимодействия для RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        DataBase dataBase;
        public RegistrationWindow()
        {
            InitializeComponent();
            dataBase = new DataBase();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            dataBase.Add_UserToDataBase(SurnameText.Text, NameText.Text, LoginText.Text, PasswordText.Text, StatusText.Text);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            List<User> users;

            users = dataBase.Read_UserFromDataBase("Admin");

            usersList.ItemsSource = users;
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            User currentUser = (User)usersList.SelectedItem;

            var updateUser = new User
            {
                Surname = SurnameText.Text,
                Name = NameText.Text,
                Login = LoginText.Text,
                Password = PasswordText.Text,
                Status = StatusText.Text
            };

            dataBase.Update_UserInDataBase(currentUser, updateUser);
        }

        private void AddResultButton_Click(object sender, RoutedEventArgs e)
        {
            User currentUser = (User)usersList.SelectedItem;

            dataBase.Add_ResultScoreToDataBase(currentUser.Login, int.Parse(ResultScoreText.Text));
        }

        private void ShowButton_Click(object sender, RoutedEventArgs e)
        {
            List<ResultScore> resultScore;

            resultScore = dataBase.Read_ResultScoreFromDataBase();

            resultsList.ItemsSource = resultScore;
        }

        private void UpdateResultScoreButton_Click(object sender, RoutedEventArgs e)
        {
            ResultScore currentResultScore = (ResultScore)resultsList.SelectedItem;

            var updateResultScore = new ResultScore
            {
                Score = int.Parse(ResultScoreText.Text)
            };

            dataBase.Update_ResultScoreInDataBase(currentResultScore, updateResultScore);
        }
    }
}
