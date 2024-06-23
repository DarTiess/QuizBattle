public interface IQuizHandler
{
    void PlayerAnswer(bool isRight, int coins);
    void StartBot(int questIndex);
    void CompleteQuiz();
}