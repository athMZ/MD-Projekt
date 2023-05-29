using GraphMatrix.Entites;

namespace GraphMatrix.Entites;

public class Layout
{
    private readonly Graph _graph;
    private const int _repulsionForceConstant = 9000;
    private const double _attractionForceConstant = 0.3;
    private const double _dampingConstant = 0.1;
    private const double _timeStep = 0.1;
    private const int _iterations = 200;

    public Layout(Graph graph)
    {
        _graph = graph;
    }

    public Graph GetGraphModel()
    {
        for (int i = 0; i < _iterations; i++) UpdatePositions();

        return _graph;
    }

    public void UpdatePositions()
    {
        //Update edge distances
        _graph.Edges.ForEach(edge => edge.UpdateDistance());

        //Calculate repulsive forces
        var repulsiveForces = new List<double>(_graph.NumberOfNodes);
    }
}