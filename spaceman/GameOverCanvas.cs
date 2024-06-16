using Godot;
using System;

public partial class GameOverCanvas : CanvasLayer
{
	[Signal]
	public delegate void RestartEventHandler();
	[Signal]
	public delegate void QuitEventHandler();

	public override void _Ready()
	{
		Hide();
	}

	public override void _Process(double delta)
	{
	}

	public void OnRestartClicked()
	{
		EmitSignal(SignalName.Restart);
	}

	public void OnQuitGame()
	{
		EmitSignal(SignalName.Quit);
	}
}
