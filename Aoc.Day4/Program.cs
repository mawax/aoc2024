

using System.Text.RegularExpressions;

var input = File.ReadAllLines("input.txt");

var size = new Size(input[0].Length, input.Length);

var total = 0;

// count XMAS in rows
foreach (var line in input)
{
    total += CountXmas(line);
}

// count XMAS in columns
for (var x = 0; x < size.Width; x++)
{
    var column = "";
    for (var y = 0; y < size.Height; y++)
    {
        column += input[y][x];
    }
    total += CountXmas(column);
}

total += CountDiagonals(input, size);

var flipped = new List<string>();
foreach (var line in input)
{
    flipped.Add(ReverseString(line));
}

total += CountDiagonals(flipped.ToArray(), size);

Console.WriteLine("D4.1: " + total);



static int CountXmas(string s)
{
    var regex = new Regex("XMAS");
    var amount = regex.Matches(s).Count;

    amount += regex.Matches(ReverseString(s)).Count;
    return amount;
}

static string ReverseString(string s)
{
    var array = s.ToCharArray();
    Array.Reverse(array);
    return new string(array);
}

static int CountDiagonals(string[] input, Size size)
{
    var total = 0;
    // count XMAS in diagonals
    for (var x = 0; x < size.Width; x++)
    {
        var diagonal = "";
        for (var y = 0; y < size.Height; y++)
        {
            if (x + y < size.Width)
            {
                diagonal += input[y][x + y];
            }
        }
        total += CountXmas(diagonal);
    }

    for (var y = 1; y < size.Height; y++)
    {
        var diagonal = "";
        for (var x = 0; x < size.Width; x++)
        {
            if (x + y < size.Height)
            {
                diagonal += input[y + x][x];
            }
        }
        total += CountXmas(diagonal);
    }

    return total;
}

record Size(int Width, int Height);