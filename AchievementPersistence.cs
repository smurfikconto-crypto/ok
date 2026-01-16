using System.IO;
using System.Text.Json;

public static class AchievementPersistence
{
    private const string FileName = "achievements.json";

    public static void Save(AchievementManager manager, GameStats stats)
    {
        var memento = new AchievementsMemento
        {
            GamesPlayed = stats.GamesPlayed,
            TotalHits = stats.TotalHits,
            MaxLoseStreak = stats.MaxLoseStreak
        };

        foreach (var a in manager.Achievements)
            memento.Unlocked[a.Key] = a.Value.Unlocked;

        var json = JsonSerializer.Serialize(memento, new JsonSerializerOptions
        {
            WriteIndented = true
        });

        File.WriteAllText(FileName, json);
    }

    public static void Load(AchievementManager manager, GameStats stats)
    {
        if (!File.Exists(FileName))
            return;

        var json = File.ReadAllText(FileName);
        var memento = JsonSerializer.Deserialize<AchievementsMemento>(json);

        if (memento == null)
            return;

        stats.GamesPlayed = memento.GamesPlayed;
        stats.TotalHits = memento.TotalHits;
        stats.MaxLoseStreak = memento.MaxLoseStreak;

        foreach (var entry in memento.Unlocked)
            if (entry.Value)
                manager.Achievements[entry.Key].Unlock();
    }
}
