var input = File.ReadLines(Path.Combine(Directory.GetCurrentDirectory(), "input", "day_01.txt"));
int password = 0;
int dialPosition = 50;

foreach (var item in input)
{
    if(item is null) continue;
    char direction = item[0];
    int distance = int.Parse(item[1..]) % 100;
    dialPosition += direction switch
    {
        'L' => -distance,
        'R' => distance,
        _ => 0
    };
    if(dialPosition < 0) dialPosition += 100;
    else if(dialPosition > 99) dialPosition -= 100;
    if(dialPosition == 0) password++;
}

Console.Write(password);