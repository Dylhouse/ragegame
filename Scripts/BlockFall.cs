using Godot;
using System;

public partial class BlockFall : AnimationPlayer
{
    public void OnPlayerEntered(Node2D body)
    {
        GD.Print("wtf please work");
        Play("block_fall");
    }
}
