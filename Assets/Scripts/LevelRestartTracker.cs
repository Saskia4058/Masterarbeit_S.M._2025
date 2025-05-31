using System.Collections.Generic;
using UnityEngine;

public static class LevelRestartTracker
{
    private static HashSet<string> restartedLevels = new HashSet<string>();

    public static void MarkLevelRestarted(string levelName)
    {
        restartedLevels.Add(levelName);
    }

    public static bool WasLevelRestarted(string levelName)
    {
        return restartedLevels.Contains(levelName);
    }

    public static void Clear()
    {
        restartedLevels.Clear();
    }
}
