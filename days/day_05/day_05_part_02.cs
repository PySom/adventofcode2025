var input = File.ReadLines(Path.Combine(Directory.GetCurrentDirectory(), "input", "day_05.txt"));

List<long[]> validIds = [];

bool isIngredientId = false;
var bsCompare = Comparer<long[]>.Create((a, b) =>
{
    if(b[0] >= a[0] && b[0] <= a[1]) return 0;
    if (b[0] < a[0]) return 1;
    return -1;
});

foreach(var line in input)
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
        var c = Comparer<long[]>.Create((a, b) =>
        {
            if (a[0] < b[0]) return -1;
            if (a[0] ==  b[0] && a[1] < b[1]) return -1;
            if (a[0] > b[0]) return 1;
            if (a[0] == b[0] && a[1] > b[1]) return 1;
            return 0;
        });
        validIds.Sort(c); // O(nlogn) <- dominate
        
        // subsume
        var previousId = validIds[0];
        long totalSum = previousId[1] - previousId[0] + 1; 

        for(int i = 1; i < validIds.Count; i++) // O(n)
        {
            var currentId = validIds[i];
            if(currentId[0] == previousId[0])
            {
                long totalBefore = previousId[1] - previousId[0] + 1;
                // remove previous sum
                totalSum -= totalBefore;
                long totalAfter = currentId[1] - currentId[0] + 1;
                // use the current sum
                totalSum += totalAfter;
                previousId[1] = currentId[1];
            }
            else if(currentId[0] <= previousId[1])
            {
                if(currentId[1] <= previousId[1]) continue;
                // remove previous sum
                long totalBefore = previousId[1] - previousId[0] + 1;
                totalSum -= totalBefore;
                // use the current sum
                long totalAfter = currentId[1] - previousId[0] + 1;
                totalSum += totalAfter;
                
                previousId[1] = currentId[1];
            }
            else
            {     
                // use the current sum
                totalSum += currentId[1] - currentId[0] + 1;           

                previousId = currentId;
            }
        }
        Console.WriteLine(totalSum);
        break;
    }
}
