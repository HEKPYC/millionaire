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
            InitializeComponent();
            dataBase = new DataBase();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            dataBase.Add_UserToDataBase(SurnameText.Text, NameText.Text, LoginText.Text, PasswordText.Text, (string)statusComboBox.SelectedValue);

            User user = new User();

            List<User> users = dataBase.Read_UserFromDataBase((string)statusComboBox.SelectedValue);
            for (int i = 0; i < users.Count; i++)
            {
                if (users[i].Login == LoginText.Text && users[i].Password == PasswordText.Text)
                {
                    user = users[i];
                }
            }

            if ((string)statusComboBox.SelectedValue == "admin")
            {
                MainWindow mainWindow = new MainWindow(user);
                mainWindow.ShowDialog();
                Close();
            }
            else
            {

                QuestionsMainWindow questionsWindow = new QuestionsMainWindow(user);
                questionsWindow.ShowDialog();
                Close();
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
