using Godot;
using Scripts;
using System;

public partial class EndSpike : KillBlock
{
    private TileMapLayer winLayer;

    public override void _Ready()
    {
        winLayer = GetNode<TileMapLayer>("WinTileMapLayer");
    }

    new public void OnPlayerEntered(Node2D body)
    {
        base.OnPlayerEntered(body);
        winLayer.Visible = true;
    }
}
