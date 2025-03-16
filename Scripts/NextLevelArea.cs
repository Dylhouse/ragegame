using Godot;
using System;

public partial class NextLevelArea : Area2D
{
    [Export]
    public PackedScene Level { get; set; }
    
    public void OnPlayerEntered(Node2D body)
    {
        GetTree().ChangeSceneToPacked(Level);
    }
}
