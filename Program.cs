using GraphMatrx_Fill_Test;
using System.Text.Json;
using System.Text.Json.Nodes;

class Program
{
    static (int parsedN, double parsedP) ParseData(string? n, string? p)
    {
        p = p?.Replace('.', ',');

        Console.WriteLine();

        if (!int.TryParse(n, out var parsedN) || !double.TryParse(p, out var parsedP))
        {
            Console.WriteLine("Podano niepoprawne dane");
            return (0, 0.0);
        }

        return (parsedN, parsedP);
    }

    static (int parsedN, double parsedP) InputData()
    {
        Console.WriteLine("Podaj liczbę wierzchołków n oraz prawdopodobieństwo p, aby wygenerować losowy graf:");

        Console.Write("n = ");
        var n = Console.ReadLine();

        Console.Write("p = ");
        var p = Console.ReadLine();

        return ParseData(n, p);
    }

    static void StudyGraph(Graph graph)
    {
        Console.WriteLine("Wprowadzone dane:");
        Console.WriteLine($"\tIlosć wierzchołków: n = {graph.Rows}");
        Console.WriteLine($"\tPrawdopodobieństwo: p = {graph.Probability}");
        Console.WriteLine();

        Console.WriteLine("Macierz grafu:");
        graph.Print();

        var degSequence = graph.CalculateDegSequence();
        Console.Write($"Ciąg stopni: ({string.Join(", ", degSequence)})\n");
        Console.WriteLine();

        graph.DisplayAllNeighbours();
        graph.DisplayAllEdges();
        graph.BFS(1);
    }

    static Graph GetRandomGraph(int parsedN, double parsedP)
    {
        Graph graph = new Graph(parsedN, parsedP, 0);
        graph.FillRandomly();

        return graph;
    }

    static void Main(string[] args)
    {
        Console.WriteLine("Gabriel Siwiec, Mikołaj Zuziak\n");

        //Refactor probably needed

        Graph graph = null;

        //User input
        if (graph is null && args.Length <= 0)
        {
            (int n, double p) = InputData();
            graph = GetRandomGraph(n, p);
        }

        //From params
        if (graph is null && args[0] == "-p")
        {
            (int n, double p) = ParseData(args[1], args[2]);
            graph = GetRandomGraph(n, p);
        }

        //From Json
        if (graph is null && args[0] == "-j")
        {
            try
            {
                var fileName = args[1];
                var jsonString = File.ReadAllText(fileName);

                var input = JsonSerializer.Deserialize<List<List<int>>>(jsonString);
                graph = new(input);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Nie załadowano pliku .json!");
                Console.WriteLine(ex.Message);
            }

        }

        //Save graph matrix
        if (graph is null && args[0] == "-g")
        {
            (int n, double p) = InputData();
            graph = GetRandomGraph(n, p);

            string fileName;

            try
            {
                fileName = args[1];
            }
            catch
            {
                fileName = "graph.json";
            }

            try
            {
                var options = new JsonSerializerOptions { WriteIndented = true };
                var jsonString = JsonSerializer.Serialize(graph.GetMatrixAsInt(), options);

                File.WriteAllText(fileName, jsonString);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Nie zapisano pliku .json!");
                Console.WriteLine(ex.Message);
            }

            return;
        }

        if (graph is null) return;
        StudyGraph(graph);
    }
}