var input = File.ReadLines(Path.Combine(Directory.GetCurrentDirectory(), "input", "day_01.txt"));
int password = 0;
int dialPosition = 50;
foreach (var item in input)
{
    if(item is null) continue;
    var dialPositionBeforeRotation = dialPosition;
    char direction = item[0];
    int fullDistance = int.Parse(item[1..]);
    int distance = fullDistance % 100;
    int rotations = fullDistance / 100;
    password += rotations;
    dialPosition += direction switch
    {
        'L' => -distance,
        'R' => distance,
        _ => 0
    };
    var init = dialPosition;
    if(dialPosition <= 0)
    {
        // when initial position is not zero, we crossed the dial
        if(dialPositionBeforeRotation != 0) password++;
        if(dialPosition < 0) dialPosition += 100;
    }
    else if(dialPosition > 99)
    {
         dialPosition -= 100;
         password++;
    }
}

Console.Write(password);