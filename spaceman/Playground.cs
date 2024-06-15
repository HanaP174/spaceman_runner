using System;
using Godot;

public partial class Playground : Node2D
{
	private static readonly Vector2I CamStartPos = new Vector2I(576, 324);
	private static readonly Vector2I SpacemanStartPos = new Vector2I(84, 401);

	private float _speed;
	private double _lastSpawnTime = 5.0;

	private const float StartSpeed = 7.0f;
	private const int MaxSpeed = 25;
	private const double TimeBetweenSpawning = 5.0;

	private Spaceman _spaceman;
	private Camera _camera;
	private Spawner _spawner;

	public override void _Ready()
	{
		_spaceman = GetNode<Spaceman>("Spaceman");
		_camera = GetNode<Camera>("Camera");
		_spawner = GetNode<Spawner>("Spawner");

		if (_spaceman != null && _camera != null)
		{
			NewGame();
		}
	}

	private void NewGame()
	{
		_spaceman.Position = SpacemanStartPos;
		_spaceman.Velocity = new Vector2I(0, 0);
		_camera.Position = CamStartPos;
		_spawner.SpawnGroupOfAsteroids(4);
	}

	public override void _Process(double delta)
	{
		_speed = StartSpeed;
		// _spaceman.Move(_speed);
		// _camera.Move(_speed);
		
		_lastSpawnTime += delta * _speed;
		if (_lastSpawnTime >= TimeBetweenSpawning)
		{
			_spawner.SpawnAsteroid();
			_lastSpawnTime = 0;
		}
	}
}
