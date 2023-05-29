namespace GraphMatrix.Entites;

public class Edge
{
    private readonly Node _start;
    private readonly Node _end;

    private double _distanceX;
    private double _distanceY;

    public Edge(Node start, Node end)
    {
        _start = start;
        _end = end;
        UpdateDistance();
    }

    public void UpdateDistance()
    {
        _distanceX = double.Abs(_start.PositionX - _end.PositionX);
        _distanceY = double.Abs(_start.PositionY - _end.PositionY);
    }
}