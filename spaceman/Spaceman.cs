using Godot;
using System;

public partial class Spaceman : CharacterBody2D
{

	private const float Gravity = 6200f;
	private const float JumpSpeed = -1800f;
	private const float CameraOffset = 490f;
	private AnimatedSprite2D _animatedSpaceman;

	public override void _Ready()
	{
		_animatedSpaceman = GetNode<AnimatedSprite2D>("AnimatedSpaceman");
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		velocity.Y += Gravity * (float)delta;
		if (IsOnFloor())
		{
			_animatedSpaceman.Play("run");
			if (Input.IsActionJustPressed("ui_accept"))
				velocity.Y = JumpSpeed;
		}
		else
		{
			_animatedSpaceman.Play("jump");
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

	public void Move(float cameraPosX)
	{
		var position = Position;
		position.X = cameraPosX - CameraOffset;
		Position = position;
	}
}
