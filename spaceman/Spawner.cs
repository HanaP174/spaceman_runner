using Godot;
using System;

public partial class Spawner : Node2D
{
	[Export]
	private AsteroidBounds _bounds;
	
	[Signal]
	public delegate void ObjectAddedEventHandler(Node2D body); 

	private Random _random;
	private PackedScene _starScene;
	private PackedScene _asteroidScene;
	private Vector2 _asteroidLastPosition = new Vector2(100, 557);

	private const float MaxDistance = 420f;
	private const float MinDistance = 350f;
	private const float StarAsteroidDistance = 160f;

	public override void _Ready()
	{
		_random = new Random();
		_asteroidScene = ResourceLoader.Load<PackedScene>("res://Asteroid.tscn");
		_starScene = ResourceLoader.Load<PackedScene>("res://Star.tscn");
	}

	public void SpawnGroupOfAsteroids(int count)
	{
		for (int i = 0; i < count; i++)
		{
			SpawnAsteroid();
			SpawnStar();
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

		if (_asteroidScene.Instantiate() is StaticBody2D asteroid)
		{
			asteroid.Position = asteroidPosition;
			GetParent().AddChild(asteroid);
			EmitSignal(SignalName.ObjectAdded, asteroid);
		}
	}

	public void SpawnStar()
	{
		var starPosition = new Vector2(_asteroidLastPosition.X, _asteroidLastPosition.Y - StarAsteroidDistance);
		if (_starScene.Instantiate() is Area2D star)
		{
			star.Position = starPosition;
			GetParent().AddChild(star);
			EmitSignal(SignalName.ObjectAdded, star);
		}
	}
}
