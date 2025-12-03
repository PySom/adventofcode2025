// find a way no to have all in memory
var input = File.ReadLines(Path.Combine(Directory.GetCurrentDirectory(), "input", "day_02.txt")).First().Split(',');
long sum = 0;
foreach (var item in input)
{
    var range = item.Split('-');
    var start = long.Parse(range[0]);
    var end = long.Parse(range[1]);

    for(var i = start; i <= end; i++)
    {
        var value = i.ToString();
        if(value.Length % 2 != 0) continue;
        int startIndex = 0;
        int midIndex = value.Length / 2;
        bool skip = true;

        while(startIndex < midIndex && midIndex < value.Length)
        {
            if(value[startIndex] != value[midIndex])
            {
                skip = true;
                break;
            }
            skip = false;
            startIndex++;
            midIndex++;
        }

        if (!skip)
        {
            sum += i;
        }
    }
}

Console.Write(sum);