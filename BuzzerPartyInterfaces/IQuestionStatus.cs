namespace BuzzerPartyInterfaces
{
    public interface IQuestionStatus
    {
        int CurrentQuestion { get; set; }
        int QuestionCount { get; set; }
        int Question { get; set; }
        bool Answerable { get; set; }
        bool UserBuzzed { get; set; }
    }
}