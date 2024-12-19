using System.IO.Enumeration;

var input = File.ReadAllLines("input.txt");

var isD2 = true;

var total = 0;
for (var y = 0; y < input.Length; y++)
{
    for (var x = 0; x < input[y].Length; x++)
    {
        var c = input[y][x];
        if (c == '0')
        {
            total += CountTrailHeads([], [], x, y);
        }
    }
}

Console.WriteLine("D10.1: " + total);

int CountTrailHeads(List<Point> foundTrailHeads, List<Point> steps, int x, int y)
{
    var subTotal = 0;

    var number = ToInt(input[y][x]);
    var point = new Point(x, y, number);
    steps.Add(point);
    if (number == 9)
    {
        if (!isD2 && foundTrailHeads.Contains(point))
        {
            return 0;
        }

        foundTrailHeads.Add(point);

        //PrintTrailHeads(x, y, steps);

        return 1;
    }

    subTotal += IsValidIncrement(number, input, x - 1, y) ? CountTrailHeads(foundTrailHeads, steps.ToList(), x - 1, y) : 0;
    subTotal += IsValidIncrement(number, input, x + 1, y) ? CountTrailHeads(foundTrailHeads, steps.ToList(), x + 1, y) : 0;
    subTotal += IsValidIncrement(number, input, x, y - 1) ? CountTrailHeads(foundTrailHeads, steps.ToList(), x, y - 1) : 0;
    subTotal += IsValidIncrement(number, input, x, y + 1) ? CountTrailHeads(foundTrailHeads, steps.ToList(), x, y + 1) : 0;

    return subTotal;
}

void PrintTrailHeads(int x, int y, List<Point> points)
{
    Console.WriteLine($"Found trail head at {x}, {y}");
    // print grid
    for (var y1 = 0; y1 < input.Length; y1++)
    {
        for (var x1 = 0; x1 < input[y1].Length; x1++)
        {
            var step = points.FirstOrDefault(s => s.X == x1 && s.Y == y1);
            if (step != null)
            {
                Console.Write(step.Value);
            }
            else
            {
                Console.Write('.');
            }
        }

        Console.WriteLine();
    }
}

static bool IsValidIncrement(int currentValue, string[] input, int x, int y)
{
    if (!InBounds(input, x, y))
    {
        return false;
    }

    var number = ToInt(input[y][x]);
    return currentValue + 1 == number;
}

static int ToInt(char c)
{
    if (c == '.')
    {
        return -1;
    }

    return Convert.ToInt32(c.ToString());
}

static bool InBounds(string[] input, int x, int y)
{
    return x >= 0 && x < input[0].Length && y >= 0 && y < input.Length;
}

record Point(int X, int Y, int Value);