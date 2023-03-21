using GraphMatrx_Fill_Test;

Console.WriteLine("Podaj liczbę wierzchołków n oraz prawdopodobieństwo p, aby wygenerować losowy graf:");

Console.Write("n = ");
var n = Console.ReadLine();

Console.Write("p = ");
var p = Console.ReadLine();

p = p.Replace('.', ',');

Console.WriteLine();

if (!int.TryParse(n, out var parsedN) || !double.TryParse(p, out var parsedP))
{
    Console.WriteLine("Podano niepoprawne dane");
    return;
}

var graph = new Graph(parsedN, parsedP, 0);
graph.FillRandomly();

Console.WriteLine("Graf:");
graph.Print();

var degSequence = graph.CalculateDegSequence();
Console.Write($"Ciąg stopni: ({string.Join(", ", degSequence)})");

//Draw graph with nodes place on the circumference of a circle
//YOu can use code from GK to generate points for polygons

//Maybe add a way to drag nodes using mouse