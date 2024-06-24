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
    /// Логика взаимодействия для AddChangeQuestionWindow.xaml
    /// </summary>
    public partial class AddChangeQuestionWindow : Window
    {
        string action;
        Questions questions;
        DataBase dataBase;
        public AddChangeQuestionWindow(string _action, Questions _questions = null)
        {
            InitializeComponent();
            this.ResizeMode = ResizeMode.NoResize;
            action = _action;
            questions = _questions;
            dataBase = new DataBase();

            if (action == "change")
            {
                questionText.Text = questions.TextQuestion;
                topicNameText.Text = questions.TopicName;
                
                for (int i = 0; i < lvlComboBox.Items.Count; i++)
                {
                    if((string)lvlComboBox.Items[i] == questions.DifficultyLevel)
                    {
                        lvlComboBox.SelectedIndex = i;
                        break;
                    }
                }

                //difficultyLevelText.Text = questions.DifficultyLevel;
                answerComboBox.SelectedIndex = questions.RightAnswer - 1;

                List<Answers> answers = dataBase.Read_AnswersFromDataBase(questions.Id);

                answer1Text.Text = answers[0].AnswerText;
                answer2Text.Text = answers[1].AnswerText;
                answer3Text.Text = answers[2].AnswerText;
                answer4Text.Text = answers[3].AnswerText;

            }
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            bool CheckForRepeatQuestionName = false;
            List<Questions> quest = dataBase.Read_QuestionsFromDataBase(topicNameText.Text);
            if (questionText.Text.Length == 0 || topicNameText.Text.Length == 0 || lvlComboBox.SelectedIndex == -1 || answerComboBox.SelectedIndex == -1 || answer1Text.Text.Length == 0 || answer2Text.Text.Length == 0 || answer3Text.Text.Length == 0 || answer4Text.Text.Length == 0)
            {
                MessageBox.Show("Не всі поля заповнені");
            }
            else
            {
                for (int i = 0; i < quest.Count; i++)
                {
                    if (quest[i].TextQuestion == questionText.Text) { 
                    
                        CheckForRepeatQuestionName=true;
                        
                    }
                    break;
                }
                if (CheckForRepeatQuestionName == false)
                {

                    if (action == "add")
                    {
                        dataBase.Add_QuestionToDataBase(questionText.Text, topicNameText.Text, (string)lvlComboBox.SelectedValue, int.Parse((string)answerComboBox.SelectedValue));

                        List<Questions> tempQuestions = dataBase.Read_QuestionsFromDataBase(topicNameText.Text);
                        for (int i = 0; i < tempQuestions.Count; i++)
                        {
                            if (tempQuestions[i].TextQuestion == questionText.Text && tempQuestions[i].TopicName == topicNameText.Text && tempQuestions[i].DifficultyLevel == (string)lvlComboBox.SelectedValue && tempQuestions[i].RightAnswer == int.Parse((string)answerComboBox.SelectedValue))
                            {
                                dataBase.Add_AnswerToDataBase(tempQuestions[i].Id, 1, answer1Text.Text);
                                dataBase.Add_AnswerToDataBase(tempQuestions[i].Id, 2, answer2Text.Text);
                                dataBase.Add_AnswerToDataBase(tempQuestions[i].Id, 3, answer3Text.Text);
                                dataBase.Add_AnswerToDataBase(tempQuestions[i].Id, 4, answer4Text.Text);
                                break;
                            }
                        }
                        questionText.Text = "";
                        topicNameText.Text = "";

                        lvlComboBox.SelectedIndex = -1;
                        answerComboBox.SelectedIndex = -1;

                        answer1Text.Text = "";
                        answer2Text.Text = "";
                        answer3Text.Text = "";
                        answer4Text.Text = "";
                    }
                    else
                    {
                        var updateQuestion = new Questions
                        {
                            Id = questions.Id,
                            TextQuestion = questionText.Text,
                            TopicName = topicNameText.Text,
                            DifficultyLevel = (string)lvlComboBox.SelectedValue,//difficultyLevelText.Text,
                            RightAnswer = int.Parse((string)answerComboBox.SelectedValue)
                        };

                        dataBase.Update_QuestionInDataBase(questions, updateQuestion);

                        List<Answers> answers = dataBase.Read_AnswersFromDataBase(questions.Id);

                        Update_Answer(answers[0], answer1Text.Text);
                        Update_Answer(answers[1], answer2Text.Text);
                        Update_Answer(answers[2], answer3Text.Text);
                        Update_Answer(answers[3], answer4Text.Text);
                        CheckForRepeatQuestionName = false;
                        this.Close();

                        //Update_AnswerCheck(questions, answer1Text);
                        //Update_AnswerCheck(questions, answer2Text);
                        //Update_AnswerCheck(questions, answer3Text);
                        //Update_AnswerCheck(questions, answer4Text);


                    }
                }
                else
                {
                    MessageBox.Show("Питання з такою назвою вже є!");
                }
                    
                }


            }

           
        
        private void Update_Answer(Answers answer, string text)
        {
            dataBase.Update_AnswerInDataBase(answer, new Answers
            {
                QuestionId = answer.QuestionId,
                NumberAnswer = answer.NumberAnswer,
                AnswerText = text
            });
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            
        }

        //private void Update_AnswerCheck(Answers currentAnswer, TextBox textBox)
        //{
        //    if (!string.IsNullOrWhiteSpace(textBox.Text))
        //    {
        //        var updateAnswer = new Answers
        //        {
        //            QuestionId = currentAnswer.QuestionId,
        //            NumberAnswer = currentAnswer.NumberAnswer,
        //            AnswerText = textBox.Text
        //        };

        //        dataBase.Update_AnswerInDataBase(currentAnswer, updateAnswer);
        //    }
        //}
    }
}
