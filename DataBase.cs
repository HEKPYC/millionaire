using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace QuizProgram
{
    public class DataBase
    {
        public void Add_QuestionToDataBase(string textQuestion, string topicName, string difficultyLevel, int rightAnswer)
        {
            using (var context = new DataBaseContext())
            {
                try
                {
                    var question = new Questions
                    {
                        TextQuestion = textQuestion,
                        TopicName = topicName,
                        DifficultyLevel = difficultyLevel,
                        RightAnswer = rightAnswer
                    };

                    context.Questions.Add(question);
                    context.SaveChanges();
                    MessageBox.Show("Question was added");
                }
                catch(Exception e)
                {
                    MessageBox.Show(e.ToString());
                }
            }
        }

        public List<Questions> Read_QuestionsFromDataBase(string topic)
        {
            List<Questions> questions;

            using (var context = new DataBaseContext())
            {
                questions = context.Questions.Where(q => q.TopicName.Equals(topic)).ToList();
            }

            return questions;
        }

        public void Delete_QuestionFromDataBase(int questionId)
        {
            using (var context = new DataBaseContext())
            {
                try
                {
                    var questionDelete = context.Questions.FirstOrDefault(q => q.Id == questionId);

                    if (questionDelete != null)
                    {
                        Delete_AnswersFromDataBase(questionId);
                        Delete_InformationFromDataBase(questionDelete);
                        context.Questions.Remove(questionDelete);
                        context.SaveChanges();

                        MessageBox.Show("Question was deleted");
                    }
                    else
                    {
                        MessageBox.Show("Question was not found");
                    }
                }
                catch(Exception e)
                {
                    MessageBox.Show(e.ToString());
                }
            }
        }

        public void Update_QuestionInDataBase(Questions questionCurrent, Questions questionUpdate)
        {
            using (var context = new DataBaseContext())
            {
                try
                {
                    var question = context.Questions.FirstOrDefault(q => q.Id == questionCurrent.Id);
                    if (question != null)
                    {
                        if (!string.IsNullOrWhiteSpace(questionUpdate.TextQuestion))
                        {
                            question.TextQuestion = questionUpdate.TextQuestion;
                        }
                        if (!string.IsNullOrWhiteSpace(questionUpdate.TopicName))
                        {
                            question.TopicName = questionUpdate.TopicName;
                        }
                        if (!string.IsNullOrWhiteSpace(questionUpdate.DifficultyLevel))
                        {
                            question.DifficultyLevel = questionUpdate.DifficultyLevel;
                        }
                        if (questionUpdate.RightAnswer > 0)
                        {
                            question.RightAnswer = questionUpdate.RightAnswer;
                        }
                    }

                    context.SaveChanges();
                    MessageBox.Show("Question was updated");
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
                }
            }
        }

        public void Add_AnswerToDataBase(int questionId, int numberAnswer, string answerText)
        {
            using (var context = new DataBaseContext())
            {
                try
                {
                    var answer = new Answers
                    {
                        QuestionId = questionId,
                        NumberAnswer = numberAnswer,
                        AnswerText = answerText
                    };

                    context.Answers.Add(answer);
                    context.SaveChanges();
                    MessageBox.Show("Answer was added");
                }
                catch (Exception e) 
                { 
                    MessageBox.Show(e.ToString());
                }
            }
        }

        public List<Answers> Read_AnswersFromDataBase(int questionId)
        {
            List<Answers> answers;

            using (var context = new DataBaseContext())
            {
                answers = context.Answers.Where(q => q.QuestionId == questionId).ToList();
            }

            return answers;
        }

        public void Delete_AnswersFromDataBase(int questionId)
        {
            using (var context = new DataBaseContext())
            {
                try
                {
                    var ansversDelete = context.Answers.Where(q => q.QuestionId == questionId);

                    if (ansversDelete != null)
                    {
                        context.Answers.RemoveRange(ansversDelete);
                        context.SaveChanges();
                        MessageBox.Show("Answers was deleted");
                    }
                    else
                    {
                        MessageBox.Show("Answers was not deleted");
                    }
                }
                catch (Exception e) 
                {
                    MessageBox.Show(e.ToString());
                }
            }
        }

        public void Update_AnswerInDataBase(Answers currentAnswer, Answers updateUnswer)
        {
            using (var context = new DataBaseContext())
            {
                try
                {
                    var answer = context.Answers.FirstOrDefault(q => q.QuestionId == currentAnswer.QuestionId && q.NumberAnswer == currentAnswer.NumberAnswer);

                    if (answer != null)
                    {
                        if (!string.IsNullOrWhiteSpace(updateUnswer.AnswerText))
                        {
                            answer.AnswerText = updateUnswer.AnswerText;
                        }
                    }

                    context.SaveChanges();
                    MessageBox.Show("Answer was updated");
                }
                catch(Exception e)
                {
                    MessageBox.Show(e.ToString());
                }
            }
        }

        public void Add_InformationToDataBase(int questionId, string informationQuestion)
        {
            try
            {
                using (var context = new DataBaseContext())
                {
                    var information = new Information
                    {
                        QuestionId = questionId,
                        InformationQuestion = informationQuestion
                    };

                    context.Information.Add(information);
                    context.SaveChanges();
                    MessageBox.Show("Information was added");
                }
            }
            catch (Exception e) 
            { 
                MessageBox.Show(e.ToString());
            }
        }

        public List<Information> Read_InformationFromDataBase(int questionId)
        {
            List<Information> information;

            using (var context = new DataBaseContext())
            {
                information = context.Information.Where(q => q.QuestionId == questionId).ToList();
            }

            return information;
        }

        public void Delete_InformationFromDataBase(int informationId)
        {
            using (var context = new DataBaseContext())
            {
                try
                {
                    var informationDelete = context.Information.FirstOrDefault(q => q.Id == informationId);

                    if (informationDelete != null)
                    {
                        context.Information.Remove(informationDelete);
                        context.SaveChanges();
                        MessageBox.Show("Information was delete");
                    }
                    else
                    {
                        MessageBox.Show("Information was not found");
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
                }
            }
        }

        public void Delete_InformationFromDataBase(Questions question)
        {
            using (var context = new DataBaseContext())
            {
                try
                {
                    var information = context.Information.Where(q => q.QuestionId == question.Id).ToList();

                    if (information != null)
                    {
                        context.Information.RemoveRange(information);
                        context.SaveChanges();
                        MessageBox.Show("Information was delete");
                    }
                    else
                    {
                        MessageBox.Show("Information was not found");
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
                }
            }
        }

        public void Update_InformationInDataBase(Information currentInformation, Information updateInformation)
        {
            using (var context = new DataBaseContext())
            {
                try
                {
                    var information = context.Information.FirstOrDefault(q => q.Id == currentInformation.Id);

                    if (information != null)
                    {
                        if (!string.IsNullOrWhiteSpace(updateInformation.InformationQuestion))
                        {
                            information.InformationQuestion = updateInformation.InformationQuestion;
                        }
                    }

                    context.SaveChanges();
                    MessageBox.Show("Information was update");
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
                }
            }
        }

        public List<string> Read_TopicFromDataBase()
        {
            List<string> topics;

            using (var context = new DataBaseContext())
            {
                topics = context.Questions.Select(q => q.TopicName).Distinct().ToList();
            }

            return topics;
        }

        public void Add_UserToDataBase(string surname, string name, string login, string password, string status)
        {
            try
            {
                using (var context = new DataBaseContext())
                {
                    var user = new User
                    {
                        Surname = surname,
                        Name = name,
                        Login = login,
                        Password = password,
                        Status = status
                    };

                    context.User.Add(user);
                    context.SaveChanges();
                    MessageBox.Show("User was added");
                }
            }
            catch (Exception e) 
            { 
                MessageBox.Show(e.ToString());
            }
        }

        public List<User> Read_UserFromDataBase(string status)
        {
            List<User> users;

            using (var context = new DataBaseContext())
            {
                users = context.User.Where(q => q.Status == status).ToList();
            }

            return users;
        }

        public void Update_UserInDataBase(User currentUser, User updateUser)
        {
            using (var context = new DataBaseContext())
            {
                try
                {
                    var user = context.User.FirstOrDefault(q => q.Id == currentUser.Id);

                    if (user != null)
                    {
                        if (!string.IsNullOrWhiteSpace(updateUser.Surname))
                        {
                            user.Surname = updateUser.Surname;
                        }
                        if (!string.IsNullOrWhiteSpace(updateUser.Name))
                        {
                            user.Name = updateUser.Name;
                        }
                        if (!string.IsNullOrWhiteSpace(updateUser.Login))
                        {
                            user.Login = updateUser.Login;
                        }
                        if (!string.IsNullOrWhiteSpace(updateUser.Password))
                        {
                            user.Password = updateUser.Password;
                        }
                        if (!string.IsNullOrWhiteSpace(updateUser.Status))
                        {
                            user.Status = updateUser.Status;
                        }
                    }

                    context.SaveChanges();
                    MessageBox.Show("User was updated");
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
                }
            }
        }

        public void Add_ResultScoreToDataBase(string username, int score)
        {
            using(var context = new DataBaseContext())
            {
                try
                {
                    var resultScore = new ResultScore
                    {
                        Username = username,
                        Score = score
                    };

                    context.Results.Add(resultScore);
                    context.SaveChanges();
                    MessageBox.Show("Result score was added");
                }
                catch(Exception e)
                {
                    MessageBox.Show(e.ToString());
                }
            }
        }

        public List<ResultScore> Read_ResultScoreFromDataBase()
        {
            List<ResultScore> resultScore;

            using (var context = new DataBaseContext())
            {
                resultScore = context.Results.ToList();
            }

            return resultScore;
        }

        public void Update_ResultScoreInDataBase(ResultScore currentResultScore, ResultScore updateResultScore)
        {
            using (var context = new DataBaseContext())
            {
                try
                {
                    var resultScore = context.Results.FirstOrDefault(q => q.Username == currentResultScore.Username);

                    if (resultScore != null)
                    {
                        if (updateResultScore.Score > currentResultScore.Score)
                        {
                            resultScore.Score = updateResultScore.Score;
                        }
                    }

                    context.SaveChanges();
                    MessageBox.Show("Result score was updated");
                }
                catch(Exception e)
                {
                    MessageBox.Show(e.ToString());
                }
            }
        }
    }
}
