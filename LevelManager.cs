using Godot;
using System;

public partial class LevelManager : Node
{
    [ExportGroup("Get Scene")]
    [Export] public PackedScene Menu;
    [Export] public PackedScene Options;
    [Export] public PackedScene Game;

    private Node currentscene;

    public override void _Ready()
	{
        //Free Nodes with our current node if the current is not null
        loadscene(0);
   
    }

    public void loadscene(int levelscene)
    {
        if (currentscene != null)
        {
            currentscene.QueueFree();
        }

        
        PackedScene loadthesceneiwant;

        switch (levelscene)
        {
            case 0:
                loadthesceneiwant = Menu;
                break;
            case 1:
                loadthesceneiwant = Options;
                break;
            case 2:
                loadthesceneiwant = Game;
                
                break;
            default:
                loadthesceneiwant = Menu;
                break;
        }
        GD.Print(loadthesceneiwant);
        //Instantiate currentscene as child node of levelmanager.
        currentscene = loadthesceneiwant.Instantiate();
        AddChild(currentscene);
        //GetTree().CurrentScene = currentscene;
        //This checks if scene in injecetable to levelmanager.
        if (currentscene is Injection inject)
        {
            inject.Injection(this);
        }
    }


	

}
