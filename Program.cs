using GraphMatrx_Fill_Test;

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

    static void Main(string[] args)
    {
        Console.WriteLine("Gabriel Siwiec, Mikołaj Zuziak");

        int parsedN;
        double parsedP;

        if (args.Length > 0) (parsedN, parsedP) = ParseData(args[0], args[1]);
        else (parsedN, parsedP) = InputData();

        Console.WriteLine("Wprowadzone dane:");
        Console.WriteLine($"\tIlosć wierzchołków: n = {parsedN}");
        Console.WriteLine($"\tPrawdopodobieństwo: p = {parsedP}");
        Console.WriteLine();

        var graph = new Graph(parsedN, parsedP, 0);
        graph.FillRandomly();

        Console.WriteLine("Macierz grafu:");
        graph.Print();

        var degSequence = graph.CalculateDegSequence();
        Console.Write($"Ciąg stopni: ({string.Join(", ", degSequence)})\n");
        Console.WriteLine();

        graph.DisplayAllNeighbours();

        graph.DisplayAllEdges();
    }
}