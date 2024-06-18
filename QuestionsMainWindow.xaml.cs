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
    /// Логика взаимодействия для QuestionsMainWindow.xaml
    /// </summary>
    public partial class QuestionsMainWindow : Window
    {
        DataBase dataBase;
        User questionsUser;
        public QuestionsMainWindow(User user)
        {
            InitializeComponent();
            questionsUser = user;

            userLabel.Content = user.Name + " " + user.Surname;
        }
    }
}
