using System.Text;

var input = File.ReadAllText("input.txt");

var id = 0;

var blocks = new List<BlockFile>();
for (var i = 0; i < input.Length; i++)
{
    var c = input[i];
    var blockCount = Convert.ToInt32(c.ToString());

    if (blockCount == 0)
        continue;

    if (i % 2 == 0)
    {
        blocks.Add(new BlockFile(id, blockCount, false));

        id++;
    }
    else
    {
        blocks.Add(new BlockFile(null, blockCount, true));
    }
}

var blocksArray = blocks.ToArray();

var sortedFiles = blocksArray.Where(x => !x.FreeSpace)
    .OrderByDescending(x => x.Id).ToList();

while (sortedFiles.Count > 0)
{
    PrintBlocks(blocksArray);

    var currentFile = sortedFiles[0];
    var currentFileIndex = Array.FindIndex(blocksArray, x => x == currentFile);

    for (var j = 0; j < blocksArray.Length; j++)
    {
        if (currentFileIndex <= j)
        {
            break;
        }

        if (blocksArray[j].FreeSpace && blocksArray[j].Blocks >= currentFile.Blocks)
        {
            //Console.WriteLine($"Moving {currentFile.Id} to {j}, {currentFile.Blocks} blocks");
            var freeSpace = blocksArray[j];
            blocksArray[j] = currentFile;
            blocksArray[currentFileIndex] = freeSpace;

            var lengthDif = freeSpace.Blocks - currentFile.Blocks;
            if (lengthDif > 0)
            {
                //Console.WriteLine($"Splitting freespace into {currentFile.Blocks} and {lengthDif}");

                blocksArray[currentFileIndex] = freeSpace with { Blocks = currentFile.Blocks };
                var tempList = blocksArray.ToList();
                tempList.Insert(j + 1, new BlockFile(null, lengthDif, true));
                blocksArray = tempList.ToArray();
            }

            break;
        }
    }

    sortedFiles.RemoveAt(0);
}

PrintBlocks(blocksArray);

long checksum = 0;

long fileIndex = 0;
foreach (var file in blocksArray)
{
    for (var block = 0; block < file.Blocks; block++)
    {
        if (!file.FreeSpace)
        {
            checksum += file.Id!.Value * (fileIndex);
        }

        fileIndex++;
    }
}

Console.WriteLine("D9.2: " + checksum);


static void PrintBlocks(BlockFile[] blocksArray)
{
    //Console.WriteLine(BlocksToString(blocksArray));
}

static string BlocksToString(BlockFile[] blocksArray)
{
    var sb = new StringBuilder();
    foreach (var block in blocksArray)
    {
        for (var i = 0; i < block.Blocks; i++)
        {
            if (block.FreeSpace)
            {
                sb.Append('.');
            }
            else
            {
                sb.Append(block.Id);
            }
        }
    }

    return (sb.ToString());
}

record BlockFile(long? Id, long Blocks, bool FreeSpace);