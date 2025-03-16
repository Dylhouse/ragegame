using Godot;
using System;

public partial class DownCrusher: TileMapLayer
{
    const float MoveSpeed = 500.0f;
    const float ReturnSpeed = MoveSpeed / 3.0f;
    const float MoveDist = 16.0f * 10.0f;

    private bool moving = false;
    private bool returning = false;

    private float initialPos;
    private AudioStreamPlayer crash;

    public override void _Ready()
    {
        initialPos = Position.Y;
        crash = GetNode<AudioStreamPlayer>("CrashSound");
    }

    public override void _PhysicsProcess(double delta)
    {
        if (!moving) return;

        var position = Position;

        if (returning)
        {
            position.Y -= ReturnSpeed * (float) delta;

            if (position.Y < initialPos)
            {
                returning = false;
                position.Y = initialPos;
            }
        }
        
        else
        {

            position.Y += MoveSpeed * (float) delta;

            if (position.Y >= MoveDist + initialPos)
            { 
                returning = true;
                position.Y = MoveDist + initialPos;
                crash.Play();
            }

        }
        
        Position = position;
    }


    public void OnBodyEntered(Node2D body)
    {
        moving = true;  
    }
}
