using Car_Lot_Lab;
/*
 Build Specifications
-_Create a class named Car (5 points) to store the data about a car. This class should contain:
    Data members for car details
        -~A string for the make
        -~A string for the model
        -~An int for the year
        -~A decimal for the price
    
    -A no-arguments constructor that sets data members to default values (blanks or your choice)
    
    -A constructor with four arguments matching the order above
    
    -A ToString() method returning a formatted string with the car details.
        ~This method comes from the base object class. Override it.
-_Create a subclass of Car named UsedCar (3 points). UsedCar should contain:
    -Data member for used car details:
        ~A double for mileage.
    
    -Constructor: Takes five arguments and calls the four-argument constructor for Car and saves the mileage argument
    
    -ToString: Returns a formatted string with the used car details
        -~This method comes from the base object class. Override it.
_Create an instance of List<Car> that can hold instances of Car and any class derived from Car. Make this list a public static member of Car.
    In your main, create at least three Car instances and at least three UsedCar instances and add these six instances to the list
    
    Add a public static method to Car called ListCars that loops through the list and prints out each member and its index in the list. (Hint: Use a regular for loop, not a foreach loop so you  can print out the index.)
    
    Add a public static method to Car called Remove which takes an integer parameter and removes the car whose index is that parameter
    
_In your main, print out the list (by calling the ListCar method). Then ask the user which car they would like to buy, by number (the index of the car).

_Print out the details for the chosen car. (Think about how to print out this information: You’ll access the item in the list by index, and call Console.WriteLine.)

_Remove the chosen car from the list

_List all the cars again

Hints:
Use the right access modifiers (public/private/protected)!
You can just use \t tab escape characters to line things up, or if you want to get fancier, look up text formatters. 

Extra Challenges:
Think about other methods which might be useful for your Car such as “BuyBack” where you can add a used car to the list. Implement them and modify your app to take advantage of them. 
Create an Admin mode which lets the user edit cars.
Provide search features:
View all cars of an entered make.
View all cars of an entered year.
View all cars of an entered price or less.
View only used cars or view only new cars.

 */
Car car = new Car();
int userResponse = -1;
//a variable is an marker that holds data in a program





Car.Cars = new List<Car>()
{
 new Car("Honda","Civic",2024,19999.99m),
 new Car("Nissan", "Centra", 2024, 24649.99m),
 new Car("Toyota", "Tundra", 2025, 54249.99m),
 new Car("Kia", "Sportage", 2025, 35349.99m),
 new UsedCar("Honda", "Civic", 2002, 1499.99m, 80000.24),
 new UsedCar("Nissan", "Note", 2022, 5125, 25343.47),
 new UsedCar("Kia", "Niro", 2024, 8435.32m, 15323.75),
};
while (true)
{
    car.ListCars();
    Console.WriteLine("Which car would you like to buy? (Enter the related number)");
    while (true) {
        if (int.TryParse(Console.ReadLine(), out userResponse))
        {
            try
            {
                Car.Cars[userResponse - 1].ToString();
                Console.Clear();
                Console.WriteLine("You have selected the " + Car.Cars[userResponse-1].ToString());
                Console.WriteLine("Press any key to continue");
                Console.ReadKey();
                Console.Clear();
                break;
            }
            catch(ArgumentOutOfRangeException)
            {
                Console.WriteLine($"Out of valid range. Please select a number between 1-{Car.Cars.Count}");
                Console.WriteLine("Press any key to continue.");
                continue;
            }
        }
        else
        {
            Console.WriteLine("Invalid input type. Please enter a number 1-"+Car.Cars.Count);
            Console.WriteLine("Press any key to continue.");
            continue;
        }
    } 
    Car.Remove(userResponse-1);
    Console.WriteLine("The remaining vehicles on the lot are the following:\n");
    car.ListCars();
    break;    
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
