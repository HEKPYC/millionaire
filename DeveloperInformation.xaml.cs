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
    /// Логика взаимодействия для DeveloperInformation.xaml
    /// </summary>
    public partial class DeveloperInformation : Window
    {
        string information;

        public DeveloperInformation()
        {
            InitializeComponent();

            information = "Розробка бази даних та методів взаємодії з інформацією – Бейник В. А. \n" +
                "Розробка алгоритмів обробки введеної користувачем інформації – Коляда М. І. \n" +
                "Розробка алгоритмів формування переліку питань, проходження вікторини та розрахунку результатів – Гаврилов Я. В. \n" +
                "Розробка графічного інтерфейсу програми – Крупницька Т. А. \n" +
                "Тестування та обробка помилок – Манжура А. К.";

            informationText.Text = information;
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        { 
            this.Close();
        }
    }
}
