using Godot;
using System;

public partial class Manager : Node
{
    private const string SavePath = "user://save.dat";

    // Save only if the new time is higher than the previous best
    public static void SaveHighscore(float currentTime)
    {
        float previousBest = Load();        // load the existing highscore
        if (currentTime > previousBest)     // only save if new record
        {
            using var file = FileAccess.Open(SavePath, FileAccess.ModeFlags.Write);
            file.StoreFloat(currentTime);
            GD.Print("New Highscore Saved: " + currentTime);
        }
    }

    // Load the saved highscore
    public static float Load()
    {
        if (!FileAccess.FileExists(SavePath))
            return 0f;

        using var file = FileAccess.Open(SavePath, FileAccess.ModeFlags.Read);
        float loadedData = file.GetFloat();
        GD.Print("Loaded Highscore: " + loadedData);
        return loadedData;
    }

    // Optional: reset highscore
    public static void ResetHighscore()
    {
        using var file = FileAccess.Open(SavePath, FileAccess.ModeFlags.Write);
        file.StoreFloat(0f);
        GD.Print("Highscore reset to 0");
    }
}
