using Godot;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
[Tool]
public partial class TelemetryVisualizer : Control
{
	[Export]
	public float CircleRadius  = 5.0f;

	[Export]
	public Color CircleColor = new Color(1f, 0f, 0f, 0.15f);
	private string filePath = "user://mouse_positions.json";
	private List<List<VisualizerMousePoint>> sessions = new();


	public override void _Draw()
	{
		foreach(List<VisualizerMousePoint> session in sessions)
		{
			foreach(VisualizerMousePoint point in session)
			{
				Vector2 position = new Vector2(point.X, point.Y);
				DrawCircle(position, CircleRadius, CircleColor);
			}
		}
	}
	
	public class VisualizerMousePoint()
	{
		public float X { get; set; }
		public float Y { get; set; }
	}

	private void LoadData()
	{
		string path = ProjectSettings.GlobalizePath(filePath);
		if (!File.Exists(path))
		{
			GD.Print("Telemetry JSON file not found.");
			return;
		}
		string json = File.ReadAllText(path);
		sessions = JsonSerializer.Deserialize<List<List<VisualizerMousePoint>>>(json);
		if (sessions == null)
		{
			sessions = new List<List<VisualizerMousePoint>>();
		}
		GD.Print("Loaded sessions: ", sessions.Count);
	}

	public override void _Ready()
	{
		LoadData();
		QueueRedraw(); 
	}
}
