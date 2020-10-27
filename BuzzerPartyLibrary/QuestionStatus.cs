using BuzzerPartyInterfaces;

namespace BuzzerPartyLibrary
{
    public class QuestionStatus : IQuestionStatus
    {
        public int CurrentQuestion { get; set; }
        public int QuestionCount { get; set; }
        public int Question { get; set; }
        public bool Answerable { get; set; }
        public bool UserBuzzed { get; set; }
    }
}