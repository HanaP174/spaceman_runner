using Godot;
using System;

public partial class StartGameCanvas : CanvasLayer
{
	[Signal]
	public delegate void StartGameEventHandler();

	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("ui_accept"))
		{
			EmitSignal(SignalName.StartGame);
		}
	}
}
