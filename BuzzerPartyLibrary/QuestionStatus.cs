using BuzzerPartyInterfaces;

namespace BuzzerPartyLibrary
{
    public class QuestionStatus : IQuestionStatus
    {
        public int currentQuestion { get; set; }
        public int questionCount { get; set; }
        public int question { get; set; }
        public bool answerable { get; set; }
        public bool userBuzzed { get; set; }
    }
}