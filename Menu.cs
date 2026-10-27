using Godot;
using System;

public partial class Menu : Node3D, Injection
{
    private LevelManager manager;
    [Export] Label highscore;
    public override void _Ready()
    {
        // Get the label node
        highscore = GetNode<Label>("Control/VBoxContainer/HighScore");

        // Load saved highscore
        float savedHighscore = Manager.Load();

        // Display it
        highscore.Text = "Highscore: " + savedHighscore.ToString("0.00");
    }
    public void Injection(LevelManager levelManager)
    {
        manager = levelManager;
    }
    void _on_play_pressed()
	{
        manager.loadscene(2);
    }

    void _on_options_pressed()
    {
        manager.loadscene(1);
    }

    void _on_exit_pressed()
    {
        GetTree().Quit();
    }

}
