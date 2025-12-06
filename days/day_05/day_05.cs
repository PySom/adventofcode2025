var input = File.ReadLines(Path.Combine(Directory.GetCurrentDirectory(), "input", "day_05.txt"));

int sum = 0;
List<long[]> validIds = [];

bool isIngredientId = false;
bool isOrdered = false;
var bsCompare = Comparer<long[]>.Create((a, b) =>
{
    if(b[0] >= a[0] && b[0] <= a[1]) return 0;
    if (b[0] < a[0]) return 1;
    return -1;
});

// O(n^2logn)
foreach(var line in input) // O(n)
{
    if (string.IsNullOrWhiteSpace(line))
    {
        isIngredientId = true;
        continue;
    }
    if (!isIngredientId)
    {
        validIds.Add(line.Split('-').Select(number => long.Parse(number)).ToArray());
    }
    else
    {
        if(!isOrdered)
        {
            var c = Comparer<long[]>.Create((a, b) =>
            {
                if (a[0] < b[0]) return -1;
                if (a[0] ==  b[0] && a[1] < b[1]) return -1;
                if (a[0] > b[0]) return 1;
                if (a[0] == b[0] && a[1] > b[1]) return 1;
                return 0;
            });
            validIds.Sort(c); //O(nlogn) <- dominate
            Console.WriteLine($"{string.Join(',', validIds.Select(c => string.Join('-', c)))}");
            isOrdered = true;
        }

        long id = long.Parse(line);

        if (validIds.BinarySearch([id], bsCompare) >= 0) // O(logn)
        {
            sum++;
        }
    }
}

Console.Write(sum);
