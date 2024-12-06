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

        var tile = new Tile(character == '#', x, y);
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

if (!CanExit(guard with { }, map))
{
    throw new InvalidOperationException("Guard is trapped");
}

var obstructionOptions = 0;
foreach (var obstructionOption in map.SelectMany(row => row).Where(tile => tile.Visited))
{
    Console.WriteLine($"Obstructing {obstructionOption.X}, {obstructionOption.Y}");

    var clonedMap = map
        .Select(row =>
            row.Select(tile =>
                new Tile(tile.IsWall, tile.X, tile.Y) { IsObstacle = obstructionOption.X == tile.X && obstructionOption.Y == tile.Y }
            )
            .ToList()
        ).ToList();

    if (!CanExit(guard with { }, clonedMap))
    {
        Console.WriteLine("Obstruction is valid");
        obstructionOptions++;
    }
    else
    {
        Console.WriteLine("Obstruction is invalid");
    }
}

Console.WriteLine("D6.2: " + obstructionOptions);
static bool CanExit(Guard guard, List<List<Tile>> map)
{
    var currentLocation = map[guard.X][guard.Y];
    var steps = 0;

    var maxSteps = map.Count * map[0].Count * 10;
    while (steps < maxSteps)
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
            return true;
        }

        if (map[newGuard.X][newGuard.Y].IsBlocked)
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

        steps++;
    }

    return false;
}

class Tile(bool isWall, int X, int Y)
{
    public bool IsWall { get; } = isWall;
    public int X { get; } = X;
    public int Y { get; } = Y;

    public bool Visited { get; private set; }
    public bool IsObstacle { get; set; }

    public void Visit()
    {
        if (IsWall)
        {
            throw new InvalidOperationException("Cannot visit a wall");
        }
        Visited = true;
    }

    public bool IsBlocked => IsWall || IsObstacle;
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