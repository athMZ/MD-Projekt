namespace GraphMatrix.Entites;

public class Node
{
    public double PositionX { get; set; }
    public double PositionY { get; set; }

    public Node(double positionX, double positionY)
    {
        PositionX = positionX;
        PositionY = positionY;
    }

    public static bool operator ==(Node node1, Node node2)
    {
        return Math.Abs(node1.PositionX - node2.PositionX) < Double.Epsilon
               && Math.Abs(node1.PositionY - node2.PositionY) < Double.Epsilon;
    }

    public static bool operator !=(Node node1, Node node2)
    {
        return !(node1 == node2);
    }

    public override bool Equals(object? obj)
    {
        //Check for null and compare run-time types.
        if ((obj == null) || GetType() != obj.GetType())
        {
            return false;
        }

        Node node = (Node) obj;
        return this == node;
    }
}