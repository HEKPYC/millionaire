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
    /// Логика взаимодействия для RegistrationMainWindow.xaml
    /// </summary>
    public partial class RegistrationMainWindow : Window
    {
        DataBase dataBase;
        public RegistrationMainWindow()
        {
            this.ResizeMode = ResizeMode.NoResize;
            InitializeComponent();
            dataBase = new DataBase();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (SurnameText.Text.Length > 1 && NameText.Text.Length > 2 && LoginText.Text.Length > 4 && PasswordText.Text.Length > 7)
            {
                dataBase.Add_UserToDataBase(SurnameText.Text, NameText.Text, LoginText.Text, PasswordText.Text, (string)statusComboBox.SelectedValue);
           
            User user = new User
            {
                Surname = SurnameText.Text,
                Name = NameText.Text,
                Login = LoginText.Text,
                Password = PasswordText.Text,
                Status = (string)statusComboBox.SelectedValue
            };
            /*List<User> users = dataBase.Read_UserFromDataBase((string)statusComboBox.SelectedValue);
            for (int i = 0; i < users.Count; i++)
            {
                if (users[i].Login == LoginText.Text && users[i].Password == PasswordText.Text)
                {
                    user = users[i];
                }
            }*/
           
                if ((string)statusComboBox.SelectedValue == "admin")
                {
                    ChangeQuestionsWindow changeQuestionsWindow = new ChangeQuestionsWindow(user);
                    changeQuestionsWindow.Show();
                    this.Close();
                }
                else
                {

                    QuestionsMainWindow questionsWindow = new QuestionsMainWindow(user);
                    questionsWindow.Show();
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Не всі поля заповнені");
            }
            
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            EntranceWindow en = new EntranceWindow();
            en.Show();
            this.Close();
        }
    }
}
