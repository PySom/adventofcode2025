var input = File.ReadLines(Path.Combine(Directory.GetCurrentDirectory(), "input", "day_03.txt"));

var sum = 0;
// O(n * m)
foreach(var number in input) // O(n)
{
    int firstMax = 0;
    int secondMax = 0;
    for(int i = 0; i < number.Length - 1; i++) // O(m) length of number
    {
        var j = i + 1;
        var currentNumber = number[i] - '0';
        var nextNumber = number[j] - '0';
        if(currentNumber > firstMax)
        {
            firstMax = currentNumber;
            secondMax = nextNumber;
        }
        else
        {
            secondMax = secondMax > nextNumber ? secondMax : nextNumber;
        }
        
    }

    sum += firstMax * 10 + secondMax;
}

Console.Write(sum);