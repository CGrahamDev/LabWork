// See https://aka.ms/new-console-template for more information
int[] nums = { 10, 2330, 112233, 12, 949, 3764, 2942, 523863 };

int minimum = nums.Min();
int maximum = nums.Max();
int maxValue = nums.Where(x => x < 10000).Max();

int[] firstValues = nums.Where(x => x > 10 && x < 100).ToArray(); //Where returns multiple values so use lists or Arrays.

int[] secondValues = nums.Where(x=>x >= 100000  && x <= 999999 ).ToArray();

int evenNumbers = nums.Count(x => x % 2 == 0);  // Counts return a single value