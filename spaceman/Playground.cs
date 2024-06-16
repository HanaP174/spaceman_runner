using System;
using Godot;
using Godot.Collections;

public partial class Playground : Node2D
{
	private static readonly Vector2 CamStartPos = new Vector2(576, 324);
	private static readonly Vector2 SpacemanStartPos = new Vector2(49, 422);

	private float _speed;
	private double _lastSpawnTime = 5.0;
	private int _score = 0;

	private const float StartSpeed = 7.0f;
	private const int MaxSpeed = 25;
	private const double TimeBetweenSpawning = 5.0;

	private Spaceman _spaceman;
	private Camera _camera;
	private Spawner _spawner;
	private CanvasLayer _scoreCanvas;
	private GameOverCanvas _gameOverCanvas;

	private Array<Node2D> _spawnedObjects = new Array<Node2D>();

	public override void _Ready()
	{
		_spaceman = GetNode<Spaceman>("Spaceman");
		_camera = GetNode<Camera>("Camera");
		_spawner = GetNode<Spawner>("Spawner");
		_scoreCanvas = GetNode<CanvasLayer>("ScoreCanvas");
		_gameOverCanvas = GetNode<GameOverCanvas>("GameOverCanvas");
		
		_spawner.ObjectAdded += AddObject;
		_spaceman.Felt += GameOver;
		_gameOverCanvas.Restart += RestartGame;
		_gameOverCanvas.Quit += QuitGame;

		if (_spaceman != null && _camera != null)
		{
			NewGame();
		}
	}

	private void NewGame()
	{
		_spaceman.Position = SpacemanStartPos;
		_spaceman.Velocity = Vector2.Zero;
		_camera.Position = CamStartPos;
		_spawner.SpawnGroupOfAsteroids(4);
	}

	public override void _Process(double delta)
	{
		_speed = StartSpeed; // todo change
		_camera.Move(_speed);
		_spaceman.Move(_camera.Position.X);

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
	
	private void AddObject(Node2D body)
	{
		_spawnedObjects.Add(body);
		if (body is Star star)
		{
			star.StarCollected += StarCollected;
		}
	}

	private void CleanNotVisibleObjects()
	{
		var toRemove = new Array<Node2D>();
		foreach (var obj in _spawnedObjects)
		{
			if (obj.Position.X < (_camera.Position.X - GetViewport().GetVisibleRect().Size.X))
			{
				toRemove.Add(obj);
			}
		}
		foreach (var obj in toRemove)
		{
			RemoveSpawnedObject(obj);
		}
		toRemove.Clear();
	}

	private void RemoveSpawnedObject(Node2D obj)
	{
		obj.QueueFree();
		_spawnedObjects.Remove(obj);
	}

	private void StarCollected(Area2D star)
	{
		_score++;
		RemoveSpawnedObject(star);
		UpdateScore();
	}

	private void UpdateScore()
	{
		var scoreLabel = _scoreCanvas.GetNode<Label>("ScoreLabel");
		scoreLabel.Text = "SCORE: " + _score;
	}

	private void GameOver()
	{
		GetTree().Paused = true;
		_gameOverCanvas.Show();
	}
	
	private void QuitGame()
	{
		_spawnedObjects.Clear();
		GetTree().Paused = false;
		GetTree().Quit();
	}

	private void RestartGame()
	{
		_spawnedObjects.Clear();
		NewGame();
		_gameOverCanvas.Hide();
		GetTree().Paused = false;
	}
}
