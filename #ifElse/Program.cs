using System;

int number = 10; string name = "Alice"; string maybeNull = null;

string result1 = (number > 0) ? "Positive" : "Not Positive"; // No IF, should be a boolean and both sides should return compatible values
if (name is string { Length: > 3 })  Console.WriteLine("test");
string safeName = maybeNull ?? "Default Name";
maybeNull ??= "Assigned if null"; //Null-coalescing assignment (??=) - Like "if null then assign, else do nothing"
string sign = number > 0 ? "Positive" : number < 0 ? "Negative" : "Zero"; //nested ternary 

if (number > 0 && number % 2 == 0) { Console.WriteLine("test"); } else if (number < 0 || number == 0) { } else { Console.WriteLine("test"); }

if (number > 0 && number % 2 == 0)
{
}
else if (number < 0 || number == 0)
{
    Console.WriteLine("test");
}
else
{
    Console.WriteLine("test");
}

/*
(nums[l], nums[r]) = (nums[r], nums[l]);  //tuple deconstruction
*/