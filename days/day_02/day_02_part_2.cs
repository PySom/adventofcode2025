using System.Text;
// find a way no to have all in memory
var input = File.ReadLines(Path.Combine(Directory.GetCurrentDirectory(), "input", "day_02.txt")).First().Split(',');
long sum = 0;
foreach (var item in input)
{
    var range = item.Split('-');
    var start = long.Parse(range[0]);
    var end = long.Parse(range[1]);

    // O(m * c^2)
    for(var i = start; i <= end; i++) // O(m) length of difference
    {
        var value = i.ToString();
        if(value.Length == 1) continue;
        int grouping = 1;
        // a naive approach to form pairs from 1 to length of integer (grouping)
        // if the grouping divides the number equally, store each grouping in a set
        // if at any point the total items in the set is 1 after adding all pairs to set
        // we have repeating sequences
        while (grouping < value.Length) // O(c) length of the integer
        {
            HashSet<string> repeating = [];
            if(value.Length % grouping == 0)
            {
                for(int j = 0; j < value.Length; j += grouping) // O(c) length of integer pairs worse case 1 
                {
                    var repeatingLetters = j + grouping >= value.Length ? value[j..] : value[j..(j+grouping)];
                    repeating.Add(repeatingLetters);
                }
            }
            if(repeating.Count == 1)
            {
                sum += i;
                break;
            }
            grouping++;
        }
    }
}

Console.Write(sum);