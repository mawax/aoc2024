using System.Text;

var input = File.ReadAllText("testinput.txt");

var index = 0;

var blocks = new List<BlockFile>();
for (var i = 0; i < input.Length; i++)
{
    var c = input[i];
    var number = Convert.ToInt32(c.ToString());
    if (i % 2 == 0)
    {
        blocks.Add(new BlockFile(index, number, false));

        index++;
    }
    else
    {
        blocks.Add(new BlockFile(null, number, true));
    }
}

var blocksArray = blocks.ToArray();
var sortedFiles = blocksArray.Skip(1).Where(x => !x.FreeSpace).OrderByDescending(x => x.Id).ToArray();
var sortedFilesIndex = 0;
for (var i = 0; i < blocks.Count; i++)
{
    if (blocksArray[i].FreeSpace)
    {
        if (sortedFilesIndex >= sortedFiles.Length)
        {
            break;
        }

        var nextFile = sortedFiles[sortedFilesIndex];
        while (nextFile.Blocks > blocksArray[i].Blocks && sortedFilesIndex < sortedFiles.Length - 1)
        {
            nextFile = sortedFiles[sortedFilesIndex];
            sortedFilesIndex++;
            continue;
        }

        var temp = blocksArray[i];
        var indexOfItem = Array.FindIndex(blocksArray, x => x == nextFile);
        blocksArray[i] = nextFile;
        blocksArray[indexOfItem] = temp;
        
        sortedFilesIndex++;
    }
}

long checksum = 0;

var newIndex = 0;
foreach (var block in blocksArray)
{
    if (!block.FreeSpace)
    {
        for (var j = 0; j < block.Blocks; j++)
        {
            checksum += (block.Id!.Value * newIndex) * block.Blocks;
            newIndex++;
        }
    }

    newIndex++;
}

Console.WriteLine("D9.2: " + checksum);

record BlockFile(int? Id, int Blocks, bool FreeSpace);