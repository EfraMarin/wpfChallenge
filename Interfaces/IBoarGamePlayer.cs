namespace wpfChallenge.Interfaces
{
    public interface IBoarGamePlayer
    {
        bool SitNextTo(IBoarGamePlayer player, bool toLeft = true);

        IBoarGamePlayer PlayerToLeft { get; set; }

        IBoarGamePlayer PlayerToRight { get; set; }
    }
}
