using Godot;
using System;

public partial class WinTileMapLayer : TileMapLayer
{
    public void OnPlayerKilled(Node2D body)
    {
        Visible = true;
    }
}
