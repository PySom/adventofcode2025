var input = File.ReadLines(Path.Combine(Directory.GetCurrentDirectory(), "input", "day_06.txt"));

List<List<long>> numbers = [];

// read last even though it enumerates
string[] operations = input.Last().Split(' ', StringSplitOptions.RemoveEmptyEntries).ToArray(); // O(n)

// O(n * k)
// populate array
List<long> total = [];
foreach (var operation in operations) // O(k) amount of operations on a line
{
    if(operation == "+")
    {
        total.Add(0);
    }
    else
    {
        total.Add(1);
    }
}
foreach(var line in input) // O(n)
{
    if(line.StartsWith("+") || line.StartsWith("*")) continue;
    var values = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
    for(int i = 0; i < values.Length; i++) // O(k)
    {
        var value = values[i];
        var operation = operations[i];
        total[i] = operation switch
        {
            "*" => total[i] * long.Parse(value),
            "" => total[i] * long.Parse(value),
            _ => total[i] + long.Parse(value)
        };
    }
}

Console.WriteLine(total.Sum());