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

    public void BFS(int startingVertex)
    {
        if (startingVertex <= 0) startingVertex = 0;

        var Tree = new Matrix(base.Rows - 1, 2, null);
        var numberOfLayer = new int[base.Rows];
        int sum = 0;
        int branchCounter = 0;
        int ini_ver = startingVertex;

        var flags = new bool[base.Rows];
        Array.Fill(flags, false);

        var Layer = new Matrix(base.Rows - 1, base.Columns - 1, null);

        for (int j = 0; j < base.Columns; j++)
        {
            if (A[ini_ver, j] == 1)
            {
                flags[j] = true;
                branchCounter++;
                Tree[branchCounter, 1] = ini_ver;
                Tree[branchCounter, 2] = j;

                //numberOfLayer[]
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
}