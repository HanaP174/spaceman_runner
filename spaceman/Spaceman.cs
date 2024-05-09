using Godot;
using System;

public partial class Spaceman : CharacterBody2D
{
	// public const float Speed = 300.0f;
	// public const float JumpVelocity = -400.0f;

	private const float Gravity = 4200f;
	private const float JumpSpeed = -1800f;
	private AnimatedSprite2D animatedSprite;

	public override void _Ready()
	{
		animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSpaceman");
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		velocity.Y += Gravity * (float)delta;
		if (IsOnFloor())
		{
			animatedSprite.Play("run");
			if (Input.IsActionJustPressed("ui_accept"))
				velocity.Y = JumpSpeed;
		}
		else
		{
			animatedSprite.Play("jump");
		}

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		// Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		// if (direction != Vector2.Zero)
		// {
		// 	velocity.X = direction.X * Speed;
		// }
		// else
		// {
		// 	velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
		// }
		//
		Velocity = velocity;
		MoveAndSlide();
	}
}
