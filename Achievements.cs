using System;
using System.Collections.Generic;

public enum AchievementId
{
    FirstBlood,
    Sharpshooter,
    Destructor,
    Unlucky,
    Veteran,
    PerfectGame
}

public class Achievement
{
    public AchievementId Id { get; }
    public string Name { get; }
    public string Description { get; }
    public bool Unlocked { get; private set; }
    public DateTime? UnlockedAt { get; private set; }

    public Achievement(AchievementId id, string name, string desc)
    {
        Id = id;
        Name = name;
        Description = desc;
        Unlocked = false;
    }

    public void Unlock()
    {
        if (!Unlocked)
        {
            Unlocked = true;
            UnlockedAt = DateTime.Now;
            Console.WriteLine($"Achievement unlocked: {Name}");
        }
    }
}

public class AchievementManager
{
    public Dictionary<AchievementId, Achievement> Achievements { get; } = new();

    public AchievementManager()
    {
        Achievements.Add(AchievementId.FirstBlood, new Achievement(AchievementId.FirstBlood, "Pierwsza krew", "Ukończ swoją pierwszą Bitwę"));
        Achievements.Add(AchievementId.Sharpshooter, new Achievement(AchievementId.Sharpshooter, "Strzelec", "Traf 10 statków"));
        Achievements.Add(AchievementId.Destructor, new Achievement(AchievementId.Destructor, "Niszczyciel", "Zniszcz wszystkie statki wroga"));
        Achievements.Add(AchievementId.Unlucky, new Achievement(AchievementId.Unlucky, "Pechowiec", "Przegraj 5 gier pod rząd"));
        Achievements.Add(AchievementId.Veteran, new Achievement(AchievementId.Veteran, "Weteran", "Rozegraj 50 bitew"));
        Achievements.Add(AchievementId.PerfectGame, new Achievement(AchievementId.PerfectGame, "Perfekcyjna gra", "Ukończyłeś grę ze średnią celnością większą lub równą 90%"));
    }

    public void CheckAchievements(GameStats stats)
    {
        if (!Achievements[AchievementId.FirstBlood].Unlocked && stats.GamesPlayed >= 1)
            Achievements[AchievementId.FirstBlood].Unlock();

        if (!Achievements[AchievementId.Sharpshooter].Unlocked && stats.TotalHits >= 10)
            Achievements[AchievementId.Sharpshooter].Unlock();

        if (!Achievements[AchievementId.Destructor].Unlocked && stats.AllShipsDestroyed)
            Achievements[AchievementId.Destructor].Unlock();

        if (!Achievements[AchievementId.Unlucky].Unlocked && stats.MaxLoseStreak >= 5)
            Achievements[AchievementId.Unlucky].Unlock();

        if (!Achievements[AchievementId.Veteran].Unlocked && stats.GamesPlayed >= 50)
            Achievements[AchievementId.Veteran].Unlock();

        if (!Achievements[AchievementId.PerfectGame].Unlocked && stats.Accuracy >= 0.9)
            Achievements[AchievementId.PerfectGame].Unlock();
    }
