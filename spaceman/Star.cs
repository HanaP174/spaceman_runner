using Godot;
using System;

public partial class Star : Area2D
{
	private AnimatedSprite2D _animatedStar;

	public override void _Ready()
	{
		_animatedStar = GetNode<AnimatedSprite2D>("AnimatedStar");
	}

	public override void _Process(double delta)
	{
		_animatedStar.Play("shining");
	}
}
