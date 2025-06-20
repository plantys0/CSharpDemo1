static bool CanMake33(int x, int y)
{
    return (x == 33 || y == 33 || x + y == 33 || x - y == 33 || y - x == 33 || x / y == 33 || x * y == 33);
}

    Console.WriteLine(CanMake33(22, 11)); // Output: false
