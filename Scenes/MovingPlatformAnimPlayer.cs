using Godot;
using System;

public partial class MovingPlatformAnimPlayer : AnimationPlayer
{
    public void OnPlayerEntered(Node2D body)
    {
        Play("move_left");
    }

    public void OnOrbGet(Node2D body)
    {
        Play("move_right");
    }
}
