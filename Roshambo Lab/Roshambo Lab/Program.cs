/*
*What will the application do?
*The application prompts the player to enter a name and select an opponent.
*The application prompts the player to select rock, paper, or scissors. Then, the application displays the player’s choice, the opponent’s choice, and the result of the match.
*The application continues until the user doesn’t want to play anymore.
*If the user makes an invalid selection, the application should display an appropriate error message and prompt the user again until the user makes a valid selection.

*Build Specifications:
-*1. Create an enumeration called Roshambo that has three values: rock, paper, and scissors.
-*2. Create an abstract class named Player that stores a name and a Roshambo value. This class should include a method named GenerateRoshambo that allows an inheriting class to generate and return a Roshambo value.
-*3. Create and name three player subclasses:
   - a.RockPlayer - Always throws Rock 
    -b.RandomPlayer - Picks and throws a value at random 
    -c.HumanPlayer - Allows the user to select and throw a value. Upon creating an instance of this class, allow the user to input their name.
-*4. Create a main where you create a HumanPlayer and then allow them to choose their opponent: either RockPlayer or RandomPlayer.
-*5. Validate your user inputs throughout your app. Try catch blocks, if statements, or any other method of validation is good.

Hints:
Paper beats rock, rock beats scissors, scissors beats paper.

Extra Challenges:
Create a Validator class to handle validation of all console input. It could have methods like GetYN (gets Y/y or N/n), GetOtherPlayer (accepts the names of your two players), GetRoshambo (accepts r/p/s and/or rock/paper/scissors).
Keep track of wins and losses, and display them at the end of each session.

 */
using Roshambo_Lab;



bool gameRetry = true;
int winCount = 0;
int lossCount = 0;
HumanPlayer human = new HumanPlayer();
RandomPlayer randomPlayer = new RandomPlayer();
RockPlayer rockPlayer = new RockPlayer();
do
{
    Console.WriteLine($"Pick an opponent to play Roshambo against, {human.Name}. \nThe available opponents are:");
    foreach (int enumValue in Enum.GetValues(typeof(OpponentType)))
    {
        Console.WriteLine($"{enumValue} - {Enum.GetName(typeof(OpponentType), enumValue)}");
    }
    Console.WriteLine("Pick an opponent by number.");


    bool isValidOpponent = int.TryParse(Console.ReadLine(), out int opponentInput);
    do
    {

        if (Enum.IsDefined(typeof(OpponentType), opponentInput) && isValidOpponent == true)
        {

            break;
        }
        else
        {
            Console.Clear();
            Console.WriteLine($"Sorry you typed {opponentInput} and that's an invalid value");
            isValidOpponent = false;
            Console.WriteLine($"Pick an opponent to play Roshambo against, {human.Name}. \nThe available opponents are:");
            foreach (int enumValue in Enum.GetValues(typeof(OpponentType)))
            {
                Console.WriteLine($"{enumValue} - {Enum.GetName(typeof(OpponentType), enumValue)}");
            }
            Console.WriteLine("Pick an opponent by number.");
        }
        isValidOpponent = int.TryParse(Console.ReadLine(), out opponentInput);
        continue;
    } while (true);
    OpponentType chosenOpponent = (OpponentType)opponentInput;


    Console.Clear();
    if (chosenOpponent == OpponentType.RandomPlayer)
    {
        bool gameResult = PlayRoshamboGame(human, randomPlayer);
        if(gameResult == true)
        {
            winCount += 1;
        } else if (gameResult == false)
        {
            lossCount += 1;
        }
    }
    else if (chosenOpponent == OpponentType.RockPlayer)
    {
        bool gameResult = PlayRoshamboGame(human, rockPlayer);
        if (gameResult == true)
        {
            winCount += 1;
        }
        else if (gameResult == false)
        {
            lossCount += 1;
        }
    }
   
    Console.WriteLine("Would you like to play again?");
    string userAnswer = AnswerYOrN();
    switch (userAnswer)
    {
        case "y":
            Console.Clear();
            continue;
            
        case "n":
            Console.Clear();
            Console.WriteLine($"Finished with:\n" +
                $"-- {winCount} wins | {lossCount} losses --");
            Console.WriteLine("Goodbye!");
            gameRetry = false;
            break;
    }
    
} while (gameRetry == true);

Console.ReadKey();  
Environment.Exit(0);




//TODO Display wins and losses ~~and~~ties~~








bool PlayRoshamboGame(HumanPlayer playerOne, Player playerTwo)
{
    bool victory = false;
    Console.WriteLine(
        
        
        
        
        );
    while (true)
    {
        Console.WriteLine("Rock, Paper, Scissors!");
        Roshambo playerOneValue = playerOne.GenerateRoshambo();
        Roshambo playerTwoValue = playerTwo.GenerateRoshambo();
        if ((playerOneValue == Roshambo.Rock && playerTwoValue == Roshambo.Scissors) ||
            (playerOneValue == Roshambo.Scissors && playerTwoValue == Roshambo.Paper) ||
            (playerOneValue == Roshambo.Paper && playerTwoValue == Roshambo.Rock))
        {
            Console.WriteLine($"You rolled {Enum.GetName(typeof(Roshambo), playerOneValue)}\n" +
                $"Your opponent rolled {Enum.GetName(typeof(Roshambo), playerTwoValue)}\n" +
                $"You win!");
            victory = true;
            return victory;
        }
        else if (playerOneValue == playerTwoValue)
        {
            Console.WriteLine($"you both rolled {Enum.GetName(typeof(Roshambo), playerOneValue)}\n" +
                $"You tied!");
            continue;
        }
        else 
        {
            Console.WriteLine($"You rolled {Enum.GetName(typeof(Roshambo), playerOneValue)}\n" +
                $"Your opponent rolled {Enum.GetName(typeof(Roshambo), playerTwoValue)}\n" +
                $"You lost!");
            return victory;
        }
    }

}




string AnswerYOrN()
{
    while (true)
    {
        Console.WriteLine("Answer \"y\" or \"n\"");
        string answer = Console.ReadLine().ToLower().Trim();
        switch (answer)
        {
            case "y":
            case "n":
                return answer;
            default:
                Console.WriteLine("Invalid input: please enter \"y\" or \"n\"");
                continue;
        }
    }
}