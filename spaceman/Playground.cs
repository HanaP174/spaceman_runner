using Godot;
using Godot.Collections;

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

	private Array<StaticBody2D> _spawnedObjects = new Array<StaticBody2D>();

	public override void _Ready()
	{
		_spaceman = GetNode<Spaceman>("Spaceman");
		_camera = GetNode<Camera>("Camera");
		_spawner = GetNode<Spawner>("Spawner");
		
		_spawner.ObjectAdded += AddObject;

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
		_spaceman.Move(_speed);
		_camera.Move(_speed);
		
		CleanNotVisibleObjects();
		
		SpawnObjects(delta);
	}

	private void SpawnObjects(double delta)
	{
		_lastSpawnTime += delta * _speed;
		if (_lastSpawnTime >= TimeBetweenSpawning)
		{
			_spawner.SpawnAsteroid();
			_spawner.SpawnStar();
			_lastSpawnTime = 0;
		}
	}
	
	private void AddObject(StaticBody2D body)
	{
		_spawnedObjects.Add(body);
	}

	private void CleanNotVisibleObjects()
	{
		var toRemove = new Array<StaticBody2D>();
		foreach (var obj in _spawnedObjects)
		{
			if (obj.Position.X < (_camera.Position.X - GetViewport().GetVisibleRect().Size.X))
			{
				toRemove.Add(obj);
			}
		}
		foreach (var obj in toRemove)
		{
			obj.QueueFree();
			_spawnedObjects.Remove(obj);
		}
		toRemove.Clear();
	}
}
