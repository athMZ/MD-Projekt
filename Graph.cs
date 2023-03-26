using System.Text;

namespace GraphMatrx_Fill_Test;

public class Graph : Matrix
{
    public readonly double Probability;

    public Graph(int n, double p, double? fill) : base(n, n, fill)
    {
        Probability = p;
    }

    public Graph(Graph graph) : base(graph)
    {
        Probability = graph.Probability;
    }

    public void FillRandomly()
    {
        var rand = new Random();

        for (var i = 0; i < Rows; i++)
        {
            for (var j = i + 1; j < Columns; j++)
            {
                var a = rand.Next(0, 100);
                if (!(a < Probability * 100)) continue;

                A[i, j] = 1;
                A[j, i] = 1;
            }
        }
    }

    public List<double> CalculateDegSequence()
    {
        List<double> result = new();

        for (var i = 0; i < Rows; i++)
        {
            var degI = 0d;

            for (var j = 0; j < Columns; j++)
            {
                degI += A[i, j];
            }

            result.Add(degI);
        }

        return result.OrderByDescending(x => x).ToList();
    }

    //Vertices are counted form 1 up
    public List<int> GetNeighboursOfVertex(int vertex)
    {
        List<int> result = new();

        for (var j = 0; j < Columns; j++)
        {
            if (A[vertex - 1, j] == 1) result.Add(j + 1);
        }

        return result.Distinct().OrderBy(x => x).ToList();
    }


    public void DisplayAllNeighbours()
    {
        //We skip 0
        for (int i = 1; i <= Rows; i++)
        {
            Console.WriteLine($"Sąsiedzi wierzchołka {i}: {string.Join(", ", GetNeighboursOfVertex(i))}");
        }

        Console.WriteLine();
    }

    //We can use GetNeighboursOfVertex() to get all edges of a vertex
    public void DisplayAllEdges()
    {
        List<int> neighbours = new();

        Console.WriteLine("Krawędzie");

        StringBuilder sb = new();

        //We skip 0
        for (int i = 1; i <= Rows; i++)
        {
            neighbours = GetNeighboursOfVertex(i);

            foreach (var neighbour in neighbours)
            {
                sb.Append($"({i}, {neighbour}), ");
            }

            sb.Append('\n');
        }

        //Remove last '\n', ' ', ','
        sb.Length -= 3;

        Console.WriteLine(sb.ToString());

    }
}