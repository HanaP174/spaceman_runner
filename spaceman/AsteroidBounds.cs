using Godot;

public partial class AsteroidBounds : Node2D
{
	private Marker2D _topLeft;

	private Marker2D _bottomRight;

	public float XMin;
	public float YMin;
	public float XMax;
	public float YMax;

	public override void _Ready()
	{
		_topLeft = GetNode<Marker2D>("TopLeft");
		_bottomRight = GetNode<Marker2D>("BottomRight");
		XMin = _topLeft.Position.X;
		YMin = _topLeft.Position.Y;
		XMax = _bottomRight.Position.X;
		YMax = _bottomRight.Position.Y;
	}

	public override void _Process(double delta)
	{
	}
}
