

#
`public delegate int Comparison<in T>(T left, T right);`
This Comparison<T> delegate can hold references to any method that Takes two parameters of type T and eturns an int (typically -1, 0, or 1 to indicate "less than", "equal to", or "greater than")

`public Comparison<T> comparator;`  

 At this point, comparator doesn't point to any method yet. 




##

- **Delegate**: A delegate is like a contract that defines a method’s signature (parameters and return type). It acts as a pointer to methods that match this signature, allowing them to be called indirectly.
- **Event**: In C#, an event is a special kind of delegate that supports the publisher-subscriber pattern. It allows multiple methods to be "subscribed" to it, and when the event is "raised" by the publisher, all subscribed methods are called.
- **Subscription (`+=`)**: This adds a method to the event’s list of handlers. It’s like signing up for a newsletter—your method gets added to the list of recipients.
- **Invocation**: When the publisher raises the event (using `Invoke`), all subscribed methods are called with the specified arguments.

In the example:
- The `TemperatureChanged` event in `WeatherStation` is of type `EventHandler<TemperatureChangedEventArgs>`, a built-in delegate type that expects methods with the signature `void (object, TemperatureChangedEventArgs)`.
- The `OnTemperatureChanged` method in `MobileApp` matches this signature, so it can be subscribed.
- The `+=` operator links the method to the event.

---

#### Step-by-Step Explanation
Here’s how the subscription `station.TemperatureChanged += app.OnTemperatureChanged;` leads to the invocation of `OnTemperatureChanged`:

##### 1. **Understanding the Method Signature**
The method in question is:

```csharp
public void OnTemperatureChanged(object sender, TemperatureChangedEventArgs e)
{
    Console.WriteLine($"Mobile App: Temperature updated to {e.Temperature}°C. Refreshing display.");
}
```

- **Signature Breakdown**:
  - **Return Type**: `void` (no return value).
  - **Parameters**:
    - `object sender`: Represents the object that raised the event (here, the `WeatherStation` instance). It’s `object` to be generic, allowing any type to be the sender.
    - `TemperatureChangedEventArgs e`: A custom class carrying data (the new temperature). It inherits from `EventArgs` to follow C# event conventions.
  - **Purpose**: This method is the subscriber’s handler. It runs when the event is raised, using `e.Temperature` to display the new temperature.

- **Why This Signature?**: The `EventHandler<TEventArgs>` delegate (used by `TemperatureChanged`) requires methods to have exactly this signature: `void (object, TEventArgs)`. This ensures compatibility when the event is raised.

##### 2. **The Event Declaration**
In the `WeatherStation` class:

```csharp
public event EventHandler<TemperatureChangedEventArgs> TemperatureChanged;
```

- **What This Means**:
  - `EventHandler<TemperatureChangedEventArgs>` is a built-in generic delegate type. It specifies that any method subscribed to this event must take an `object` (sender) and a `TemperatureChangedEventArgs` (event data) and return `void`.
  - The `event` keyword makes `TemperatureChanged` an event, restricting how it’s used (e.g., only the `WeatherStation` can raise it, not subscribers).
  - Think of `TemperatureChanged` as a list of methods waiting to be called when something happens (temperature changes).

##### 3. **Subscription with `+=`**
The line:

```csharp
station.TemperatureChanged += app.OnTemperatureChanged;
```

- **What Happens**:
  - `station` is an instance of `WeatherStation`, so `station.TemperatureChanged` is the event field.
  - `app` is an instance of `MobileApp`, and `app.OnTemperatureChanged` is the method we want to run when the event fires.
  - `+=` adds `app.OnTemperatureChanged` to the event’s internal list of handlers (called the **invocation list**). This is like signing up for a newsletter: the method is now "listening" for the event.
- **Analogy**: Imagine `TemperatureChanged` as a radio station’s broadcast list. `+=` tunes `app.OnTemperatureChanged` into that station. When the station broadcasts (event is raised), this method hears it.
- **Behind the Scenes**:
  - The delegate type ensures `OnTemperatureChanged` matches the required signature (`void (object, TemperatureChangedEventArgs)`).
  - If the signature didn’t match (e.g., wrong parameters), you’d get a compile-time error.

##### 4. **Raising the Event**
The event is raised in the `WeatherStation`’s `Temperature` property setter:

```csharp
public double Temperature
{
    get => _temperature;
    set
    {
        _temperature = value;
        TemperatureChanged?.Invoke(this, new TemperatureChangedEventArgs(value));
    }
}
```

- **How Invocation Happens**:
  - When you set `station.Temperature = 25;`, the setter runs.
  - `TemperatureChanged?.Invoke(this, new TemperatureChangedEventArgs(value))`:
    - `?.` checks if there are any subscribers (if `TemperatureChanged` is `null`, no one’s subscribed, so skip).
    - `Invoke` calls **every method** in the event’s invocation list (here, `app.OnTemperatureChanged` and others if subscribed).
    - Arguments passed:
      - `this`: The `WeatherStation` instance (becomes `sender` in the handler).
      - `new TemperatureChangedEventArgs(value)`: A new instance with `Temperature = 25` (becomes `e` in the handler).
  - So, `app.OnTemperatureChanged(station, new TemperatureChangedEventArgs(25))` is called, printing: `Mobile App: Temperature updated to 25°C. Refreshing display.`

- **Analogy**: Setting `Temperature` is like the newspaper printing a new issue. `Invoke` sends it to all subscribers’ mailboxes, and each subscriber reads the postcard (`TemperatureChangedEventArgs`) to act on it.

##### 5. **How Subscription Leads to Invocation**
- **Subscription (`+=`)**: Adds `app.OnTemperatureChanged` to `TemperatureChanged`’s invocation list. Internally, the event maintains a list of delegates pointing to subscribed methods.
- **Raising (`Invoke`)**: When `TemperatureChanged?.Invoke(...)` runs, it loops through the invocation list, calling each method with the provided arguments (`sender` and `e`).
- **Multicast Behavior**: If multiple methods are subscribed (e.g., `MobileApp` and `SmartThermostat`), all are called in order. In the example, both get invoked for each temperature change until unsubscribed.
- **Why It Works Seamlessly**:
  - The `EventHandler<TemperatureChangedEventArgs>` delegate ensures all subscribers match the signature.
  - The event system automates calling all handlers, passing the correct arguments.

##### 6. **Why Use `object sender` and `EventArgs`?**
- **C# Event Convention**: Most events in .NET follow the pattern `void (object sender, EventArgs e)`:
  - `sender`: Lets subscribers know who raised the event (useful if handling multiple publishers).
  - `e`: Carries event-specific data (here, `TemperatureChangedEventArgs` with the temperature).
- This standard makes code reusable across different events. `EventHandler<TEventArgs>` is a generic version that lets you use custom `EventArgs` (like ours).

##### 7. **Visualizing the Flow**
1. **Setup**:
   - Create `station` (WeatherStation) and `app` (MobileApp).
   - Subscribe: `station.TemperatureChanged += app.OnTemperatureChanged;` adds the method to the event’s list.
2. **Trigger**:
   - Set `station.Temperature = 25;`.
   - Setter calls `TemperatureChanged?.Invoke(station, new TemperatureChangedEventArgs(25));`.
3. **Execution**:
   - `Invoke` finds `app.OnTemperatureChanged` in the list.
   - Calls it with `sender = station`, `e = new TemperatureChangedEventArgs(25)`.
   - Method runs, printing the message.



#### Common Questions
- **Why `+=`?** It’s syntactic sugar for adding a method to the event’s delegate list. It’s like adding a phone number to a call list.
- **What if no subscribers?** The `?.Invoke` ensures no error—nothing happens if `TemperatureChanged` is `null`.
- **Why `object sender`?** It’s generic so any class can be the publisher. In practice, you might cast `sender` to `WeatherStation` if you need specific properties.
- **Can I subscribe multiple times?** Yes, each `+=` adds a new handler, and all run (multicast).

This is how the subscription leads to invocation. If you’re still confused, try running the code or let me know which part needs more clarity!
##
using System;

// 1. Nullable value type declaration
int? nullableInt = null;
// nullableInt is null here

// 2. Null conditional operator
string test = null;
int? length = test?.Length;
// length is null as test is null

// 3. Null coalescing operator
int? anotherNullableInt = null;
int nonNullableInt = anotherNullableInt ?? 0;
// nonNullableInt is 0 as anotherNullableInt is null

// 4. Nullable reference type (only if Nullable Reference Types feature is enabled)
#nullable enable
string? nullableString = null;
#nullable restore
// nullableString is null here

// 5. Ternary conditional operator
int x = 10;
string result = x > 5 ? "Greater than 5" : "Less than or equal to 5";
// result is "Greater than 5"

// 6. Nullable value type with GetValueOrDefault method
int? nullableWithValue = 5;
int valueOrDefault = nullableWithValue.GetValueOrDefault(10);
// valueOrDefault is 5 as nullableWithValue is not null

// 7. Nullable value type with HasValue property
bool hasValue = nullableWithValue.HasValue;
// hasValue is true as nullableWithValue is not null

// 8. Nullable value type with Value property
int value = nullableWithValue.Value;
// value is 5 as nullableWithValue is not null

// 9. Nullable value type assignment with null conditional operator
int? possiblyNullInt = null;
nullableWithValue = possiblyNullInt ?? nullableWithValue;
// nullableWithValue is still 5 as possiblyNullInt is null

// 10. Nullable reference type checking with is null
#nullable enable
bool isNull = nullableString is null;
#nullable restore
// isNull is true as nullableString is null
Console.WriteLine("end");

#14. Longest Common Prefix
//string[] strs
string longestPrefix = "";

for (int i=1; strs.Length - 1; i++)
{
		for (int j=1; strs[0].Length; j++) //are 2 .Length ok
			{
					if (left(strs[0],j) != left(strs[i],j) exit;
			}
			longestPrefix = left(strs[0],j)
};
return longestPrefix;

#13. Roman to Integer
//string s
char[] sCharArray = s; //??

Dictionary[char, int]  romanInt = { (I,0), (V,5), (X,10), (L,50), (C,100), (D,500), (M,1000)}; //?
int[] sIntArray; 

for (int i=0; sCharArray.Length-1; i++)
{
Case sCharArray(i):
			I: if (sCharArray(i+1)==( "V" || "X") ? sIntArray(i) = -1 : sIntArray(i) = 1 ;

			V:  sIntArray(i) =5 ;
			X:  if (sCharArray(i+1)==( "L" || "C") ? sIntArray(i) = -10 : sIntArray(i) = 10 ;

			L:  sIntArray(i) = 50;
			C:  if (sCharArray(i+1)==( "D" || "M") ? sIntArray(i) = -100 : sIntArray(i) = 100 ;

			D:  sIntArray(i) = 500;
			M: sIntArray(i) = 1000;
};

#1 
a) Use an array as size change is not needed
sort the array in increasing order
numsNew = nums.Sort()
left=0
right=numsNew.Length -1

j = 0 to numsNew.Length-1, j++ 
{
result= target -numsNew[left]-numsNew[right]
case result 
				0: return
				>0:  increase left
				{	
				0 <= (target - numsNew[left] - numsNew[right]). If equal then exit
				}
				<0   reduce right
				{	
				 0>= (target - numsNew[left] - numsNew[right]). If equal then exit
				}
}
prelim answer  = left, right

nums[leftFinal] = numsNew[left] 
nums[rightFinal] = numsNew[right]
 
 edge cases: what happens if 2 numbers are same
 
#9. Palindrome Number leetcode has latest
bool isPalindrome = false;
if x<0 then exit
x=989989

char [] xArray =  x.ToString()  //gives [9,8,9,9,8,9]
 for (i=0, xArray.Length/2, i++)
 {
   if xArray[i] != xArray[xArray.Length -i] return false;  //exit 
  };
 isPalindrome = true;
###
Start: What does your LeetCode problem need to store/process?
|
|-- Need key-value pairs? (e.g., map ID to value, fast lookup by key like in Two Sum)
|   |
|   Yes --> Dictionary Category
|   |       - Need sorted by key? (e.g., leaderboard scores)
|   |       |   Yes --> Sorted: SortedDictionary<TKey,TValue> (tree-based, log(n) ops), SortedList<TKey,TValue> (array-based, fast index but slow inserts), ImmutableSortedDictionary<TKey,TValue> (unchangeable)
|   |       |          Non-generic: SortedList (older)
|   |       |   No  --> Unordered fast: Dictionary<TKey,TValue> (hash O(1)), ImmutableDictionary<TKey,TValue> (safe snapshot)
|   |                   Thread-safe: ConcurrentDictionary<TKey,TValue> (multi-thread, rare in LeetCode)
|   |                   Small/variable: HybridDictionary, ListDictionary (non-generic, for few items)
|   |                   Strings only: StringDictionary (case-insensitive), NameValueCollection (multi-values per key, e.g., headers)
|   |                   Non-generic: Hashtable (older hash)
|   |
|   No --> Need unique items only? (no duplicates, e.g., check existence fast like in duplicates problems)
|         |
|         Yes --> Set Category
|         |       - Need sorted? (e.g., remove dups and sort)
|         |       |   Yes --> SortedSet<T> (tree), ImmutableSortedSet<T> (unchangeable)
|         |       |   No  --> HashSet<T> (fast hash), ImmutableHashSet<T> (safe)
|         |
|         No --> Need sequence/order? (e.g., list of items, random access like array in Two Sum)
|               |
|               Yes --> List Category
|               |       - Fast random access/growable? (most common, e.g., to-do list)
|               |       |   Yes --> List<T> (dynamic array), ImmutableList<T> (unchangeable), ImmutableArray<T> (fixed safe array)
|               |       |          Non-generic: ArrayList (mixed types, older), StringCollection (strings only)
|               |       |          UI/notifies: ObservableCollection<T> (for WPF, changes trigger updates)
|               |       |          Read-only: ReadOnlyCollection<T> (share without changes)
|               |       - Fast insert/delete in middle? (e.g., browser history)
|               |           Yes --> LinkedList<T> (doubly-linked)
|               |       - Bits/booleans compact? (rare, e.g., flags)
|               |           Yes --> BitArray (non-generic bits)
|               |
|               No --> Need specific processing order?
|                       |
|                       |-- FIFO (first-in-first-out, e.g., BFS tree traversal)
|                       |    --> Queue Category
|                       |         - Basic: Queue<T>, Queue (non-generic)
|                       |         - Thread-safe: ConcurrentQueue<T> (producer-consumer)
|                       |         - Immutable: ImmutableQueue<T> (persistent)
|                       |         - Blocking: BlockingCollection<T> (wait if empty/full, bounded buffer)
|                       |
|                       |-- LIFO (last-in-first-out, e.g., parentheses validation)
|                       |    --> Stack Category
|                       |         - Basic: Stack<T>, Stack (non-generic)
|                       |         - Thread-safe: ConcurrentStack<T> (multi-user)
|                       |         - Immutable: ImmutableStack<T> (version control)
|                       |
|                       No --> Specialized Category (niche, rare in LeetCode)
|                               - Thread-safe bag (unordered, duplicates OK): ConcurrentBag<T> (multi-thread logging)
|                               - Single pair: KeyValuePair<TKey,TValue> (for iterating dicts)

###
Start: What does your LeetCode problem need to store/process?
|
|-- Need key-value pairs? (e.g., map ID to value, fast lookup by key like in Two Sum)
|   |
|   Yes --> Dictionary Category
|   |       - Need sorted by key? (e.g., leaderboard scores)
|   |       |   Yes --> 
|   |       |          SortedDictionary<TKey,TValue> (tree-based, log(n) ops)
|   |       |             a) Problem: Merge k Sorted Lists (LeetCode 23) - Merge multiple sorted linked lists into one sorted list efficiently using a min-heap-like structure, but SortedDictionary can track priorities.
|   |       |             b) Data: Key=1 Value=Node(1), Key=3 Value=Node(3), Key=4 Value=Node(4) (sorted by keys automatically).
|   |       |             c) Pseudocode: 
|   |       |                SortedDictionary<int, ListNode> dict = new(); // Add nodes with values as keys
|   |       |                while (dict.Count > 0) { var min = dict.First(); Merge(min.Value); dict.Remove(min.Key); }
|   |       |          SortedList<TKey,TValue> (array-based, fast index but slow inserts)
|   |       |             a) Problem: Remove Duplicates from Sorted List (LeetCode 83) - Given a sorted linked list, remove duplicates; SortedList can store and auto-sort with index access.
|   |       |             b) Data: Key=1 Value="A", Key=2 Value="B", Key=3 Value="C" (accessible by index 0,1,2).
|   |       |             c) Pseudocode: 
|   |       |                SortedList<int, string> sl = new() { {1,"A"}, {1,"A"}, {2,"B"} }; // Auto removes dups if custom logic
|   |       |                for (int i = 0; i < sl.Count; i++) { if (sl.Keys[i] == prev) RemoveAt(i); }
|   |       |          ImmutableSortedDictionary<TKey,TValue> (unchangeable, rare in LeetCode)
|   |       |             a) Similar Problem: Range Sum Query - Immutable (LeetCode 303) - Precompute prefix sums in an immutable array for fast queries; use immutable dict for fixed key-value sums.
|   |       |             b) Data: Key=1 Value=10, Key=2 Value=20, Key=3 Value=30 (can't change after creation).
|   |       |             c) Pseudocode: 
|   |       |                var builder = ImmutableSortedDictionary<int,int>.Empty.ToBuilder(); builder.Add(1,10); builder.Add(2,20);
|   |       |                var immDict = builder.ToImmutable(); int sum = immDict[2] - immDict[1];
|   |       |          Non-generic: SortedList (older, similar to above)
|   |       |             a) Same as SortedList<TKey,TValue> but without types: Remove Duplicates from Sorted List.
|   |       |             b) Data: Key="A" Value=1, Key="B" Value=2, Key="C" Value=3 (object types).
|   |       |             c) Pseudocode: 
|   |       |                SortedList sl = new() { {"A",1}, {"A",1} }; // Handle dups manually
|   |       |                for (int i = 0; i < sl.Count; i++) { if (sl.GetKey(i).Equals(prev)) sl.RemoveAt(i); }
|   |       |   No  --> 
|   |                   Dictionary<TKey,TValue> (hash O(1))
|   |                      a) Problem: Two Sum (LeetCode 1) - Given nums array and target, find two indices that sum to target using dict for fast complement lookup.
|   |                      b) Data: Key=2 Value=0, Key=7 Value=1, Key=11 Value=2 (keys are numbers, values are indices).
|   |                      c) Pseudocode: 
|   |                         Dictionary<int,int> dict = new();
|   |                         for (int i=0; i<nums.Length; i++) { int comp = target - nums[i]; if (dict.ContainsKey(comp)) return new[] {dict[comp], i}; dict[nums[i]] = i; }
|   |                   ImmutableDictionary<TKey,TValue> (safe snapshot, rare)
|   |                      a) Similar: Word Break (LeetCode 139) - Check if string can be segmented into dictionary words; immutable for fixed word set.
|   |                      b) Data: Key="apple" Value=true, Key="pen" Value=true, Key="pine" Value=true (unchangeable).
|   |                      c) Pseudocode: 
|   |                         var immDict = ImmutableDictionary<string,bool>.Empty.Add("apple",true).Add("pen",true);
|   |                         bool canBreak = immDict.ContainsKey(substring);
|   |                   Thread-safe: ConcurrentDictionary<TKey,TValue> (multi-thread, rare in LeetCode)
|   |                      a) Similar: Building H2O (LeetCode 1117) - Multithreaded sync for H and O atoms; use concurrent dict for counting.
|   |                      b) Data: Key="H" Value=2, Key="O" Value=1, Key="H2O" Value=0 (atomic updates).
|   |                      c) Pseudocode: 
|   |                         ConcurrentDictionary<string,int> cd = new(); cd.AddOrUpdate("H", 1, (k,v)=>v+1);
|   |                         if (cd["H"] >=2 && cd["O"]>=1) { /* release H2O */ cd["H"]-=2; cd["O"]-=1; }
|   |                   Small/variable: HybridDictionary (non-generic)
|   |                      a) Similar to Dictionary but switches impl: Contains Duplicate II (LeetCode 219).
|   |                      b) Data: Key=1 Value=0, Key=3 Value=1, Key=1 Value=2 (handles small to large).
|   |                      c) Pseudocode: 
|   |                         HybridDictionary hd = new() { {1,0}, {3,1} }; if (hd.Contains(1)) CheckDistance((int)hd[1]);
|   |                   ListDictionary (non-generic, small sets)
|   |                      a) Similar: Find Duplicate File in System (LeetCode 609) - Group files by content.
|   |                      b) Data: Key="file1" Value="contentA", Key="file2" Value="contentA", Key="file3" Value="contentB".
|   |                      c) Pseudocode: 
|   |                         ListDictionary ld = new() { {"file1","contentA"} }; ld.Add("file2","contentA"); GroupByValue(ld);
|   |                   Strings only: StringDictionary (case-insensitive)
|   |                      a) Similar: Two Sum but with strings: Abbreviations to words.
|   |                      b) Data: Key="mr" Value="Mister", Key="mrs" Value="Missus", Key="dr" Value="Doctor".
|   |                      c) Pseudocode: 
|   |                         StringDictionary sd = new() { {"mr","Mister"} }; string full = sd["MR"]; // Case-insensitive
|   |                   NameValueCollection (multi-values per key)
|   |                      a) Similar: Group Anagrams (LeetCode 49) - Group strings by sorted chars.
|   |                      b) Data: Key="eat" Values=["eat","tea","ate"], Key="tan" Values=["tan","nat"].
|   |                      c) Pseudocode: 
|   |                         NameValueCollection nvc = new(); nvc.Add("eat","eat"); nvc.Add("eat","tea"); GetAllValues("eat");
|   |                   Non-generic: Hashtable (older hash)
|   |                      a) Same as Dictionary: Two Sum.
|   |                      b) Data: Key=2 Value=0, Key=7 Value=1, Key=11 Value=2.
|   |                      c) Pseudocode: 
|   |                         Hashtable ht = new(); ht[2]=0; if (ht.ContainsKey(comp)) return;
|   |
|   No --> Need unique items only? (no duplicates, e.g., check existence fast like in duplicates problems)
|         |
|         Yes --> Set Category
|         |       - Need sorted? (e.g., remove dups and sort)
|         |       |   Yes --> 
|         |       |          SortedSet<T> (tree)
|         |       |             a) Problem: Remove Duplicates from Sorted Array (LeetCode 26) - Remove dups in-place; SortedSet auto-sorts uniques.
|         |       |             b) Data: 1, 2, 3 (sorted, no dups).
|         |       |             c) Pseudocode: 
|         |       |                SortedSet<int> ss = new(nums); int i=0; foreach(var num in ss) nums[i++]=num; return ss.Count;
|         |       |          ImmutableSortedSet<T> (unchangeable)
|         |       |             a) Similar: Range Sum Query 2D - Immutable (LeetCode 304) - Fixed matrix for queries; use immutable set for unique rows.
|         |       |             b) Data: 1, 2, 3 (fixed sorted).
|         |       |             c) Pseudocode: 
|         |       |                var iss = ImmutableSortedSet<int>.Empty.Add(1).Add(2); bool has = iss.Contains(2);
|         |       |   No  --> 
|         |                   HashSet<T> (fast hash)
|         |                      a) Problem: Contains Duplicate (LeetCode 217) - Check if array has duplicates using set for uniqueness.
|         |                      b) Data: 1, 2, 3 (no dups allowed).
|         |                      c) Pseudocode: 
|         |                         HashSet<int> hs = new(); foreach(int n in nums) if (!hs.Add(n)) return true; return false;
|         |                   ImmutableHashSet<T> (safe)
|         |                      a) Similar: Additive Number (LeetCode 306) - Check cumulative sums; immutable for fixed uniques.
|         |                      b) Data: "1", "2", "3" (unchangeable).
|         |                      c) Pseudocode: 
|         |                         var ihs = ImmutableHashSet<string>.Empty.Add("1"); bool isAdditive = ihs.Contains(nextSum);
|         |
|         No --> Need sequence/order? (e.g., list of items, random access like array in Two Sum)
|               |
|               Yes --> List Category
|               |       - Fast random access/growable? (most common, e.g., to-do list)
|               |       |   Yes --> 
|               |       |          List<T> (dynamic array)
|               |       |             a) Problem: Two Sum (LeetCode 1) - Use list for input nums, iterate for sums.
|               |       |             b) Data: 2, 7, 11 (indices 0,1,2).
|               |       |             c) Pseudocode: 
|               |       |                List<int> nums = new() {2,7,11}; for(int i=0; i<nums.Count; i++) for(int j=i+1; j<nums.Count; j++) if(nums[i]+nums[j]==target) return;
|               |       |          ImmutableList<T> (unchangeable)
|               |       |             a) Similar: Range Sum Query - Immutable (LeetCode 303) - Precompute immutable prefix list.
|               |       |             b) Data: 1, 3, 6 (cumulative sums).
|               |       |             c) Pseudocode: 
|               |       |                ImmutableList<int> il = ImmutableList.Create(1,3,6); int sum = il[to] - il[from-1];
|               |       |          ImmutableArray<T> (fixed safe array)
|               |       |             a) Similar: Same as above, for fixed arrays.
|               |       |             b) Data: 2, 7, 11 (can't resize).
|               |       |             c) Pseudocode: 
|               |       |                ImmutableArray<int> ia = ImmutableArray.Create(2,7,11); int val = ia[1];
|               |       |          Non-generic: ArrayList (mixed types, older)
|               |       |             a) Same as List<T>: Two Sum with mixed.
|               |       |             b) Data: 2, "seven", 11.0 (any types).
|               |       |             c) Pseudocode: 
|               |       |                ArrayList al = new() {2,7,11}; int sum = (int)al[0] + (int)al[1];
|               |       |          StringCollection (strings only)
|               |       |             a) Similar: Recent documents list.
|               |       |             b) Data: "file1.txt", "file2.txt", "file3.txt".
|               |       |             c) Pseudocode: 
|               |       |                StringCollection sc = new() {"file1.txt"}; sc.Add("file2.txt"); foreach(string s in sc) Console.Write(s);
|               |       |          UI/notifies: ObservableCollection<T> (for WPF, changes trigger updates, rare)
|               |       |             a) Similar: Dynamic UI list updates.
|               |       |             b) Data: "Item1", "Item2", "Item3" (notifies on add/remove).
|               |       |             c) Pseudocode: 
|               |       |                ObservableCollection<string> oc = new(); oc.CollectionChanged += UpdateUI; oc.Add("Item1");
|               |       |          Read-only: ReadOnlyCollection<T> (share without changes)
|               |       |             a) Similar: Constant config list in problems.
|               |       |             b) Data: 1, 2, 3 (can't modify).
|               |       |             c) Pseudocode: 
|               |       |                List<int> temp = new(){1,2,3}; ReadOnlyCollection<int> roc = new(temp); int val = roc[0]; // No Add()
|               |       - Fast insert/delete in middle? (e.g., browser history)
|               |           Yes --> 
|               |                  LinkedList<T> (doubly-linked)
|               |                     a) Problem: Reverse Linked List (LeetCode 206) - Reverse a singly linked list; use LinkedList for easy node ops.
|               |                     b) Data: Node(1) -> Node(2) -> Node(3) (links forward/back).
|               |                     c) Pseudocode: 
|               |                        LinkedList<int> ll = new() {1,2,3}; LinkedListNode<int> prev=null, curr=ll.First;
|               |                        while(curr!=null) { var next=curr.Next; curr.Next=prev; prev=curr; curr=next; }
|               |       - Bits/booleans compact? (rare, e.g., flags)
|               |           Yes --> 
|               |                  BitArray (non-generic bits)
|               |                     a) Similar: Track permissions as bits.
|               |                     b) Data: true, false, true (bits 1,0,1).
|               |                     c) Pseudocode: 
|               |                        BitArray ba = new(3) { [0]=true, [2]=true }; bool perm = ba[0];
|               |
|               No --> Need specific processing order?
|                       |
|                       |-- FIFO (first-in-first-out, e.g., BFS tree traversal)
|                       |    --> Queue Category
|                       |         - Basic: Queue<T>
|                       |            a) Problem: Binary Tree Level Order Traversal (LeetCode 102) - Traverse tree level by level using queue for BFS.
|                       |            b) Data: Node(1), Node(2), Node(3) (dequeue 1 first).
|                       |            c) Pseudocode: 
|                       |               Queue<TreeNode> q = new(); q.Enqueue(root);
|                       |               while(q.Count>0) { int size=q.Count; for(int i=0;i<size;i++) { var node=q.Dequeue(); Process(node); if(node.left!=null) q.Enqueue(node.left); } }
|                       |         - Queue (non-generic)
|                       |            a) Same as Queue<T>: Level Order.
|                       |            b) Data: 1, 2, 3 (object types).
|                       |            c) Pseudocode: 
|                       |               Queue q = new(); q.Enqueue(1); int first = (int)q.Dequeue();
|                       |         - Thread-safe: ConcurrentQueue<T> (producer-consumer)
|                       |            a) Similar: Print in Order (LeetCode 1114) - Sync threads; use concurrent queue for messages.
|                       |            b) Data: "First", "Second", "Third" (safe enqueue from threads).
|                       |            c) Pseudocode: 
|                       |               ConcurrentQueue<string> cq = new(); Task.Run(() => cq.Enqueue("First")); string msg; cq.TryDequeue(out msg);
|                       |         - Immutable: ImmutableQueue<T> (persistent)
|                       |            a) Similar: Undo history snapshots.
|                       |            b) Data: 1, 2, 3 (immutable copy on change).
|                       |            c) Pseudocode: 
|                       |               ImmutableQueue<int> iq = ImmutableQueue<int>.Empty.Enqueue(1).Enqueue(2); var newIq = iq.Dequeue();
|                       |         - Blocking: BlockingCollection<T> (wait if empty/full)
|                       |            a) Similar: Bounded producer-consumer.
|                       |            b) Data: 1, 2, 3 (blocks if full).
|                       |            c) Pseudocode: 
|                       |               BlockingCollection<int> bc = new(3); bc.Add(1); int item = bc.Take(); // Blocks if empty
|                       |
|                       |-- LIFO (last-in-first-out, e.g., parentheses validation)
|                       |    --> Stack Category
|                       |         - Basic: Stack<T>
|                       |            a) Problem: Valid Parentheses (LeetCode 20) - Check balanced brackets using stack to match opens/closes.
|                       |            b) Data: '(', '{', '[' (pop '[' first).
|                       |            c) Pseudocode: 
|                       |               Stack<char> s = new(); foreach(char c in str) { if (IsOpen(c)) s.Push(c); else if (s.Count==0 || !Matches(s.Pop(),c)) return false; } return s.Count==0;
|                       |         - Stack (non-generic)
|                       |            a) Same: Valid Parentheses.
|                       |            b) Data: '(', '{', '['.
|                       |            c) Pseudocode: 
|                       |               Stack s = new(); s.Push('('); char top = (char)s.Pop();
|                       |         - Thread-safe: ConcurrentStack<T>
|                       |            a) Similar: Multithreaded undo.
|                       |            b) Data: "Action1", "Action2", "Action3".
|                       |            c) Pseudocode: 
|                       |               ConcurrentStack<string> cs = new(); cs.Push("Action1"); string undo; cs.TryPop(out undo);
|                       |         - Immutable: ImmutableStack<T>
|                       |            a) Similar: Version control undo.
|                       |            b) Data: 1, 2, 3 (immutable).
|                       |            c) Pseudocode: 
|                       |               ImmutableStack<int> is = ImmutableStack<int>.Empty.Push(1).Push(2); var newIs = is.Pop();
|                       |
|                       No --> Specialized Category (niche, rare in LeetCode)
|                               - Thread-safe bag (unordered, duplicates OK): ConcurrentBag<T>
|                                  a) Similar: Multithreaded logging.
|                                  b) Data: "Log1", "Log2", "Log1" (duplicates OK).
|                                  c) Pseudocode: 
|                                     ConcurrentBag<string> cb = new(); Task.Run(() => cb.Add("Log1")); string log; cb.TryTake(out log);
|                               - Single pair: KeyValuePair<TKey,TValue>
|                                  a) Similar: Iterating dict in Two Sum.
|                                  b) Data: Key=2 Value=0, Key=7 Value=1, Key=11 Value=2.
|                                  c) Pseudocode: 
|                                     foreach(var kvp in dict) { int key = kvp.Key; int val = kvp.Value; Process(kvp); }
##other
IEnumerable is like searching a small box of toys in your room. 
IQueryable is like asking a librarian to search the whole library and only bring back matching books—efficient for big "libraries."
Lazy loading - Like planning a grocery list but not shopping until you're hungry. If plans change, no wasted trip.
Materialized (Eager Execution) - forcing the query to run immediately and store results in memory (e.g., as a List<T> or array). Methods like ToList(), ToArray(), Count(), First() trigger this

Where
Join, GroupBy, GroupJoin
OrderBy, OrderByDescending, ThenBy
Select, SelectMany
Any, All, Count, Take, Skip, First
ToList(), ToArray()


In ASCII, 'A' < 'a'.

There are 2 forms
n => n % 2 == 0 
n => { return n % 2 == 0; }
Modularity and Reusability: Methods encapsulate logic into self-contained units (e.g., AddNumbers can be called from anywhere). Delegates let you reuse behavior dynamically—e.g., swap addition for multiplication without changing code.
Abstraction and Encapsulation: You hide implementation details (e.g., how AddNumbers works) behind signatures. Callers only need the delegate or method name, not the internals. This reduces complexity in large programs.
Polymorphism: Delegates support it by allowing different methods/functions with the same signature to be interchangeable (e.g., addition vs. multiplication via the same delegate type). This makes code flexible—e.g., in UI apps, events (built on delegates) let buttons trigger different actions.
Loose Coupling: Code doesn't depend on specific methods; it depends on signatures via delegates. This makes testing easier (mock a delegate) and extensibility better (add new functions without rewriting classes).
Type Safety: C# enforces that delegates only reference matching signatures at compile-time, preventing runtime errors. Compared to non-OOP languages (e.g., procedural C), this avoids bugs like passing wrong function pointers.
Efficiency and Readability: OOP organizes code into classes (e.g., math operations in a Calculator class), while delegates add functional power without clutter. In our example, the lambda is concise, improving readability for simple cases.
Method overloading (having multiple methods with the same name) is allowed as long as signatures differ (e.g., different parameter types).

Give examples of 
- Functions can be passed around like variables
- Delegates as callbacks
- Delegates as events
- Delegates as passing behavior as data
- Multicast Delegate: A delegate that can reference multiple methods (invoked in sequence).
- Event: A special delegate for publisher-subscriber patterns (e.g., button clicks).
- Func<t> and Action<t></t></t>: Built-in generic delegates in C# (e.g., Func<int, int> for a function taking an int and returning an int)
- Delegate enables polymorphism (different objects behaving differently via the same interface) and loose coupling.



#note
#extension methods




Func is a built-in delegate type in C#. It's a generic way to describe a function that returns a value (unlike Action, which doesn't return anything).
In C# 9+,  single .cs file with at least one executable statement
In C# 8 or less,  single .cs file with Using System and Static Void Main
##Understand

when I see the method signature as below
public static TResult Aggregate<TSource, TAccumulate, TResult>(this IEnumerable<TSource> source, TAccumulate seed, Func<TAccumulate, TSource, TAccumulate> func, Func<TAccumulate, TResult> resultSelector);

does this mean that 
a) TSource, TAccumulate, TResult can be of anytime string, int, int[], etc.
b) TSource has to be IEnumerable
c) Func<TAccumulate, TSource, TAccumulate>  takes a variable, processes it and returns as a variable of the same type
d) Func<TAccumulate, TResult>  - does this mean that this takes the output of previous Func (i.e. Func<TAccumulate, TSource, TAccumulate> ),  processes it and returns the result
e) result of the whole method is TResult
f) what does  Aggregate<TSource, TAccumulate, TResult> mean

IEnumerable<TSource>, which means "a collection (like a list or array) where each item is of type TSource.
If source is a List<int> (a list of numbers), then TSource is int.




fruits.Aggregate(
            "banana",  // Seed: Initial accumulator value.
            (longest, next) => next.Length > longest.Length ? next : longest,  // Func: Accumulator compares lengths.
            fruit => fruit.ToUpper()  // ResultSelector: Transform to uppercase.
        );
		