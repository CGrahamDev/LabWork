/*
 STUDENT DATABASE
Objectives: Storing Data in Arrays, Searching Data

Task: Write a program that will recognize invalid inputs when the user requests information about students in a class.

What will the application do?
-2 Points: Create 3 arrays and fill them with student information—one with name, one with hometown, one with favorite food
-1 Point: Prompt the user to ask about a particular student by number. Convert the input to an integer. Use the integer as the index for the arrays. Print the student’s name.
-1 Point: Ask the user which category to display: Hometown or Favorite food. Then display the relevant information.
-1 Point: Ask the user if they would like to learn about another student.

Build Specifications:
-1 Point: Validate user number: Use an if statement to check if the number is out of range, i.e. either less than 1 or greater than the length of the arrays. If so, display a friendly message and let the user try again.
-1 Point: Validate category: Ask the user what information category to display: "Hometown" or "Favorite Food". Use an if statement to check that they entered either category name correctly. If they entered an incorrect category, display a friendly message and re-ask the question. (See hints below!)
-1 Point: Array Length: Use the first array’s Length property in your code instead of hardcoding it.

Hints:
Make sure the arrays are the same size.
Let the user enter a number from 1 up to and including the length of the array. To get the index, subtract 1 from the number they entered.
For the valid category: You might create a separate program to test out some code that uses a while loop and asks for either “Hometown” or “Favorite Food.” And only finishes the loop if either of these two is entered. Once you have it working, copy the code over to your “real” code.
Make it easy for the user – tell them what information is available
Try to use good grammar. Make your messages polite.

Extra Challenges:
-1 Point: Provide an option where the user can see a list of all students.
2 Points: Allow the user to search by student name (Good challenge but difficult!)
-1 Point: Category names: Allow uppercase and lowercase; allow portion of word such as "Food" instead of "Favorite Food"

 */

using System.Globalization;

string[] studentNames = new string[6];
string[] studentHometowns = new string[6];
string[] studentFavoriteFoods = new string[6];

studentNames[0] = "John";
studentNames[1] = "Cathy";
studentNames[2] = "Trinity";
studentNames[3] = "Dominic";
studentNames[4] = "Chloe";
studentNames[5] = "Ben";

studentHometowns[0] = "Detroit";
studentHometowns[1] = "Caledonia";
studentHometowns[2] = "Grand Rapids";
studentHometowns[3] = "Georgia";
studentHometowns[4] = "Ionia";
studentHometowns[5] = "Southfield";

studentFavoriteFoods[0] = "Shrimp";
studentFavoriteFoods[1] = "Steak";
studentFavoriteFoods[2] = "Rice";
studentFavoriteFoods[3] = "Potatos";
studentFavoriteFoods[4] = "Fries";
studentFavoriteFoods[5] = "Pierogi";
do
{
    
    Console.WriteLine($"There are {studentNames.Length} students. Enter a value out of range to list them.");
    //making sure its the correct data type
    int userIndexNumber = -1;
   
    // loop to validate data

    while (true) 
    {
        try
        {
            Console.WriteLine("Input a number to learn more about a student:");
            bool userInput = int.TryParse(Console.ReadLine(), out int userRawIndexNumber);
            while (userInput != true)
            {
                Console.WriteLine("Sorry! You entered an incorrect data type. Please enter a number");
                userInput = int.TryParse(Console.ReadLine(), out userRawIndexNumber);
            }
            //making sure the index is in range.
            userIndexNumber = ConvertInputToIndex(userRawIndexNumber);
            Console.WriteLine($"The student you have selected is {studentNames[userIndexNumber]}");
        }
        catch (IndexOutOfRangeException)
        {
            Console.WriteLine($"You have entered a value out of range. There are {studentNames.Length} students. They are: ");
            foreach (string student in studentNames)
            {
                Console.WriteLine(student);
            }
            continue;

        }
        break;
    }

    bool loopEnd = false;
    while (loopEnd != true)
    {
        Console.WriteLine("Which category of information would you like to learn about the student? Hometown or Favorite Food:");
        string userCategory = Console.ReadLine();
        switch (userCategory.ToLower())
        {
            case "hometown":
            case "ht":
                Console.WriteLine($"{studentNames[userIndexNumber]}'s hometown is {studentHometowns[userIndexNumber]}.");
                loopEnd = true;
                break;
            case "favorite food":
            case "fav food":
                Console.WriteLine($"{studentNames[userIndexNumber]}'s favorite food is {studentFavoriteFoods[userIndexNumber]}.");
                loopEnd = true;
                break;

            default:
                Console.WriteLine("You entered an incorrect input. Enter \"favorite food\" or \"hometown\" please. ");
                break;
        }
    }
    
        Console.WriteLine("Would you like to learn about another student? (y/n)");
        string userContinueAnswer = Console.ReadLine();
    while (userContinueAnswer.ToLower() != "y" && userContinueAnswer != "n")
    {

        Console.WriteLine("Invalid input. Answer \"y\" or \"n\".");
        userContinueAnswer = Console.ReadLine();

    }
    if (userContinueAnswer.ToLower() == "n")
    {
        Console.WriteLine("Goodbye!");
        break;
    }
    else if (userContinueAnswer.ToLower() == "y")
    {
        Console.WriteLine("okay!");
        continue;
    }
    else
    {
        
    }
} while (true);









Console.ReadKey();
Environment.Exit(0);    
int ConvertInputToIndex(int userInput)
{
    return userInput - 1;
}