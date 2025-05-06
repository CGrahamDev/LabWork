/*
 Build Specifications:
-Request and take user input (1 point)
-Create a separate method for reversing your string (3 points)
    -Return type should be a string
    -Parameter should be a string
-Inside your string reverse method, figure out how to reverse the inputted string using a Stack (5 points total)
    -Instantiating a stack: 1 point
    -Using the stack to store characters: 2 points
    -Using the stack to retrieve characters in reverse order: 2 points
-Display the reversed string in the console (1 point)

Hints:
-This problem CAN be solved without using a stack; if you find it easier without using a stack, try solving it without a stack first. After solving the problem without a stack, re-iterate over your solution and use a stack instead.

Extra Challenges:
-Consider cases where the user does not enter a single word but a sentence instead.
    -Do not reverse the entire sentence, instead reverse each word in the sentence, keeping the words in their original positions.
-Validate user input: make sure the user is only entering words and not symbols or numbers.

 */

while (true)
{
    Console.WriteLine("Hello! Please type and enter anything you want:");
    string userInput = "";
    userInput = Console.ReadLine();
    Console.Clear();
    Console.WriteLine($"Your input was \"{userInput}\"\n\n Your input reversed is: ");
    Console.WriteLine($"\"{ReverseInput(userInput)}\"\n\n");

    Console.WriteLine("Would you like to continue? (y/n)");
    userInput = AnswerYOrN();
    if (userInput == "y")
    {
        Console.Clear();
        continue;
    }
    else if (userInput == "n") 
    {
        Console.WriteLine("Thank you for using my text reverser! Goodbye!");
        Environment.Exit(0);
    }


}




string ReverseInput(string userInput)
{
    string reversedString = "";
    Stack<string> stackedText = new Stack<string>();
    List<string> reversedStrings = new List<string>();
    
    foreach (string s in userInput.Split())
    {
        stackedText.Push(s);
    }
    for(int i = stackedText.Count; i>0;i--) 
    {
        reversedStrings.Add(stackedText.Pop());
    }
    reversedString = String.Join(" ",reversedStrings);
    return reversedString;
}




string AnswerYOrN()
{
    while (true)
    {
        Console.WriteLine("Answer \"y\" or \"n\"");
        string answer = Console.ReadLine().ToLower();
        switch (answer)
        {
            case "y":
            case "n":
                return answer;
                break;
            default:
                Console.WriteLine("Invalid input: please enter \"y\" or \"n\"");
                continue;
        }
    }
}

