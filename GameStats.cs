public class GameStats
{
    public int GamesPlayed { get; set; } = 0;
    public int TotalHits { get; set; } = 0;
    public bool AllShipsDestroyed { get; set; } = false;
    public int MaxLoseStreak { get; set; } = 0;
    public double Accuracy { get; set; } = 0.0;

    private int currentLoseStreak = 0;

    public void RegisterGame(bool won, int hits, int attempts)
    {
        GamesPlayed++;
        TotalHits += hits;
        Accuracy = attempts > 0 ? (double)TotalHits / attempts : 0.0;
        AllShipsDestroyed = won;
        
        currentLoseStreak = won ? 0 : currentLoseStreak + 1;
        if (currentLoseStreak > MaxLoseStreak)
            MaxLoseStreak = currentLoseStreak;
    }
}
