var input = File.ReadLines(Path.Combine(Directory.GetCurrentDirectory(), "input", "day_07.txt"));

HashSet<int> visited = [];
Queue<int> neighbours = [];
int totalSplits = 0;

var indexOfFirstTachyon = input.First().IndexOf('S'); // O(m)
visited.Add(indexOfFirstTachyon);

static int[] ComputeNeighbours(char character, int index) => character switch
{
    '^' => [index - 1, index + 1],
    _ => [index]
};

foreach (var neighbour in ComputeNeighbours('S', indexOfFirstTachyon))
{
    neighbours.Enqueue(neighbour);
    
}

// O(n*m)

foreach (var item in input.Skip(1)) // O(n)
{
    HashSet<int> nextNeighbours = [];
    while(neighbours.Count > 0) // O(m)
    {
        var beamPosition = neighbours.Dequeue();
        visited.Add(beamPosition);
        // get next neigbour
        var nextPositions = ComputeNeighbours(item[beamPosition], beamPosition); // O(2)
        if(nextPositions.Length == 2)
        {
            totalSplits++;
            nextNeighbours.Add(nextPositions[0]);
            nextNeighbours.Add(nextPositions[1]);
        }
        else
        { 
            nextNeighbours.Add(nextPositions[0]);
        }
    }
    foreach (var neigbour in nextNeighbours) // O(m)
    {
        neighbours.Enqueue(neigbour);
    }
}

Console.WriteLine(totalSplits);