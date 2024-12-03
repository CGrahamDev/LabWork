/*
*What will the application do?
*The application prompts the player to enter a name and select an opponent.
*The application prompts the player to select rock, paper, or scissors. Then, the application displays the player’s choice, the opponent’s choice, and the result of the match.
*The application continues until the user doesn’t want to play anymore.
*If the user makes an invalid selection, the application should display an appropriate error message and prompt the user again until the user makes a valid selection.

*Build Specifications:
*1. Create an enumeration called Roshambo that has three values: rock, paper, and scissors.
*2. Create an abstract class named Player that stores a name and a Roshambo value. This class should include a method named GenerateRoshambo that allows an inheriting class to generate and return a Roshambo value.
*3. Create and name three player subclasses:
    a.RockPlayer - Always throws Rock 
    b.RandomPlayer - Picks and throws a value at random 
    c.HumanPlayer - Allows the user to select and throw a value. Upon creating an instance of this class, allow the user to input their name.
*4. Create a main where you create a HumanPlayer and then allow them to choose their opponent: either RockPlayer or RandomPlayer.
*5. Validate your user inputs throughout your app. Try catch blocks, if statements, or any other method of validation is good.

Hints:
Paper beats rock, rock beats scissors, scissors beats paper.

Extra Challenges:
Create a Validator class to handle validation of all console input. It could have methods like GetYN (gets Y/y or N/n), GetOtherPlayer (accepts the names of your two players), GetRoshambo (accepts r/p/s and/or rock/paper/scissors).
Keep track of wins and losses, and display them at the end of each session.

 */

