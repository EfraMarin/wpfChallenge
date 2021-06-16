namespace wpfChallenge.Interfaces
{
    public interface IBoardGamePlayer
    {
        bool SitNextTo(ILCRPlayerBase player, bool toLeft = true);

        ILCRPlayerBase PlayerToLeft { get; set; }

        ILCRPlayerBase PlayerToRight { get; set; }
    }
}
