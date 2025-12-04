var input = File.ReadLines(Path.Combine(Directory.GetCurrentDirectory(), "input", "day_03.txt"));
int total = 12;
long sum = 0;
// the idea of this algorithm is to create an array of size k - total size
// fill them up with each pass start from zero, for each pass compare with similar index
// O(n * m)
foreach(var number in input) // O(n)
{
    if(total > number.Length) continue;
    long localSum = 0;
    int[] max = [..Enumerable.Repeat(0, total)];
    for(int i = 0; i < number.Length - (total - 1); i++) // O(m -k) = O(m)
    {
        long currentLocalSum = 0;
        bool previousChanged = false;
        for(int j = 0; j < max.Length; j++) // O(k)
        {
            int currentNumberIndex = i + j;
            int currentMax = max[j];

            var currentNumber = number[currentNumberIndex] - '0';
            if (previousChanged)
            {
                max[j] = currentNumber;
            }
            else
            {
                if(currentNumber > currentMax)
                {
                    max[j] = currentNumber;
                    previousChanged = true;
                }
                else
                {
                    previousChanged = false;
                }
            }
            currentLocalSum += max[j] * (long)Math.Pow(10, total - 1 - j);
        }
        localSum = currentLocalSum;
        currentLocalSum = 0;
        
    }
    
    sum += localSum;
}

Console.Write(sum);