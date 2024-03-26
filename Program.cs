using System;

abstract class Player
{
    //This is an Abstract Class
    //We still do not know if the game is gonna be played: Human vs Human || Human vs Robot
}
class Human : Player
{
    //Human Player Elements defined Here:
    //Player Name
    //Player Token
    //Make Move
 
}
class Robot : Player
{
    //Robot Player Elements defined Here:
    //Computer Token
    //Computer Move
}
class Board
{
    //board Elements defined Here:
    //Sets rows and columns
    //Displays the boards
    //Refresh the Board

}
class GameController
{
    //
    //Applies Game Mode
    //Manage the players, Board, and UI Classes
    //This Module Contains the game logic
    //Starts the Game Play() Method.
    //Manage Turns
    //Checks conditions after every move.
    //Ends the Game when conditions met: 4 in a row; exit command; full board draw.

}
class UI
{
    //Gives sense to the game:
    //Provides Game Intro
    //Provide Game Instructions.
    //Collects Game Mode
    //Collects Player(s) Name
    //Provides Game Commands
    //Displays User Turns
    //Collect User Inputs
    //Displays Score

}
class Program
{
    static void Main(string[] args)
    {
            //Controller Class is executed here
            //Game Starts and is only ended by the user
    }
}