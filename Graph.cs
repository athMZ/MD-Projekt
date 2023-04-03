using System.Text;

namespace GraphMatrix;

public class Graph : Matrix
{
    public readonly double Probability;

    public Graph() : base(0, 0, 0)
    {
    }

    public Graph(int n, double p, double? fill) : base(n, n, fill)
    {
        Probability = p;
    }

    public Graph(Graph graph) : base(graph)
    {
        Probability = graph.Probability;
    }

    public Graph(List<List<int>> input) : base(input.Count, input[0].Count, 0)
    {
        for (int i = 0; i < input.Count; i++)
        {
            for (int j = 0; j < input[i].Count; j++)
            {
                A[i, j] = input[i][j];
            }
        }
    }

    public List<List<int>> GetMatrixAsInt()
    {
        List<List<int>> result = new(0);

        for (int i = 0; i < Rows; i++)
        {
            result.Add(new List<int>());

            for (int j = 0; j < Columns; j++)
            {
                result[i].Add(Convert.ToInt32(A[i, j]));
            }
        }

        return result;
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

    public List<int> CalculateDegSequence()
    {
        List<int> result = new();

        for (var i = 0; i < Rows; i++)
        {
            var degI = 0;

            for (var j = 0; j < Columns; j++)
            {
                degI += (int)A[i, j];
            }

            result.Add(degI);
        }

        return result.OrderByDescending(x => x).ToList();
    }

    public int GetGraphM()
    {
        return CalculateDegSequence().Sum() / 2;
    }

    public double GetGraphDensity()
    {
        return GetGraphM() / (0.5 * base.Rows * (base.Rows - 1));
    }

    //Vertices are counted form 1 up
    public List<int> GetNeighboursOfVertex(int vertex)
    {
        List<int> result = new();

        for (var j = 0; j < Columns; j++)
        {
            if (Math.Abs(A[vertex - 1, j] - 1) < double.Epsilon) result.Add(j + 1);
        }

        return result.Distinct().OrderBy(x => x).ToList();
    }

    public void DisplayAllNeighbours()
    {
        //We skip 0
        for (var i = 1; i <= Rows; i++)
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
        for (var i = 1; i <= Rows; i++)
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

    public void BFS(int startingVertex)
    {

        //We go from starting vertex and select all neighbours
        //We count the neighbours
        //We repeat this for evry neighbour 

        List<int> visited = new();
        int numOfVertices = Rows;

        //Klasa, wierzchołki, ile wierzchołków
        List<Tuple<int, string, int>> logs = new();

        int klasa = 0;

        logs.Add(new(klasa, startingVertex.ToString(), 1));

        do
        {
            klasa++;

            visited.Add(startingVertex);

            var neighbours = GetNeighboursOfVertex(startingVertex).Where(x => !visited.Contains(x)).ToList();

            logs.Add(new(klasa, string.Join(',',neighbours), neighbours.Count));

        } while (visited.Count == numOfVertices);

        Console.WriteLine();


    }
}