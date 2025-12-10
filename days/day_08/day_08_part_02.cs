var input = File.ReadLines(Path.Combine(Directory.GetCurrentDirectory(), "input", "day_08.txt"));

// create priorty queue of all items min at the top assign
List<Point> points = [];
PriorityQueue<CableConnection, double> connections = new();
int start = 0;
// O(n^2logn)
foreach(var line in input) // O(n)
{
    var point = Point.Create(line);
    if(points.Count - 1 < start)
    {
        points.Add(point);
    }
    for(int i = 0; i < points.Count - 1; i++) // O(nlogn)
    {
        var basePoint = points[i]; 
        connections.Enqueue(new(basePoint, point), basePoint.GetDistance(point));
    }
    start++;
}


int totalConnections = 1000;
Dictionary<Point, Point?> cableMapping = [];
Dictionary<Point, int> frequency = [];
HashSet<Point> connected = [];
long product = 1;

while(connections.Count > 0) // O(n^2logn)
{
    var (A, B) = connections.Dequeue();
    // cases
    // 1 - no pair found
    bool pointAisFound = cableMapping.TryGetValue(A, out var connectionForA);
    bool pointBisFound = cableMapping.TryGetValue(B, out var connectionForB);
    if(!pointAisFound && !pointBisFound)
    {
        cableMapping[A] = null;
        cableMapping[B] = A;
        frequency[A] = 2;
        product = A.X.Mult(B.X);
        connected.Add(A);
        connected.Add(B);
    }
    else if((pointAisFound && !pointBisFound) || (pointBisFound && !pointAisFound))
    {
        Point pointWhichIsFound = pointAisFound ? A : B;
        Point pointWhichIsNotFound = pointAisFound ? B : A;
        Point? pointFoundConnection = pointWhichIsFound;
        Point previousValue = pointWhichIsFound;

        while(pointFoundConnection is Point p)
        {
            previousValue = p;
            pointFoundConnection = cableMapping[p];
        }
        frequency[previousValue] += 1;
        cableMapping[pointWhichIsNotFound] = pointWhichIsFound;
        product = pointWhichIsNotFound.X.Mult(pointWhichIsFound.X);
        connected.Add(pointWhichIsNotFound);
    }
    else
    {
        Point? pointB = B;
        var previousBValue = B;

        
        Point? pointA = A;
        var previousAValue = B;

        while(pointA is Point p)
        {
            previousAValue = p;
            pointA = cableMapping[p];
        }

        while(pointB is Point p)
        {
            previousBValue = p;
            pointB = cableMapping[p];
        }

        if(previousAValue != previousBValue)
        {
            var bsValue = frequency[previousBValue];
            frequency[previousAValue] += bsValue ;
            cableMapping[previousBValue] = previousAValue;
            product = previousBValue.X.Mult(previousAValue.X);
            frequency.Remove(previousBValue);

        }
        else
        {
            product = A.X.Mult(B.X);
        }
    }

    if(connected.Count == totalConnections)
    {
        Console.WriteLine(product);
        break;
    }
}


record struct CableConnection(Point A, Point B);
record class HeapItem(Point Point, PriorityQueue<Point, double> Distances);
record struct Point(string X, string Y, string Z);

static class PointExtension
{
    extension(Point source)
    {
        public static Point Create(string line)
        {
            var points = line.Split(',');
            return new Point(points[0], points[1], points[2]);
        }

        public double GetDistance(Point point) => Math.Sqrt(
            source.X.GetAbsolute(point.X) + 
            source.Y.GetAbsolute(point.Y) +
            source.Z.GetAbsolute(point.Z)
        );
    }

    extension(string source)
    {
        private double GetAbsolute(string other) => Math.Pow(long.Parse(source) - long.Parse(other), 2);
        
        public long Mult(string other) => long.Parse(source) * long.Parse(other);
    }
}