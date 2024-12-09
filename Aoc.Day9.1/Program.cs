var input = File.ReadAllText("input.txt");

var index = 0;

var blocks = new List<Block>();
for (var i = 0; i < input.Length; i++)
{
    var c = input[i];
    var number = Convert.ToInt32(c.ToString());
    if (i % 2 == 0)
    {
        for (var x = 0; x < number; x++)
        {
            blocks.Add(new Block(index, false));
        }

        index++;
    }
    else
    {
        for (var x = 0; x < number; x++)
        {
            blocks.Add(new Block(null, true));
        }
    }
}

var blocksArray = blocks.ToArray();
var last = blocks.Count - 1;
for (var i = 0; i < blocks.Count; i++)
{
    if (blocksArray[i].FreeSpace)
    {
        while (blocksArray[last].FreeSpace)
        {
            last--;
        }

        if (last <= i)
            break;

        (blocksArray[i], blocksArray[last]) = (blocksArray[last], blocksArray[i]);
        last--;
    }
}

long checksum = 0;
for (var i = 0; i < blocksArray.Length; i++)
{
    var block = blocksArray[i];
    if (!block.FreeSpace)
    {
        ;
        checksum += block.Id!.Value * i;
    }
}

Console.WriteLine("D9.1: " + checksum);

record Block(int? Id, bool FreeSpace);