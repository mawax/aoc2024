var lines = File.ReadAllLines("input.txt");

var safeReports = 0;
foreach (var line in lines)
{
    var report = line.Split(' ').Select(int.Parse);
    if (IsReportSafeWithDampener(report))
    {
        safeReports++;
    }
    else
    {
        Console.WriteLine(line);
    }
}

Console.WriteLine(safeReports);


static bool IsReportSafeWithDampener(IEnumerable<int> report)
{
    var enumerable = report as int[] ?? report.ToArray();
    // for (var i = 0; i < enumerable.Count(); i++)
    // {
    //     var l = enumerable.ToList();
    //     l.RemoveAt(i);
    //     
    //     if(IsReportSafe(l, out _))
    //     {
    //         return true;
    //     }
    // }
    // return false;

    var isSafe = IsReportSafe(enumerable, out var index);
    if (isSafe)
    {
        return true;
    }

    var list = enumerable.ToList();
    list.RemoveAt(index!.Value);
    if (IsReportSafe(list, out _))
    {
        return true;
    }

    var list2 = enumerable.ToList();
    list2.RemoveAt(index!.Value - 1);
    if (IsReportSafe(list2, out _))
    {
        return true;
    }
    // edge case these 3 safe reports that are detected as ascending/descending but change
    // 69 71 69 66 63
    // 15 13 15 16 17 20 21 24
    // 12 14 13 10 9
    return IsReportSafe(enumerable.Skip(1), out _);
}

static bool IsReportSafe(IEnumerable<int> report, out int? index)
{
    int? previousLevel = null;
    bool? isReportIncreasing = null;

    var enumerable = report as int[] ?? report.ToArray();
    for (var i = 0; i < enumerable.Count(); i++)
    {
        var level = enumerable.ElementAt(i);

        if (previousLevel == null)
        {
            previousLevel = level;
            continue;
        }

        if (level == previousLevel)
        {
            index = i;
            return false;
        }

        var isLevelIncreasing = previousLevel.Value < level;
        isReportIncreasing ??= isLevelIncreasing;

        if (isLevelIncreasing != isReportIncreasing)
        {
            index = i;
            return false;
        }

        var difference = Math.Abs(level - previousLevel.Value);
        if (difference is < 1 or > 3)
        {
            index = i;
            return false;
        }

        previousLevel = level;
    }

    index = null;
    return true;
}