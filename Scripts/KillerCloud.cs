using Godot;
using Scripts;
using System;

public partial class KillerCloud : Area2D
{
    public void OnBodyEntered(Node2D body)
    {
        if (body is Player)
        {
            Player player = body as Player;
            player.Kill();
        }
    }
}
