var input = File.ReadAllLines("input.txt");

var list1 = new List<int>();
var list2 = new List<int>();
foreach (var line in input)
{
    var listItems = line.Split("   ");
    list1.Add(Convert.ToInt32(listItems[0]));
    list2.Add(Convert.ToInt32(listItems[1]));
}

Day1Part1(list1.ToList(), list2.ToList());
Day1Part2(list1.ToList(), list2.ToList());

return;

static void Day1Part2(List<int> list1, List<int> list2)
{
    var similarityScore = 0;

    foreach (var item in list1)
    {
        var occurrences = list2.Count(x => x == item);
        similarityScore += item * occurrences;
    }
    
    Console.WriteLine(similarityScore);
}

static void Day1Part1(List<int> list1, List<int> list2)
{
    list1.Sort();
    list2.Sort();

    var difference = list1
        .Select((t, i) => Math.Abs(t - list2[i]))
        .ToList();

    Console.WriteLine(difference.Sum());
    // 2378066
}