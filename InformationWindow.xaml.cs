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
    /// Логика взаимодействия для InformationWindow.xaml
    /// </summary>
    public partial class InformationWindow : Window
    {
        DataBase dataBase;
        public InformationWindow()
        {
            InitializeComponent();
            dataBase = new DataBase();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            List<Questions> questions;

            questions = dataBase.Read_QuestionsFromDataBase("History");

            questionsList.ItemsSource = questions;
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            var question = (Questions)questionsList.SelectedItem;
            dataBase.Add_InformationToDataBase(question.Id, InformationText.Text);
        }

        private void ShowButton_Click(object sender, RoutedEventArgs e)
        {
            List<Information> information;

            var question = (Questions)questionsList.SelectedItem;

            information = dataBase.Read_InformationFromDataBase(question.Id);

            informationList.ItemsSource = information;
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var information = (Information)informationList.SelectedItem;

            dataBase.Delete_InformationFromDataBase(information.Id);
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            Information currentInformation = (Information)informationList.SelectedItem;

            var updateInformation = new Information
            {
                Id = currentInformation.Id,
                QuestionId = currentInformation.QuestionId,
                InformationQuestion = InformationText.Text
            };

            dataBase.Update_InformationInDataBase(currentInformation, updateInformation);
        }
    }
}
