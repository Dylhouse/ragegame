using Godot;
using System;

public partial class WallMove1 : TileMapLayer
{
    const float MoveSpeed = 250.0f;
    const float MoveDist = 16.0f * 5.0f;

    private bool moving = false;

    private float initialPos;

    public override void _Ready()
    {
        initialPos = Position.X;
    }

    public override void _PhysicsProcess(double delta)
    {
        if (!moving) return;

        var position = Position;
        position.X += MoveSpeed * (float) delta;

        if (position.X >= MoveDist + initialPos)
        { 
            moving = false;
            position.X = MoveDist + initialPos;
        }
        
        Position = position;
    }


    public void OnBodyEntered(Node2D body)
    {
        moving = true;  
    }
}
