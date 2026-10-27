using Godot;
using System;

public partial class Option : Node3D, Injection
{
    private LevelManager manager;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

    public void Injection(LevelManager levelManager)
    {
        manager = levelManager;
    }

    void _on_clear_score_pressed()
	{
        Manager.ResetHighscore();

    }
	void _on_exit_pressed()
	{
        manager.loadscene(0);
    }

}
