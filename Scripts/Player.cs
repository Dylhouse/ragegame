using Godot;
using System;

namespace Scripts;

public partial class Player : CharacterBody2D
{
	const float TerminalVelocity = 100.0f;
	const float TerminalAirVelocity = 150.0f;
	const float Acceleration = 400.0f;
	const float Deceleration = 250.0f;
	const float JumpVelocity = -275.0f;
	const float WallSlideVelocity = 150.0f;
	readonly float WallJumpXVelocity = 400.0f;
	const float WallJumpyVelocity = -400.0f;
	
	private bool wasOnFloor = false;
	private bool ableToJump = false;
	//private bool wasOnFloor = false;
	private bool ableToWallJump = false;

	//private Timer coyoteTimer;
	private AnimatedSprite2D animatedSprite;

    public override void _Ready()
    {
		animatedSprite = GetNode<AnimatedSprite2D>("PlayerSprite");
        //coyoteTimer = GetNode<Timer>("CoyoteTimer");
    }

    public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;
		bool onFloor = IsOnFloor();
		bool onWall = IsOnWall();
		float fdelta = (float) delta;

		if (onFloor) ableToJump = true;
		if (onWall) ableToWallJump = true;

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

			else if (ableToWallJump)
			{
				velocity = new Vector2(-WallJumpXVelocity, WallJumpyVelocity);
				ableToWallJump = false;
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

		//Handle wallslide
		if (onWall) 
		{
			if (velocity.Y > WallSlideVelocity) velocity.Y = WallSlideVelocity;
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