/*
 Description
Prompt the user to enter a sentence.  Split the sentence into individual words and display each word on its own line.

Example
>>Enter a sentence: <<The cow jumped over the moon.
>>The
cow
jumped
over
the
moon.
Would you like to continue (y/n)? <<y
>>Enter a sentence: <<Hello, World!
>>Hello,
World!
Would you like to continue (y/n)? <n
<<Goodbye!


Description
Repeatedly prompt the user to enter a string.  Store the string in a list and display the contents of the list with each element separated by a space.

Example
>>Enter some text: <<The
<<You have entered: The
Would you like to continue (y/n)? <<y
>>Enter some text: <<cow
<<You have entered: The cow
Would you like to continue (y/n)? <<y
>>Enter some text: <<jumped...
<<You have entered: The cow jumped...
Would you like to continue (y/n)? <<n
>>Goodbye!
 */

using System.Transactions;

while (true)
{
    Console.WriteLine("Welcome! Enter a sentence:");
    string userSentence = Console.ReadLine();


    SplitAndDisplayUserString(userSentence);
    Console.WriteLine("Would you like to continue (y/n)");
    string userContinueAnswer = Console.ReadLine();

    if(userContinueAnswer == "n")
    {
        Console.WriteLine("Goodbye!");
        break;
    }

}
List<string> textList = new List<string>();

while (true)
{
    Console.WriteLine("Enter some text:");
    string userText = Console.ReadLine();
    textList.Add(userText);
    Console.Write("You have entered: ");
    foreach (string text in textList)
    {
        Console.Write(text + " ");
    }
    Console.WriteLine(" ");
    Console.WriteLine("Would you like to continue (y/n)?");
    string userContinueAnswer = Console.ReadLine();
    if (userContinueAnswer == "n")
    {
        Console.WriteLine("Goodbye!");
        break;
    }
}













Console.ReadKey();
Environment.Exit(0);









void SplitAndDisplayUserString(string userString)
{
    string[] splitString = userString.Split(" ");
    foreach (string s in splitString)
    {
        Console.WriteLine(s);
    }
}