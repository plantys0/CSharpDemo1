using System.Collections;
using System.Reflection.Metadata.Ecma335;
using System.Text;
int first = 2;
string second = "4";
int result = first;
Console.WriteLine(result);
string result1 = first + second;
Console.WriteLine(result1);










/*
Console.WriteLine("");
Console.WriteLine("Floating point types:");
Console.WriteLine($"float  : {float.MinValue} to {float.MaxValue} (with ~6-9 digits of precision)");
Console.WriteLine($"double : {double.MinValue} to {double.MaxValue} (with ~15-17 digits of precision)");
Console.WriteLine($"decimal: {decimal.MinValue} to {decimal.MaxValue} (with 28-29 digits of precision)");
Console.WriteLine("Signed integral types:");
Console.WriteLine($"sbyte  : {sbyte.MinValue} to {sbyte.MaxValue}");
Console.WriteLine($"short  : {short.MinValue} to {short.MaxValue}");
Console.WriteLine($"int    : {int.MinValue} to {int.MaxValue}");
Console.WriteLine($"long   : {long.MinValue} to {long.MaxValue}");


string[] validRoles = { "administrator", "manager", "user" };
Console.WriteLine("Enter Administrator, Manager, or User");
while (true)
{
    string inputRole = Console.ReadLine().ToLower().Trim();
    if (Array.Exists(validRoles, role => role == inputRole))
    {
        Console.WriteLine("role has been accepted");
        break;
    }
    Console.WriteLine("enter a valid role");
}



Console.WriteLine("Enter an integer between 5 and 10");
int inputInteger = 0;
bool isValid = false;
bool allGood = false;
do
{
    string readInput = Console.ReadLine();
    isValid = int.TryParse(readInput, out inputInteger);
    if (isValid)
    {
        if (inputInteger is < 5 or > 10) Console.WriteLine("make sure to enter a number between 5 and 10");
        else allGood = true;
    }
    else Console.WriteLine("input needs to be a number");
} while (!allGood);

Console.WriteLine($"input ({inputInteger}) has been accepted");

 
int[] points = new int[] { 10, 10 };//0=Hero, 1=monster
int lostPoints = 0;
Random random = new Random();
bool attackMonster = true;
do
{
    lostPoints = random.Next(0, 10);
    if (attackMonster) points[1] = points[1] - lostPoints;
    else points[0] = points[0] - lostPoints;

    Console.WriteLine($"{((attackMonster) ? "Monster" : "Hero")} was damaged and lost {lostPoints} health and now has {((attackMonster) ? points[0] : points[1])} health.");
    attackMonster = !attackMonster;

} while (points[0] > 0 || points[1] > 0);

Random random9 = new Random();
int current = 0;

do
{
    current = random9.Next(1, 11);
    Console.WriteLine(current);
} while (current != 7);

string name8 = "";
for (int i=1;  i<=100; i++)
{
    if ((i % 3 == 0) && (i % 5 == 0)) name8 = "FizzBuzz";
    else if (i % 3 == 0) name8 = "Fizz";
    else if (i % 5 == 0) name8 = "Buzz";
    else name8 = "";
    Console.WriteLine($"{i} - {name8}");
}


string[] names = { "Alex", "Eddie", "David", "Michael" };
for (int i = 0; i < names.Length; i++)
    if (names[i] == "David") names[i] = "Sammy";

foreach (var name9 in names) Console.WriteLine(name9);


for (int i = 0; i <10; i +=3)
{
    Console.WriteLine(i);
    if (i > 5) break;
}

int employeeLevel = 200;
string employeeName = "John Smith";

string title = "";

switch (employeeLevel)
{
    case 100:
    case 200:
        title = "Senior Associate";
        break;
    default:
        title = "Associate";
        break;
}

Console.WriteLine($"{employeeName}, {title}");



string permission = "Admin|Manager";
int level = 55;

if (permission.Contains("Admin") && level > 55)
    Console.WriteLine("Welcome, Super Admin user.");
else if (permission.Contains("Admin") && level <= 55)
    Console.WriteLine("Welcome, Admin user.");
else if (permission.Contains("Manager") && level > 20)
    Console.WriteLine("Contact an Admin for access.");
else if (!permission.Contains("Manager") && !permission.Contains("Admin"))
    Console.WriteLine("You do not have sufficient privileges.");

    Random random8 = new Random();
int myRandom = random8.Next(1, 100);

Console.WriteLine($"The output is: {(random.Next(0,1) ==0 ? "Heads" : "Tails")} ");
Console.WriteLine($"The output is: {(myRandom > 50 ? "Heads" : "Tails")} ");
string result = myRandom >50 ? "Heads" : "Tails";
Console.WriteLine($"The output is: {result} ");


int saleAmount = 999;
int discount = saleAmount > 1000 ? 100 : 50;
Console.WriteLine($"Discount: {discount}");
Console.WriteLine($"Discount: {(saleAmount > 1000 ? 100 : 50)}");

string pangram = "The quick brown fox jumps over the lazy dog.";
Console.WriteLine(pangram.Contains("fox"));
Console.WriteLine(pangram.Contains("cow"));
Console.WriteLine(!pangram.Contains("fox"));

Console.WriteLine("a" == "A");
string myValue = "a";
Console.WriteLine(myValue == "a");




static bool CanMake33(int x, int y)
{
    return (x == 33 || y == 33 || x + y == 33 || x - y == 33 || y - x == 33 || x / y == 33 || x * y == 33);
}

var sb = new StringBuilder();
sb.Append("Pradeep ");
sb.AppendLine(" Singh");
sb.AppendLine("C# is great!");
sb.Replace("great", "good");
sb.Insert(6, "really");
sb.Remove(3, 2);

var sb1 = new StringBuilder("Today is ");
sb1.Append(DateTime.Now.ToString("yyyy-MM-dd"));
sb1.Append(" and the weather is sunny.");
string sb1string = sb1.ToString();
int length = sb1.Length;
sb1.Clear();

var sb2 = new StringBuilder("Number  Square");

for (int i=0; i < 5; i++)
{
    sb2.Append( i.ToString()); sb2.AppendLine((i * i).ToString());
};

    Console.WriteLine(CanMake33(22, 11)); // Output: false
*/