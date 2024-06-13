using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizProgram
{
    public class Answers
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public int NumberAnswer { get; set; }
        public string AnswerText { get; set; }
    }
}
