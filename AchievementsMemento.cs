using System;
using System.Collections.Generic;

public class AchievementsMemento
{
    public Dictionary<AchievementId, bool> Unlocked { get; set; } = new();
    public int GamesPlayed { get; set; }
    public int TotalHits { get; set; }
    public int MaxLoseStreak { get; set; }
}
