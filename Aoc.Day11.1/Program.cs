var input = File.ReadAllText("input.txt");

var stones = new LinkedList<long>(input.Split(' ').Select(long.Parse));

const int blinks = 25;
for (var i = 0; i < blinks; i++)
{
    var node = stones.First;
    while (node != null)
    {
        if (node.Value == 0)
        {
            node.Value = 1;

            node = node.Next;
            continue;
        }

        if (node.Value.ToString().Length % 2 == 0)
        {
            var nodeString = node.Value.ToString();
            var leftPart = nodeString[..(nodeString.Length / 2)];
            var rightPart = nodeString[(nodeString.Length / 2)..];

            node.Value = int.Parse(leftPart);
            var newNode = new LinkedListNode<long>(long.Parse(rightPart));
            stones.AddAfter(node, newNode);

            node = newNode.Next;
            continue;
        }

        node.Value *= 2024;

        node = node.Next;
    }

    //Console.WriteLine(string.Join(' ', stones));
}

Console.WriteLine("D11.1: " + stones.Count);