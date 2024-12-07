using System.ComponentModel;
using System.Text;


var input = File.ReadAllLines("input.txt");

var sum = 0L;
foreach (var line in input)
{
    var parts = line.Split((": "));
    var testValue = long.Parse(parts[0]);
    var numbers = parts[1].Split(' ').Select(int.Parse).ToArray();

    if (TestLine(testValue, numbers))
    {
        sum += testValue;
    }
}

Console.WriteLine("D7.1: " + sum);

bool TestLine(long testValue, int[] numbers)
{
    var operatorCount = numbers.Length - 1;
    var possibilities = Convert.ToInt32(Math.Pow(2, operatorCount));

    for (var i = 0; i < possibilities; i++)
    {
        //Console.WriteLine(i);
        var stringRepresentation = Convert.ToString(i, 2);
        //Console.WriteLine(stringRepresentation);
        var combination = stringRepresentation.PadLeft(operatorCount, '0');
        //Console.WriteLine(combination);
        combination = combination.Replace('0', '+').Replace('1', '*');
        //Console.WriteLine(combination);
        
        long total = numbers[0];
        for (var j = 1; j < numbers.Length; j++)
        {
            var number = numbers[j];
            total = Calculate(total, number, combination[j - 1]);
        }

        PrintPossibility(total, testValue, combination, numbers);

        if (total == testValue)
        {
            Console.WriteLine("Found!");
            return true;
        }
    }

    return false;
}

void PrintPossibility(long total, long testValue1, string combination, int[] ints)
{
    var sb = new StringBuilder();
    sb.Append(testValue1);
    sb.Append(": ");

    for (var i = 0; i < ints.Length; i++)
    {
        sb.Append(ints[i]);

        if (i == ints.Length - 1)
        {
            break;
        }

        sb.Append($" {combination[i]} ");
    }

    sb.Append($" = {total}");

    Console.WriteLine(sb.ToString());
}

long Calculate(long a, long b, char @operator) =>
    @operator switch
    {
        '+' => a + b,
        '*' => a * b,
        _ => throw new InvalidEnumArgumentException()
    };