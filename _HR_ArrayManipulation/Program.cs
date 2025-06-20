namespace _HR_ArrayManipulation {
 internal class Program {
  static void Main(string[] args) {
   //mostly done
   int n = 10;
   List<List<int>> queries = new List<List<int>> {
   new List<int> {1,5,3 },
   new List<int> { 4,8,7 },
   new List<int> { 6,9,1 } };

   int[,] myArray = new int[queries.Count + 1, n];


   for (int i = 0; i < queries.Count; i++) {
    int startj = queries[i][0]; int endj = queries[i][1];
    for (int k = startj-1; k < endj-1; k++) {
      myArray[i + 1, k] = myArray[i, k] + queries[index: i][2];
     }
   }
  }
 }
}