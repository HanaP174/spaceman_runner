using Godot;
using System;

public partial class Spaceman : CharacterBody2D
{
	
	[Signal]
	public delegate void FeltEventHandler(); 
	
	private const float Gravity = 6200f;
	private const float JumpSpeed = -1500f;
	private const float CameraOffset = 490f;
	private AnimatedSprite2D _animatedSpaceman;
	private bool _wasOnFloor = false;
	private bool _isJumping = false;
	private bool _isFalling = false;
	private bool _initialized = false;
	private Timer _fallTimer;

	public override void _Ready()
	{
		_animatedSpaceman = GetNode<AnimatedSprite2D>("AnimatedSpaceman");
		_fallTimer = GetNode<Timer>("FallTimer");
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		velocity.Y += Gravity * (float)delta;
		if (IsOnFloor())
		{
			Init();
			_animatedSpaceman.Play("run");
			if (Input.IsActionJustPressed("ui_accept"))
			{
				velocity.Y = JumpSpeed;
				_isJumping = true;
				_isFalling = false;
				_animatedSpaceman.Play("jump");
			}
			_initialized = true;
		}
		else
		{
			HandleFalling(velocity);
		}
		Velocity = velocity;
		MoveAndSlide();
	}

	private void Init()
	{
		if (!_wasOnFloor)
		{
			_wasOnFloor = true;
			_isJumping = false;
			_isFalling = false;
		}
	}

	private void HandleFalling(Vector2 velocity)
	{
		if ((!_isJumping && velocity.Y > 1000 && _initialized) || (_isJumping && velocity.Y > 1000))
		{
			if (!_isFalling)
				_fallTimer.Start();

			_isFalling = true;
		}

		_wasOnFloor = false;
	}

	private void OnFallTimerTimeout()
	{
		if (_isFalling)
		{
			EmitSignal(SignalName.Felt);
		}
	}

	public void Move(float cameraPosX)
	{
		var position = Position;
		position.X = cameraPosX - CameraOffset;
		Position = position;
	}
}
