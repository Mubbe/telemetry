using Godot;
using System;

public partial class Game : Node3D, Injection
{
    [Export] Label timeLabel;
    float Time= 10f;
    bool gameover = false;
    private LevelManager levelmanager;
   
    public void Injection(LevelManager levelManager)
    {
        levelmanager = levelManager;
    }


    public override void _Ready()
    {
        timeLabel = GetNode<Label>("Label");
    }


    
    public override void _Process(double delta)
	{
        
        if (gameover)
        {
          return;
        }


        Time -= (float)delta;
        if (Input.IsMouseButtonPressed(MouseButton.Left) || Time <=0)
        {
            gameover = true;

            Manager.SaveHighscore(Time);
            Time =0;

           

            levelmanager.loadscene(0);


        }
       timeLabel.Text = Time.ToString("Timer:0.00");
      
    }
}
