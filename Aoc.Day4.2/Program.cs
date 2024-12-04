

var input = File.ReadAllLines("input.txt");

var size = new Size(input[0].Length, input.Length);

var total = 0;
for (int x = 1; x < size.Width - 1; x++)
{
    for (int y = 1; y < size.Height - 1; y++)
    {
        if (IsXmas(x, y))
        {
            total++;
        }
    }
}

Console.WriteLine("D4.2: " + total);

bool IsXmas(int x, int y)
{
    if (input[x][y] != 'A')
    {
        return false;
    }

    var topLeft = input[x - 1][y - 1];
    var topRight = input[x + 1][y - 1];
    var bottomLeft = input[x - 1][y + 1];
    var bottomRight = input[x + 1][y + 1];

    return IsMorS(topLeft, bottomRight) && IsMorS(bottomLeft, topRight);
}

bool IsMorS(char a, char b)
{
    return (a == 'M' && b == 'S') || (a == 'S' && b == 'M');

}

record Size(int Width, int Height);