using Godot;
using System;

public partial class PlayerDetectionArea : Area2D
{
    public void OnPlayerEntered(Node2D body)
    {
        QueueFree();
    }
}
