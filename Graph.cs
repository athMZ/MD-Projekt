using System.Text;

namespace GraphMatrix;

//We need to refactor code to use new Lists of nodes and edges

public class Graph : Matrix
{
    public readonly double Probability;

    private List<Node> Nodes { get; set; } = new();
    public List<Edge> Edges { get; set; } = new();

    public Graph() : base(0, 0, 0)
    {
        RepresentGraphAsNodesAndEdges();
    }

    public Graph(int n, double p, double? fill) : base(n, n, fill)
    {
        Probability = p;

        RepresentGraphAsNodesAndEdges();
    }

    public Graph(Graph graph) : base(graph)
    {
        Probability = graph.Probability;

        RepresentGraphAsNodesAndEdges();
    }

    public Graph(List<List<int>> input, double probability) : base(input.Count, input[0].Count, 0)
    {
        Probability = probability;

        for (var i = 0; i < input.Count; i++)
        {
            for (int j = 0; j < input[i].Count; j++)
            {
                A[i, j] = input[i][j];
            }
        }

        RepresentGraphAsNodesAndEdges();
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
            result.Add(GetVertexDeg(i));
        }

        return result.OrderByDescending(x => x).ToList();
    }

    public int GetVertexDeg(int vertex)
    {
        var degI = 0;

        for (var j = 0; j < Columns; j++)
        {
            degI += (int)A[vertex, j];
        }

        return degI;
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

    public Dictionary<int, List<int>> GetAllNeighbours()
    {
        var result = new Dictionary<int, List<int>>();

        for (var i = 1; i <= Rows; i++)
        {
            result.Add(i, GetNeighboursOfVertex(i));
        }

        return result;
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
        Console.WriteLine("Krawędzie");

        StringBuilder sb = new();

        //We skip 0
        for (var i = 1; i <= Rows; i++)
        {
            var neighbours = GetNeighboursOfVertex(i);

            foreach (var neighbour in neighbours)
            {
                sb.Append($"({i}, {neighbour}), ");
            }

            sb.Append('\n');
        }

        //Remove last '\n', ' ', ','
        sb.Length -= 3;

        Console.WriteLine(sb.ToString());
        Console.WriteLine();
    }

    public void BFS(int startVertex)
    {
        int numVertices = A.GetLength(0);
        bool[] visited = new bool[numVertices];

        Queue<int> queue = new Queue<int>();
        visited[startVertex] = true;
        queue.Enqueue(startVertex);

        while (queue.Count > 0)
        {
            int currentVertex = queue.Dequeue();
                
            Console.Write((currentVertex + 1) + " ");

            for (var i = 0; i < numVertices; i++)
            {
                if (A[currentVertex, i] != 1 || visited[i]) continue;
                visited[i] = true;
                queue.Enqueue(i);
            }
        }
    }

    public new void Print()
    {
        for (var i = 0; i < base.Rows; i++)
        {
            for (var j = 0; j < base.Columns; j++)
            {
                Console.ForegroundColor = Math.Abs(A[i, j] - 1) < double.Epsilon ? ConsoleColor.Green : ConsoleColor.White;

                Console.Write($"{A[i, j]} ");
            }

            Console.WriteLine();
        }

        Console.WriteLine();
    }

    private void RepresentGraphAsNodesAndEdges()
    {
        for (int i = 0; i < Rows; i++)
        {
            for (int j = 0; j < Columns; j++)
            {
                if (!(Math.Abs(A[i, j] - 1) < Double.Epsilon)) continue;

                var n1 = new Node(i, i);
                var n2 = new Node(j, j);

                Nodes.AddIfNotExists(n1);
                Nodes.AddIfNotExists(n2);
                Edges.AddIfNotExists(new Edge(n1, n2));

            }
        }
    }

    public int GetNumberOfNodes()
    {
        return Nodes.Count;
    }
}