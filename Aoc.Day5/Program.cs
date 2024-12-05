
var input = File.ReadAllLines("input.txt");

var rules = new List<OrderRule>();
var processingUpdated = false;

var total = 0;
foreach (var line in input)
{
    if (line == "")
    {
        processingUpdated = true;
        continue;
    }

    if (!processingUpdated)
    {
        var parts = line.Split("|");
        rules.Add(new OrderRule(int.Parse(parts[0]), int.Parse(parts[1])));
    }
    else
    {
        total += CalculateCorrectOrder(line, rules);
    }
}

Console.WriteLine("D5.1: " + total);

static int CalculateCorrectOrder(string update, List<OrderRule> rules)
{
    var updateNumbers = update.Split(",").Select(int.Parse).ToList();

    foreach (var rule in rules)
    {
        var indexA = updateNumbers.IndexOf(rule.A);
        var indexB = updateNumbers.IndexOf(rule.B);

        if (indexA == -1 || indexB == -1)
        {
            continue;
        }

        if (indexA > indexB)
        {
            return 0;
        }
    }

    var middleIndex = (updateNumbers.Count - 1) / 2;
    return updateNumbers[middleIndex];
}

record OrderRule(int A, int B);