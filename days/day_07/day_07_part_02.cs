var input = File.ReadLines(Path.Combine(Directory.GetCurrentDirectory(), "input", "day_07.txt"));

HashSet<int> visited = [];
Queue<int> neighbours = [];

var first = input.First();
long[] total = [..Enumerable.Repeat(0, first.Count())];
var indexOfFirstTachyon = first.IndexOf('S');
visited.Add(indexOfFirstTachyon);
total[indexOfFirstTachyon] = 1;


static int[] ComputeNeighbours(char character, int index) => character switch
{
    '^' => [index - 1, index + 1],
    _ => [index]
};

foreach (var neighbour in ComputeNeighbours('S', indexOfFirstTachyon))
{
    neighbours.Enqueue(neighbour);
    
}

// O(n * m) like in first part

foreach (var item in input.Skip(1))
{
    HashSet<int> nextNeighbours = [];
    // for all previous neighbours compute it's next
    while(neighbours.Count > 0)
    {
        var beamPosition = neighbours.Dequeue();
        visited.Add(beamPosition);
        long totalAtPosition = total[beamPosition];
        // if next neighbour is split, split, otherwise continue on that path
        var nextPositions = ComputeNeighbours(item[beamPosition], beamPosition);
        // split
        if(nextPositions.Length == 2)
        {
            total[nextPositions[0]] += totalAtPosition;
            total[nextPositions[1]] += totalAtPosition;
            total[beamPosition] = 0;
            nextNeighbours.Add(nextPositions[0]);
            nextNeighbours.Add(nextPositions[1]);

        }
        else
        { 
            total[nextPositions[0]] = totalAtPosition;
            nextNeighbours.Add(nextPositions[0]);
        }
    }
    foreach (var neigbour in nextNeighbours)
    {
        neighbours.Enqueue(neigbour);
    }
}

Console.WriteLine(total.Sum());