var input = File.ReadLines(Path.Combine(Directory.GetCurrentDirectory(), "input", "day_04.txt"));

var graph = input.Select(line => line.Select(character => character).ToArray()).ToArray();
int sum = 0;

// O(n*m)
for(int i = 0; i < graph.Length; i++) // O(n)
{
    int maxColumn = graph[i].Length;
    for(int j = 0; j < maxColumn; j++) // O(m)
    {
        if(graph[i][j] == '.') continue;
        if(HasFewerRolls(i, j, maxColumn) < 4) // O(1)
        {
            sum++;
        }
    }
}
Console.Write(sum);
int HasFewerRolls(int i, int j, int maxColumn)
{
    int maxRow = graph.Length;
    int localSum = 0;
    int leftIndex = j - 1;
    int rightIndex = j + 1;
    int topIndex = i - 1;
    int bottomIndex = i + 1;

    int ItemCount(int row, int column) => graph[row][column] == '.' ? 0 : 1;

    // top i - 1, j
    if(topIndex >= 0)
    {
        localSum += ItemCount(topIndex, j);
    }
    // right i, j + 1
    if(rightIndex < maxColumn)
    {
        localSum += ItemCount(i, rightIndex);
    }
    // bottom i + 1, j
    if(bottomIndex < maxRow)
    {
        localSum += ItemCount(bottomIndex, j);
    }
    // left i, j - 1
    if(leftIndex >= 0)
    {
        localSum += ItemCount(i, leftIndex);
    }
    // ---------------------------- //
    // top left i - 1, j - 1
    if(topIndex >= 0 && leftIndex >= 0)
    {
        localSum += ItemCount(topIndex, leftIndex);
    }
    // top right i - 1, j + 1
    if(topIndex >= 0 && rightIndex < maxColumn)
    {
        localSum += ItemCount(topIndex, rightIndex);
    }
    // bottom left i + 1, j - 1
    if(bottomIndex < maxRow && leftIndex >= 0)
    {
        localSum += ItemCount(bottomIndex, leftIndex);
    }
    // bottom right i + 1, j + 1
    if(bottomIndex < maxRow && rightIndex < maxColumn)
    {
        localSum += ItemCount(bottomIndex, rightIndex);
    }
    return localSum;
}