/*
 
Task: Calculate the perimeter and area of a room.

What will the application do?
>1 Point The application prompts the user to enter values of length and width of the classroom.
>2 Points: The application displays the area and perimeter of that classroom.
>2 Points: The application classifies the room as small (up to and including 250 square feet), medium (more than 250 but less than 650 square feet), or large (650 square feet or more).

Build Specifications:
>1 Point: Assume that the rooms are rectangles (90 degree corners).
>1 Point: Assume that the user will enter valid numeric data for length and width.
>1 Point: The application should accept decimal entries.

Additional Requirements:
1 Point: For answering the Lab Summary when submitting to the LMS
-2 Points: if there are any syntax errors or if the program does not run (for example, in a Main method). 

Extra Challenges:
1 Point: Calculate the volume of the rooms.
1 Point: Calculate the surface area of the rooms.

*/


Console.WriteLine("Welcome to Chelsea's Room Calculator.");

double classroomLength = 0;
double classroomWidth = 0;


Console.WriteLine("Please enter the length of the classroom (in feet):");
classroomLength = double.Parse(Console.ReadLine());

Console.WriteLine("Please enter the width of the classroom (in feet):");
classroomWidth = double.Parse(Console.ReadLine());

//Area for classroom
double classroomArea = classroomLength * classroomWidth;
//Console.WriteLine(classroomArea);

//Perimeter for the classroom

double classroomPerimeter = classroomLength + classroomWidth;
Console.WriteLine($"Area: {classroomArea:f3}");
Console.WriteLine($"Perimeter: {classroomPerimeter:f3}");

//Room Sizing
if (classroomArea >= 0 && classroomArea <= 250)
{
    Console.WriteLine("The room is small.");
} else if (classroomArea > 250 && classroomArea < 650)
{
    Console.WriteLine("The room is medium.");
} else if (classroomArea > 650)
{
    Console.WriteLine("The room is large.");
} else
{
    Console.WriteLine("The room is non existent; Invalid input given.");
}


Console.WriteLine("Thank you for using this room calculator!");






//Come back to do extra challenge when it's actually assigned.