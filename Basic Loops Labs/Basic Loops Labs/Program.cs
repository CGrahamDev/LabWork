// See https://aka.ms/new-console-template for more information
using System;
using System.CodeDom.Compiler;
using System.ComponentModel.Design;
using System.Data;
using System.Reflection.Metadata;
using System.Transactions;

/*Console.WriteLine("Hello, World!");
//////Description
Use a do-while loop to output "Hello, World!" in a loop. Each time you output "Hello, World!" ask the user whether they would like to continue.

Example
>>Hello, World!
Would you like to continue (y/n)? <<y
>>Hello, World!
Would you like to continue (y/n)? <<y
>>Hello, World!
Would you like to continue (y/n)? <<y
>>Hello, World!
Would you like to continue (y/n)? <<n
>>Goodbye!
-------------------------------------------------
Description
Prompt the user for a number. Use a for loop to output all the numbers from that number to 0. After that loop finishes, use another loop to output all the numbers from 0 to that number.

Example
>>Enter a number: << 5
>> 5 4 3 2 1 0
>> 0 1 2 3 4 5
Would you like to continue (y/n)? <<y
>>Enter a number: << 12
>> 12 11 10 9 8 7 6 5 4 3 2 1 0
>> 0 1 2 3 4 5 6 7 8 9 10 11 12
Would you like to continue (y/n)? <<n
>>Goodbye!

--------------------------------------------------------

Description
A door has a keypad entry. The combination to get in is 13579. Write a while loop (not a do while loop) that asks the user to enter a key code. The loop will repeat as long as the user enters the wrong code. After the user enters the correct code, the program will print out a welcome message.

Hint: There are many ways to accomplish this, but one way is to create a boolean variable that represents if the door is locked or unlocked. Then think about real life, when you approach a door with a keypad, what state is it initially in before you type anything into the keypad?

---------------------------------------------------------

Description
Continue the previous exercise, but now add a limited number of tries, say 5. After 5 unsuccessful attempts, the loop ends, but instead of printing a welcome message, it prints a message warning that there were too many incorrect attempts. (But if the user entered the correct number, it will still print the welcome message as before.)

---------------------------------------------------------------

Description
Start a new console project, and repeat the same exercise as the previous, except this time implement it with a do while loop.

---------------------------------------------------------------

Optional Stretch
Move the last while loop or the do-while loop into its own function. The function should return a true if access is granted, or a false if the user didn’t enter the correct code within 5 tries.
*/


//Lab Assignment 1

bool helloWorldLoopEnd = false;
do
{
    
    Console.WriteLine("Hello World!");

    Console.WriteLine("Would you like to continue? (y/n)");
    string helloWorldUserAnswer = Console.ReadLine();

    if (helloWorldUserAnswer.ToLower() == "n")
    {
        Console.WriteLine("Goodbye!");
        helloWorldLoopEnd = true;
    }
    else if (helloWorldUserAnswer.ToLower() != "y" && helloWorldUserAnswer.ToLower() != "n")
    {
        Console.WriteLine("Invalid input. Goodbye!");
        helloWorldLoopEnd = true;
    }
    

} while (helloWorldLoopEnd != true);


//--------------------------------------------------------------------------------------------------------------------

//Lab asignment 2


bool loopTwoEnd = false;


do
{
    Console.WriteLine("Enter a number");

    int userLabTwoNumber = int.Parse(Console.ReadLine());
    //Count Down
    for (int i = userLabTwoNumber; i >= 0; i--)
    {
        Console.Write(i + " ");




    }
    Console.WriteLine(" ");
    
    
    //Count Up
    for (int j = 0; j <= userLabTwoNumber; j++)
    {
        Console.Write(j + " ");
    }
    Console.WriteLine(" ");
    Console.WriteLine("Would you like to continue? (y/n)");
    
    //Program Continue or End question
    string userContinueAnswerTwo = Console.ReadLine();
    if (userContinueAnswerTwo.ToLower() == "n")
    {
        loopTwoEnd = true;
        Console.WriteLine("Goodbye!");
        //thought it was fun.
        Console.Beep();
    }
    else if (userContinueAnswerTwo.ToLower() == "y")
    {
        Console.Write("Okay! ");
    }
    else
    {
        Console.WriteLine("Invalid input. Goodbye!");
        loopTwoEnd = true;
    }
} while (loopTwoEnd != true);

//------------------------------------------------------------------------------------------------------------------

//Lab Assignments 3 & 4

int doorCombination = 13579;
int userDoorNumGuess = -1;
bool doorUnlocked = false;
const int unlockAttemptLimit = 5;
int userUnlockAttempts = 0;

while (doorUnlocked == false )
{
    if (userUnlockAttempts == unlockAttemptLimit)
    {
        Console.WriteLine("Warning. Attempt limit reached.");
        break;
    } 
    
    Console.WriteLine("Enter the door password");

    userDoorNumGuess = int.Parse(Console.ReadLine());
    if (userDoorNumGuess == doorCombination)
    {
        Console.WriteLine("Welcome Home!");
        doorUnlocked = true;
    }
    else
    {
        userUnlockAttempts++;
        Console.WriteLine($"Incorrect Input. {5 - userUnlockAttempts} attempts remaining.  ");
        
    }
}


//----------------------------------------------------------------------------------------------------

//Lab Assignment 5


int doorCombinationLabFive = 13579;
int userDoorNumGuessLabFive = -1;
bool doorUnlockedLabFive = false;
const int unlockAttemptLimitLabFive = 5;
int userUnlockAttemptsLabFive = 0;

do
{
    if (userUnlockAttemptsLabFive == unlockAttemptLimitLabFive)
    {
        Console.WriteLine("Warning. Attempt limit reached.");
        break;
    }

    Console.WriteLine("Enter the door password");

    userDoorNumGuessLabFive = int.Parse(Console.ReadLine());
    if (userDoorNumGuessLabFive == doorCombinationLabFive)
    {
        Console.WriteLine("Welcome Home!");
        doorUnlockedLabFive = true;
    }
    else
    {
        userUnlockAttemptsLabFive++;
        Console.WriteLine($"Incorrect Input. {5 - userUnlockAttemptsLabFive} attempts remaining.  ");
    }
} while (doorUnlockedLabFive == false);



//I would do the stretch, but I'm not sure how functions work in C# yet.