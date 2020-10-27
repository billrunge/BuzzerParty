namespace BuzzerPartyInterfaces
{
    public interface IQuestionStatus
    {
        int currentQuestion { get; set; }
        int questionCount { get; set; }
        int question { get; set; }
        bool answerable { get; set; }
        bool userBuzzed { get; set; }
    }
}