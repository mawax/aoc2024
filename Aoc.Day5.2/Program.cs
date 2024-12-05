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
        if (!IsCorrectOrder(line, rules))
        {
            // update is not in correct order, re-order it
            total += ReorderAndCalculate(line, rules);
        }
    }
}

Console.WriteLine("D5.2: " + total);

static int ReorderAndCalculate(string update, List<OrderRule> rules)
{
    var updateNumbers = update.Split(",").Select(int.Parse).ToList();

    while (!IsCorrectOrder(string.Join(",", updateNumbers), rules))
    {
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
                var temp = updateNumbers[indexA];
                updateNumbers[indexA] = updateNumbers[indexB];
                updateNumbers[indexB] = temp;
            }
        }
    }

    var middleIndex = (updateNumbers.Count - 1) / 2;
    return updateNumbers[middleIndex];
}

static bool IsCorrectOrder(string update, List<OrderRule> rules)
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
            return false;
        }
    }

    return true;
}

record OrderRule(int A, int B);
