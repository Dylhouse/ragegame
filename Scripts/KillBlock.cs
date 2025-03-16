using Godot;
using System;

namespace Scripts;

public partial class KillBlock : Sprite2D
{
    public void OnPlayerEntered(Node2D body)
    {
        if (body is Player)
        {
            Player player = body as Player;
            player.Kill();
        }
    }
}
