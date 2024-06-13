using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizProgram
{
    public class Questions
    {
        public int Id { get; set; }
        public string TextQuestion { get; set; }
        public string TopicName { get; set; }
        public string DifficultyLevel { get; set; }
        public int RightAnswer { get; set; }
    }
}
