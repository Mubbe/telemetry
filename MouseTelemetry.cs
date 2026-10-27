using Godot;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public partial class MouseTelemetry : Node
{
    [Export] 
    public float RecordInterval = 0.1f;
    private float timer = 0f;
    private List<Vector2> listmousePositions = new();
    private string filePath = "user://mouse_positions.json";
    
    public override void _Process(double delta)
    {
        timer += (float)delta;
        if (timer >= RecordInterval)
        {
            timer = 0f;
            Vector2 mousePosition = GetViewport().GetMousePosition();
            listmousePositions.Add(mousePosition);
            GD.Print("Total recorded positions: ",  listmousePositions.Count);
        }
    }

    private void savedata()
    {
       string path = ProjectSettings.GlobalizePath(filePath);
       List<List<MousePoint>> sessions = new();

       if(File.Exists(path))
        {
            List<List<MousePoint>> loadedSessions = JsonSerializer.Deserialize<List<List<MousePoint>>>(File.ReadAllText(path));
            if (loadedSessions != null)
            {
                sessions = loadedSessions;
            }
        }
        List<MousePoint> currentSession = new();
        foreach (Vector2 position in listmousePositions)
        {
            currentSession.Add(new MousePoint(position.X, position.Y));
        }
        sessions.Add(currentSession);

        string json = JsonSerializer.Serialize(sessions, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(path, json);
        GD.Print("Telemetry saved to: ", path);

    }
    public class MousePoint
    {
        public float X { get; set; }
        public float Y { get; set; }

        public MousePoint(float x, float y)
        {
            X = x;
            Y = y;
        }
          public MousePoint() { }
    }
    public override void _ExitTree()
    {
        savedata();
    }
}
