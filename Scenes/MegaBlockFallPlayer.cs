using Godot;
using System;

public partial class MegaBlockFallPlayer : AnimationPlayer
{
    public void OnPlayerEntered(Node2D body)
    {
        Play("MegaBlockFall");
    }
}
