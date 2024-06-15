using Godot;
using System;

public partial class Star : Area2D
{
	[Signal]
	public delegate void StarCollectedEventHandler(Area2D star);
	
	private AnimatedSprite2D _animatedStar;

	public override void _Ready()
	{
		_animatedStar = GetNode<AnimatedSprite2D>("AnimatedStar");
	}

	public override void _Process(double delta)
	{
		_animatedStar.Play("shining");
	}

	public void OnBodyEntered(Node2D body)
	{
		if (body.Name == "Spaceman")
		{
			EmitSignal(SignalName.StarCollected, this);
		}
	}
}
