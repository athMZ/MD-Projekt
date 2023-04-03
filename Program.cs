using System.Text.Json;

namespace GraphMatrix;

internal class Program
{
    private static (int parsedN, double parsedP) ParseData(string? n, string? p)
    {
        p = p?.Replace('.', ',');

        Console.WriteLine();

        if (int.TryParse(n, out var parsedN) && double.TryParse(p, out var parsedP)) return (parsedN, parsedP);

        Console.WriteLine("Podano niepoprawne dane");
        return (0, 0.0);
    }

    private static (int parsedN, double parsedP) InputData()
    {
        Console.WriteLine("Podaj liczbę wierzchołków n oraz prawdopodobieństwo p, aby wygenerować losowy graf:");

        Console.Write("n = ");
        var n = Console.ReadLine();

        Console.Write("p = ");
        var p = Console.ReadLine();

        return ParseData(n, p);
    }

    private static void StudyGraph(Graph? graph)
    {
        if (graph == null) return;

        Console.WriteLine("Wprowadzone dane:");
        Console.WriteLine($"\tIlosć wierzchołków: n = {graph.Rows}");
        Console.WriteLine($"\tPrawdopodobieństwo: p = {graph.Probability}");
        Console.WriteLine();

        Console.WriteLine("Macierz grafu:");
        graph.Print();

        graph.DisplayAllNeighbours();
        graph.DisplayAllEdges();

        var degSequence = graph.CalculateDegSequence();
        Console.Write($"Ciąg stopni: ({string.Join(", ", degSequence)})\n");
        Console.WriteLine($"Suma ciągu: {degSequence.Sum()}");
        Console.WriteLine();

        Console.WriteLine($"Liczba krawędzi grafu m: {graph.GetGraphM()}");
        Console.WriteLine($"Gęstość: {graph.GetGraphDensity()}");

/*        Console.WriteLine("Naciśnij dowolny klawisz, aby przeszukać graf");
        Console.ReadLine();

        Console.Write("Podaj wierzchołek, od którego chcesz rozpocząć przeszukiwanie: ");
        var input = Console.ReadLine();
        if (int.TryParse(input, out var parsedInput))
        {
            graph.BFS(parsedInput);
        }
        else
        {
            Console.WriteLine("Niepoprawne dane");
        }*/

        graph.BFS(1);

        DrawingModule.DrawGraphOnCircle(graph);
    }

    private static Graph GetRandomGraph(int parsedN, double parsedP)
    {
        Graph graph = new(parsedN, parsedP, 0);
        graph.FillRandomly();

        return graph;
    }

    private static void Main(string[] args)
    {
        Console.WriteLine("Gabriel Siwiec, Mikołaj Zuziak\n");

        //Refactor probably needed

        Graph? graph = null;

        //User input
        if (graph is null && args.Length <= 0)
        {
            var (n, p) = InputData();
            graph = GetRandomGraph(n, p);
        }

        //From params
        if (graph is null && args[0] == "-p")
        {
            var (n, p) = ParseData(args[1], args[2]);
            graph = GetRandomGraph(n, p);
        }

        //From Json
        if (graph is null && args[0] == "-j")
        {
            try
            {
                var fileName = args[1];
                var jsonString = File.ReadAllText(fileName);

                var input = JsonSerializer.Deserialize<Tuple<List<List<int>>, double>>(jsonString);
                graph = new Graph(input.Item1, input.Item2);
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
            var (n, p) = InputData();
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
                var toSerialize = new Tuple<List<List<int>>, double>(graph.GetMatrixAsInt(),
                    graph.Probability);

                var options = new JsonSerializerOptions { WriteIndented = true };
                var jsonString = JsonSerializer.Serialize(toSerialize, options);

                Console.WriteLine(jsonString);

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