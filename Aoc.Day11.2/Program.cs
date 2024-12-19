var input = File.ReadAllText("input.txt");

var stones = new LinkedList<long>(input.Split(' ').Select(long.Parse));

var counts = new Dictionary<string, long>();
const int blinks = 75; 
long total = 0;
foreach (var stone in stones)
{
   total += CountStones(stone, 0); 
   Console.WriteLine($"Processed {stone}");
}

Console.WriteLine("D11.2: " + total);

long CountStones(long stone, int depth)
{
    while (true)
    {
        if (depth == blinks)
        {
            return 1;
        }

        depth++;

        if (counts.ContainsKey($"{stone}|{depth}"))
        {
            return counts[$"{stone}|{depth}"];
        }

        if (stone == 0)
        {
            stone = 1;
            continue;
        }

        if (stone.ToString().Length % 2 == 0)
        {
            var nodeString = stone.ToString();
            var leftPart = nodeString[..(nodeString.Length / 2)];
            var rightPart = nodeString[(nodeString.Length / 2)..];

            var count = CountStones(long.Parse(leftPart), depth) + CountStones(long.Parse(rightPart), depth);
            counts.TryAdd($"{stone}|{depth}", count);
            return count;
        }

        stone = stone * 2024;
    }
}

