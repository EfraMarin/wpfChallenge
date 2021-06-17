namespace wpfChallenge.Interfaces
{
    public interface IBoardGamePlayer
    {
        /// <summary>
        /// Simulates a player sitting next to the specidied player
        /// </summary>
        /// <param name="player">An object of a class that implements <see cref="ILCRPlayerBase"/></param>
        /// <param name="toLeft">The direction the Player is going to sit</param>
        /// <returns>True if the place is not occupied, otherwise, returns false</returns>
        bool SitNextTo(ILCRPlayerBase player, bool toLeft = true);

        ILCRPlayerBase PlayerToLeft { get; set; }

        ILCRPlayerBase PlayerToRight { get; set; }
    }
}
