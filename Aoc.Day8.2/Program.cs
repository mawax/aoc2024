var input = File.ReadAllLines("input.txt");

var tiles = new List<Tile>();

for (int y = 0; y < input.Length; y++)
{
    for (int x = 0; x < input[y].Length; x++)
    {
        var tile = new Tile(new Point(x, y), input[y][x]);
        tiles.Add(tile);
    }
}

foreach (var tile in tiles)
{
    if (tile.Type == '.') continue;

    foreach (var node in tiles.Where(x => x.Type == tile.Type && x != tile))
    {
        tile.HasAntiNode = true;

        var distanceX = tile.Point.X - node.Point.X;
        var distanceY = tile.Point.Y - node.Point.Y;

        var multiplier = 1;
        while (true)
        {
            var antiNodeLocation = new Point(tile.Point.X + (distanceX * multiplier), tile.Point.Y + (distanceY * multiplier));

            if (InBounds(antiNodeLocation))
            {
                var antiNode = tiles.First(t => t.Point == antiNodeLocation);
                antiNode.HasAntiNode = true;
            }
            else
            {
                break;
            }

            multiplier++;
        }
    }
}

for (var y = 0; y < input.Length; y++)
{
    for (var x = 0; x < input[y].Length; x++)
    {
        var tile = tiles.First(t => t.Point == new Point(x, y));
        Console.Write(tile.HasAntiNode ? "#" : tile.Type);
    }
    Console.WriteLine();
}

Console.WriteLine("D8.1: " + tiles.Count(x => x.HasAntiNode));

//1006 too low

bool InBounds(Point point)
{
    return point.X >= 0 && point.X < input[0].Length && point.Y >= 0 && point.Y < input.Length;
}

record Point(int X, int Y);

class Tile(Point point, char Type)
{
    public bool HasAntiNode { get; set; }
    public Point Point { get; } = point;
    public char Type { get; } = Type;
}