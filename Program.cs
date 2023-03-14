double p;
int n;
int[,] A;
Random rand = new();

void printSquareMatrix()
{
    Console.Write("\n  ");
    for (int i = 0; i < n; i++)
    {
        Console.Write($"{i + 1} ");

    }
    Console.WriteLine();

    for (int i = 0; i < n; i++)
    {
        Console.Write($"{i + 1} ");

        for (int j = 0; j < n; j++)
        {
            Console.Write($"{A[i, j]} ");
        }
        Console.WriteLine();
    }
}

void fillMatrix(int value)
{
    for (int i = 0; i < n; i++)
    {
        for (int j = 0; j < n; j++)
        {
            A[i, j] = value;
        }
    }
}

void fillGraphMatrix()
{
    for (int i = 0; i < n; i++)
    {
        for (int j = i + 1; j < n; j++)
        {
            double a = rand.Next(0, 100);

            if (a < p * 100)
            {
                A[i, j] = 1;
                A[j, i] = 1;
            }
        }
    }
}

List<int> calculateDegSequence()
{
    List<int> result = new();

    for (int i = 0; i < n; i++)
    {
        int degI = 0;

        for (int j = 0; j < n; j++)
        {
            degI += A[i, j];
        }

        result.Add(degI);
    }

    return result.OrderByDescending(x => x).ToList();

}

void printDegSequence(List<int> degSequence)
{
    Console.WriteLine("\nCiąg stopni:");

    Console.Write("( ");
    degSequence
        .ForEach(x => Console.Write($"{x} "));
    Console.Write(")\n\n");
}

Console.Write("Podaj liczbę wierzchołków n = ");
n = Convert.ToInt32(Console.ReadLine());

A = new int[n, n];
fillMatrix(0);

Console.Write("Podaj prawdopodobieństwo p = ");
string str = Console.ReadLine();

if (str.Contains('.')) str = str.Replace('.', ',');
p = Convert.ToDouble(str);


fillGraphMatrix();

printSquareMatrix();

List<int> degSequence = calculateDegSequence();
printDegSequence(degSequence);

//Draw graph with nodes place on the circumference of a circle
//YOu can use code from GK to generate points for polygons

//Maybe add a way to drag nodes using mouse