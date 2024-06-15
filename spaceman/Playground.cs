using Godot;

public partial class Playground : Node2D
{
	private static readonly Vector2I CamStartPos = new Vector2I(576, 324);
	private static readonly Vector2I SpacemanStartPos = new Vector2I(62, 401);

	private float _speed;
	private const float StartSpeed = 10.0f;
	private const int MaxSpeed = 25;

	private Spaceman _spaceman;
	private Camera _camera;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_spaceman = GetNode<Spaceman>("Spaceman");
		_camera = GetNode<Camera>("Camera");
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
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		_speed = StartSpeed;
		_spaceman.Move(_speed);
		_camera.Move(_speed);
	}
}
