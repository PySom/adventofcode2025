using System.Text;
var input = File.ReadLines(Path.Combine(Directory.GetCurrentDirectory(), "input", "day_06.txt"));

List<List<char>> numbers = [];
List<string> operations = [];

StringBuilder number = new(5);
foreach(var line in input)
{
    if(line.Contains("+") || line.Contains("*"))
    {
        var lineOperations = line.Split(' ');
        int start = 0;
        for(int i = lineOperations.Length - 1; i >= 0; i--)
        {
            var operation = lineOperations[i];
            if (string.IsNullOrEmpty(operation))
            {
                start++;
            }
            else
            {
                operations.Add($"{start}-{operation}");
                start = 0;
            }
        }
        
    }
    else
    {
        var values = line.Split(' ');

        int start = 0;
        for(int i = values.Length - 1; i >= 0; i--)
        {
            var value = values[i];
            if (string.IsNullOrEmpty(values[i]))
            {
                if(numbers.Count  - 1 < start)
                {
                    numbers.Add(['#']);
                }
                else
                {
                    numbers[start].Add('#');
                }
                start++;
            }
            else
            {
                for(int j = value.Length - 1; j >= 0; j--)
                {
                    if(numbers.Count  - 1 < start)
                    {
                        numbers.Add([value[j]]);
                    }
                    else
                    {
                        numbers[start].Add(value[j]);
                    }
                    start++;
                }
            }
            
        }
    }
    
}

int position = 0;
long total = 0;
StringBuilder sb = new(5);

foreach(var operation in operations)
{
    var operationPair = operation.Split('-');
    var totalIndices = int.Parse(operationPair[0]) + 1;
    long localSum = operationPair[1] == "*" ? 1 : 0;
    foreach(var values in numbers[position..(position + totalIndices)])
    {
        // build numbers

        foreach(var value in values)
        {
            if(value != '#') sb.Append(value);
        }
        var normalized = sb.ToString();
        var valueAsNum = string.IsNullOrEmpty(normalized) ? 0 : long.Parse(sb.ToString());
        localSum = operationPair[1] switch
        {
            "*" => localSum * valueAsNum,
            "" => localSum * valueAsNum,
            _ => localSum + valueAsNum
        };
        sb.Clear();
    }
    position += totalIndices;
    total += localSum;
}

Console.WriteLine(total);