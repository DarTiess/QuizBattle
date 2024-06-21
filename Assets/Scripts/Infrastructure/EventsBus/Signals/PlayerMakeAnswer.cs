namespace Infrastructure.Level.EventsBus.Signals
{
    public struct PlayerMakeAnswer
    {
        private readonly bool _isRight;
        public bool IsRight => _isRight;

        public PlayerMakeAnswer(bool isRight)
        {
            _isRight = isRight;
        }
    }
}