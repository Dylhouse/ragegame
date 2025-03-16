using Godot;
using System;

namespace Scripts;

public partial class Player : CharacterBody2D
{
	/// <summary>
	/// Max/min pitch shift of death sound.
	/// </summary>
	private const float PitchShiftRange = 0.2f;

	const float TerminalVelocity = 110.0f;
	const float TerminalAirVelocity = 150.0f;
	const float Acceleration = 400.0f;
	const float Deceleration = 250.0f;
	const float JumpVelocity = -275.0f;
	const float WallSlideVelocity = 150.0f;
	readonly float WallJumpXVelocity = 400.0f;
	const float WallJumpyVelocity = -400.0f;

	const float DeathDepth = 135.0f;

	private bool dead = false;
	
	private bool wasOnFloor = false;
	private bool ableToJump = false;
	//private bool wasOnFloor = false;
	private bool ableToWallJump = false;

	//private Timer coyoteTimer;
	private AnimatedSprite2D animatedSprite;
	private CpuParticles2D deathParticles;
	private Timer respawnTimer;
	private AudioStreamPlayer deathSoundPlayer;
	private AudioStreamPlayer musicPlayer;

	public void Kill()
	{
		dead = true;
		deathSoundPlayer.Play();
		GD.Print(respawnTimer.Paused);
		respawnTimer.Start();
		deathParticles.Emitting = true;
		animatedSprite.Visible = false;
	}

	public void OnRespawnTimerTimeout()
	{
		GetTree().ReloadCurrentScene();
	}

    public override void _Ready()
    {
		animatedSprite = GetNode<AnimatedSprite2D>("PlayerSprite");
		deathParticles = GetNode<CpuParticles2D>("DeathParticles");
		respawnTimer = GetNode<Timer>("RespawnTimer");
		deathSoundPlayer = GetNode<AudioStreamPlayer>("DeathSoundPlayer");
		musicPlayer = GetNode<AudioStreamPlayer>("MusicPlayer");

		musicPlayer.Play();

		deathSoundPlayer.PitchScale = (GD.Randf() - 0.5f) * 2.0f * PitchShiftRange + 1.0f;
        //coyoteTimer = GetNode<Timer>("CoyoteTimer");
    }

    public override void _Process(double delta)
    {
        if (Input.IsActionJustPressed("reset")) GetTree().ReloadCurrentScene();
    }

    public override void _PhysicsProcess(double delta)
	{
		if (dead) return;
		if (Position.Y > DeathDepth)
		{
			Kill();
		}
		Vector2 velocity = Velocity;
		bool onFloor = IsOnFloor();
		float fdelta = (float) delta;

		if (onFloor) ableToJump = true;

		// Add the gravity.
		if (!onFloor)
		{
			var gravity = GetGravity();
			gravity *= fdelta;
			velocity += gravity;
		}

		// Handle Jump.
		if (Input.IsActionJustPressed("jump") && onFloor)
		{
			if (ableToJump)
			{
				velocity.Y = JumpVelocity;
				ableToJump = false;
			}
		}

		// Get the input direction and handle the movement/deceleration.
		Vector2 direction = Vector2.Zero;
		float XSlowing = 0;

		if (Input.IsActionPressed("move_left")) 
		{
			animatedSprite.Play("walk");
			direction = new Vector2(-1, 0);
			animatedSprite.FlipH = true;
		}
		else if (Input.IsActionPressed("move_right")) 
		{
			animatedSprite.Play("walk");
			direction = new Vector2(1, 0);
			animatedSprite.FlipH = false;
		}

		else animatedSprite.Play("idle");

		//If the player is asking to move
		if (direction != Vector2.Zero)
		{
			
			velocity.X += direction.X * Acceleration * fdelta;
			if (onFloor && Mathf.Abs(velocity.X) > TerminalVelocity) velocity.X = TerminalVelocity * direction.X;
		}

		//If the player isn't asking to move, start slowing down
		else
		{
			XSlowing = Mathf.Sign(velocity.X) * Deceleration * fdelta;

			//Check if the slowing would cause a switch in direction
			if (Mathf.Sign(velocity.X - XSlowing) != Mathf.Sign(velocity.X))
			{
				velocity.X = 0;
			}

			else velocity.X -= XSlowing;
		}

		if (!onFloor)
		{
			if (Mathf.Abs(velocity.X) > TerminalVelocity) velocity.X += Acceleration * -Mathf.Sign(velocity.X) * fdelta;
		}

		wasOnFloor = IsOnFloor();
		Velocity = velocity;
		MoveAndSlide();
	}
}