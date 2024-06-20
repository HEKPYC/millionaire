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
    /// Логика взаимодействия для EntranceWindow.xaml
    /// </summary>
    public partial class EntranceWindow : Window
    {
        DataBase dataBase;
        public EntranceWindow()
        {
            InitializeComponent();
            dataBase = new DataBase();
        }


        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            List<User> users = dataBase.Read_UserFromDataBase((string)statusComboBox.SelectedValue);

            for(int i = 0; i < users.Count; i++)
            {
                if (users[i].Login == LoginText.Text && users[i].Password == PasswordText.Text)
                {
                    if ((string)statusComboBox.SelectedValue == "admin")
                    {
                        //MainWindow mainWindow = new MainWindow(users[i]);
                        //mainWindow.Show();
                        ChangeQuestionsWindow changeQWindow = new ChangeQuestionsWindow(users[i]);
                        changeQWindow.ShowDialog();
                        return;
                    }
                    else
                    {
                        QuestionsMainWindow questionsWindow = new QuestionsMainWindow(users[i]);
                        questionsWindow.ShowDialog();
                        return;
                    }
                }
            }

            MessageBox.Show("Incorrect password");
            //string str = "";

            //for(int i = 0; i < users.Count; i++)
            //{
            //    str += users[i].Name + "\n";
            //}

            //MessageBox.Show(str);
        }

        private void RegistrationButton_Click(object sender, RoutedEventArgs e)
        {
            RegistrationMainWindow registrationWindow = new RegistrationMainWindow();
            registrationWindow.ShowDialog();
        }
    }
}
