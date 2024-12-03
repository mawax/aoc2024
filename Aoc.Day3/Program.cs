using System.Text.RegularExpressions;

var input = File.ReadAllText("input.txt");

var dontRegex = new Regex(@"don't\(\).*?(do\(\)|$)", RegexOptions.Singleline);
var dontMatches = dontRegex.Matches(input);

var processedInput = input;
foreach (Match match in dontMatches.Reverse())
{
    var toRemove = processedInput.Substring(match.Index, match.Length);
    Console.WriteLine(toRemove);
    processedInput = processedInput.Remove(match.Index, match.Length);
}

var regex = new Regex(@"mul\((?<a>\d+),(?<b>\d+)\)");
var matches = regex.Matches(processedInput);

var total = 0;
foreach (Match match in matches)
{
    total += int.Parse(match.Groups["a"].Value) * int.Parse(match.Groups["b"].Value);
}

Console.WriteLine("d3.2: " + total);