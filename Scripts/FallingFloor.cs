using Godot;

public partial class FallingFloor : TileMapLayer
{
    const float MoveSpeed = 100.0f;
    private bool moving = false;

    private AudioStreamPlayer pitFallSoundPlayer;

    public override void _Ready()
    {
        pitFallSoundPlayer = GetNode<AudioStreamPlayer>("PitFallNoisePlayer");
    }

    public override void _PhysicsProcess(double delta)
    {
        if (GlobalPosition.Y > 200) QueueFree();
        if (!moving) return;

        var position = Position;
        position.Y += MoveSpeed * (float) delta;
        Position = position;
    }


    public void OnBodyEntered(Node2D body)
    {
        CollisionEnabled = false;
        moving = true; 
        pitFallSoundPlayer.Play();
    }
}
