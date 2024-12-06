var input = File.ReadAllLines("input.txt");

Guard? guard = null;
var map = new List<List<Tile>>();
for (int x = 0; x < input.Length; x++)
{
    var row = new List<Tile>();
    map.Add(row);

    var line = input[x];
    for (int y = 0; y < line.Length; y++)
    {
        var character = line[y];

        var tile = new Tile(character == '#');
        row.Add(tile);

        Direction? direction = character switch
        {
            '^' => Direction.Up,
            'v' => Direction.Down,
            '<' => Direction.Left,
            '>' => Direction.Right,
            _ => null
        };

        if (direction != null)
        {
            guard = new Guard(x, y, direction.Value);
        }
    }
}

if (guard == null)
{
    throw new InvalidOperationException("No guard found");
}

var currentLocation = map[guard.X][guard.Y];
while (true)
{
    currentLocation.Visit();

    var newGuard = guard with { };
    if (guard.Direction == Direction.Up)
    {
        newGuard = guard with { X = guard.X - 1 };
    }
    else if (guard.Direction == Direction.Down)
    {
        newGuard = guard with { X = guard.X + 1 };
    }
    else if (guard.Direction == Direction.Left)
    {
        newGuard = guard with { Y = guard.Y - 1 };
    }
    else if (guard.Direction == Direction.Right)
    {
        newGuard = guard with { Y = guard.Y + 1 };
    }

    if (newGuard.X < 0 || newGuard.X >= map.Count || newGuard.Y < 0 || newGuard.Y >= map[0].Count)
    {
        // exited the map
        break;
    }

    if (map[newGuard.X][newGuard.Y].IsWall)
    {
        guard = guard with
        {
            Direction = newGuard.Direction switch
            {
                Direction.Up => Direction.Right,
                Direction.Right => Direction.Down,
                Direction.Down => Direction.Left,
                Direction.Left => Direction.Up,
                _ => throw new InvalidOperationException("Invalid direction")
            }
        };
    }
    else
    {
        guard = newGuard;
        currentLocation = map[guard.X][guard.Y];
    }
}

//foreach (var row in map)
//{
//    foreach (var tile in row)
//    {
//        if (tile.Visited)
//        {
//            Console.Write("X");
//        }
//        else
//        {
//            Console.Write(tile.IsWall ? "#" : ".");
//        }
//    }
//    Console.WriteLine();
//}

Console.WriteLine("D6.1: " + map.Sum(row => row.Count(tile => tile.Visited)));

class Tile(bool isWall)
{
    public bool IsWall { get; } = isWall;
    public bool Visited { get; private set; }

    public void Visit()
    {
        if (IsWall)
        {
            throw new InvalidOperationException("Cannot visit a wall");
        }
        Visited = true;
    }
}

record Guard(int X, int Y, Direction Direction);
//class Guard(int x, int y, char direction)
//{
//    public int X { get; set; } = x;
//    public int Y { get; set; } = y;
//    public Direction Direction { get; set; } = direction switch
//    {
//        '^' => Direction.Up,
//        'v' => Direction.Down,
//        '<' => Direction.Left,
//        '>' => Direction.Right,
//        _ => throw new ArgumentException("Invalid direction")
//    };
//}

enum Direction
{
    Up,
    Down,
    Left,
    Right
}