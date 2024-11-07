/*What will the application do? 
>1 Point: The application prompts the user to enter an integer between 1 and 100 inclusive..
Display the associated result based on the integer range entered.  

Build Specifications:
>1 Point: Use if/else statements to take different actions depending on user input.  
Given an integer entered by a user, perform the following conditional actions:
>1 Point: If the integer entered is odd and less than 60, print the number entered and “Odd and less than 60.”
>1 Point: If the integer entered is even and in the inclusive range of 2 to 24, print “Even and less than 25.”
>1 Point: If the integer entered is even and in the inclusive range of 26 to 60, print “Even and between 26 and 60 inclusive.”
>1 Point: If the integer entered is even and greater than 60, print the number entered and “Even and greater than 60.”
>1 Point: If the integer entered is odd and greater than 60, print the number entered and “Odd and greater than 60.”

Additional Requirements:
1 Point: For answering the Lab Summary when submitting to the LMS
-2 Points: if there are any syntax errors or if the program does not run (for example, in a Main method). 

Extra Challenges (2 Points Maximum):
>1 Point: Set up the program to continue running with a prompt at the end to see if they want to stop. (hint: Loops)
>1 Point: Ask for user information (ex. name) at the beginning of the application, and use it to refer to the user throughout the application.
>1 Point: Add validation to guarantee that a user enters a positive integer between 1 and 100.

*/


Console.WriteLine("What is your name?");
string userName = Console.ReadLine();

bool gameEnd = false;



//Redo this loop later to include retry logic.
while (gameEnd != true)
{
    Console.WriteLine($"Hi {userName}, enter a number between 1 and 100 inclusive");
    int userInt = int.Parse(Console.ReadLine());

    //Validation to make sure user doesn't input an incorrect input 
    if (userInt < 1 || userInt > 100) //||perhaps useless because the proram throws an exception || userInt.GetType() != typeof(int) )
    {
        gameEnd = true;
        Console.WriteLine($"Invalid Number, {userName}; thank you for using this program");
        break;
    }
    
    AnalyzeNumber( userInt );


    Console.WriteLine($"Would you like to stop the program {userName}? y/n");
    string userRerunRequest = Console.ReadLine();
    if (userRerunRequest.ToLower() == "y")
    {
        Console.WriteLine("Thank you for using the number analyzer!");
        gameEnd = true;   
    } else if (userRerunRequest.ToLower() == "n")
    {
        Console.WriteLine("Okay!");
        Console.WriteLine( " " );
    }
    else
    {
        Console.WriteLine("Invalid input, sorry!");
        gameEnd = true;
    }


}






static string AnalyzeNumber(int userInt)
{
    //the number analyzer
    if (userInt % 2 != 0 && userInt < 60)
    {
        Console.WriteLine("Odd and less than 60");
        return "Odd and less than 60";
    }
    else if (userInt % 2 == 0 && userInt >= 2 && userInt <= 24)
    {
        Console.WriteLine("Even and less than 25");
        return "Even and less than 25";
    }
    else if (userInt % 2 == 0 && userInt >= 26 & userInt <= 60)
    {
        Console.WriteLine("Even and between 26 and 60 inclusive.");
        return "Even and between 26 and 60 inclusive.";
    }
    else if (userInt % 2 == 0 && userInt > 60)
    {
        Console.WriteLine("Even and greater than 60.");
        return "Even and greater than 60.";
    }
    else if (userInt % 2 != 0 && userInt > 60)
    {
    Console.WriteLine("Odd and greater than 60.");
        return "Odd and greater than 60.";
    }
    return "Invalid input";

}   