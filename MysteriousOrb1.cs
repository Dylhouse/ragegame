using Godot;
using System;

public partial class MysteriousOrb1 : PlayerDetectionArea
{
    private AnimatedSprite2D sprite;

    public override void _Ready()
    {
        sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
    }
    
    new public void OnPlayerEntered(Node2D body)
    {
        sprite.QueueFree();
        base.OnPlayerEntered(body);
    }
}
