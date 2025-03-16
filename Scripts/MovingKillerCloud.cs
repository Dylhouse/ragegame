using Godot;
using System;

public partial class MovingKillerCloud : KillerCloud
{
    /// <summary>
    /// The speed the cloud will move at, x and y (not normalized, just x and y)
    /// </summary>
    private const float MoveSpeed = 400.0f;

    private Vector2 MoveVector => new(MoveSpeed, MoveSpeed);

    private bool moving = false;

    public void OnPlayerEntered(Node2D body)
    {
        moving = true;
    }

    public override void _Process(double delta)
    {
        if (!moving) return;
        Position += MoveVector * (float) delta;
    }
}
