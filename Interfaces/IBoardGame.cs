namespace wpfChallenge.Interfaces
{
    public interface IBoardGame
    {
        ILCRPlayerBase Winner { get; }

        void ProcessNextTurn();
    }
}
