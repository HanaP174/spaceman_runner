using Godot;
using System;

public partial class Spawner : Node2D
{
	[Export]
	private AsteroidBounds _bounds;

	private Random _random;
	private PackedScene _asteroidScene;
	private Vector2 _asteroidLastPosition = new Vector2(100, 557);

	private const float MaxDistance = 390f;
	private const float MinDistance = 340f;

	public override void _Ready()
	{
		_random = new Random();
		_asteroidScene = ResourceLoader.Load<PackedScene>("res://Asteroid.tscn");
	}

	public void SpawnGroupOfAsteroids(int count)
	{
		for (int i = 0; i < count; i++)
		{
			SpawnAsteroid();
		}
	}

	public void SpawnAsteroid()
	{
		var asteroidPosition = Vector2.Zero;
		var maxX = _asteroidLastPosition.X + MaxDistance;
		var minX = _asteroidLastPosition.X + MinDistance;
		asteroidPosition.X = _random.Next(Mathf.FloorToInt(minX), Mathf.FloorToInt(maxX));
		asteroidPosition.Y = _random.Next(Mathf.FloorToInt(_bounds.YMin), Mathf.FloorToInt(_bounds.YMax));

		_asteroidLastPosition = asteroidPosition;
		Console.WriteLine(_asteroidLastPosition);

		if (_asteroidScene.Instantiate() is StaticBody2D asteroid)
		{
			asteroid.Position = asteroidPosition;
			GetParent().AddChild(asteroid);
		}
	}
}
