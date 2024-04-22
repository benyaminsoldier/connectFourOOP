using System;
using System.Threading;
using System.Runtime.InteropServices;

static class GameController
{
    //
    //Applies Game Mode
    //Manage the players, Board, and UI Classes
    //This Module Contains the game logic
    //Starts the Game Play() Method. Static Method
    //Manage Turns
    //Checks conditions after every move.
    //Ends the Game when conditions met: 4 in a row; exit command; full board draw.

    private static UI _userInterface = new UI();
    private static GameBoard _board = new GameBoard();
    private static IPlayer _p1;
    private static IPlayer _p2;


    private static bool checkConditions()
    {
        return false;
    }


    public static void Play()
    {

        int gameMode = _userInterface.Intro();
        string[] names = _userInterface.GetPlayersName(gameMode);
        switch (gameMode)
        {
            case 1:
                Console.WriteLine("fucker");
                _p1 = new Human($"{names[0]}");
                _p2 = new Robot();
                break;
            case 2:
                _p1 = new Human($"{names[0]}");
                _p2 = new Human($"{names[1]}");
                break;
        }
        Console.WriteLine("Press any key to continue...");
        Console.ReadLine();
        _board.StartBoard();

        do
        {
            _userInterface.ResetUI();
            _board.DrawBoard();
            _userInterface.DisplayTurn(_p1);
            _p1.MakeMove(_board.Board);
            _userInterface.ResetUI();
            _board.DrawBoard();
            _userInterface.DisplayTurn(_p2);
            _p2.MakeMove(_board.Board);

        } while (true);


        //_board.Draw();


    }
    

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

    public int GameMode { get; private set; }
    private bool ValidateStart(string command)
    {
        command = command.ToUpper().Trim();
        if (command != "PLAY")
            return false;
        return true;
    }
    private bool ValidateGameMode()
    {
        if (GameMode != 1 && GameMode != 2)
            return false;
        return true;
    }
    private bool ValidatePlayerName()
    {
        return true;
    }
    public void ResetUI()
    {
        Console.Clear();
        Console.WriteLine("      Connect 4 Game   ");
        Console.WriteLine();
    }
    public string CheckStart(out Exception eObj)
    {
        string command;
        do
        {
            eObj = null;
            ResetUI();
            Console.WriteLine("Type 'PLAY' to start the game.");
            command = Console.ReadLine();
            try
            {      
               if (!ValidateStart(command)) throw (new Exception("Invalid Command."));               
            }
            catch (Exception e)
            {
                eObj = e;
                Console.Clear();
                
            }

        } while (eObj != null);

        return command;
    }

    public int CheckGameMode(out Exception eObj)
    {
        
        do
        {
            eObj = null;
            ResetUI();
            Console.WriteLine("Select Game Mode: Single Player (1)  -OR- MultiPlayer (2)");
            GameMode = int.Parse(Console.ReadLine());
            try
            {
                if (!ValidateGameMode()) throw (new Exception("Invalid Game Mode."));
            }
            catch (Exception e)
            {
                eObj = e;

            }

        } while (eObj != null);

        return GameMode;

    }

    public int Intro() //Intro gets the game mode and pass it to the controller
    {
        Exception eObj;
        CheckStart(out eObj);
        return CheckGameMode(out eObj);
        
    }
    
    public string[] GetPlayersName(int gameMode)
    {
        string [] names = new string [gameMode];
        for(int i=1; i <= gameMode; i++)
        {
            Console.WriteLine($"Please Enter Player {i} Name:");
            names[i-1] = Console.ReadLine();

        }
        return names;
    }
    public void DisplayTurn(IPlayer player)
    {
        Console.WriteLine($"   It is {player.Name}'s turn"); 
    }
   


}
public interface IPlayer 
{
    public string Name { get; set; }
    public char Token { get; set; }
    public int Score { get; set; }
    
    //We still do not know if the game is gonna be played: Human vs Human || Human vs Robot
    public void MakeMove(char[,] board);

}
class Human : IPlayer
{
    //Human Player Elements defined Here:
 
    public string Name { get; set; }
    public char Token { get; set; }
    public int Score { get; set; }
    
    public Human(string name)
    {
        Name = name;
        Token = 'X';
        Score = 0;
    }
    //Make Move
    public void MakeMove(char[,] board)
    {
        //Validation pending....
        Console.Write("Make a move: ");
        int move = int.Parse(Console.ReadLine())-1;
        for(int i=0; i<board.GetLength(0); i++) //height
        {
            if(i == board.GetLength(0) - 1 && board[i, move] == '#')
            {
                board[i, move] = this.Token;
                break;
            }
            else if (board[i, move] == 'X' || board[i, move] == 'O')
            {
                board[i-1, move] = this.Token;
                break;
            }
            else if (board[i, move] == '#')
                continue;
        }

    }

 
}
class Robot : IPlayer
{
    //Human Player Elements defined Here:

    public string Name { get; set; }
    public char Token { get; set; }
    public int Score { get; set; }

    public Robot()
    {
        Name = "Robot";
        Token = 'O';
        Score = 0;
    }
    //Make Move
    public void MakeMove(char[,] board)
    {

        Random r = new Random();
        int move = r.Next(7);
        for (int i = 0; i < board.GetLength(0); i++) //height
        {
            if (i == board.GetLength(0) - 1 && board[i, move] == '#')
            {
                board[i, move] = this.Token;
                break;
            }
            else if (board[i, move] == 'X' || board[i, move] == 'O')
            {
                board[i - 1, move] = this.Token;
                break;
            }
            else if (board[i, move] == '#')
                continue;
        }

    }


}


class GameBoard
{
    //board Elements defined Here:
    //Sets rows and columns
    //Displays the boards
    //Refresh the Board
    private const int _cols = 7;
    private const int _rows = 6;
    private char[,] _board;
    public char[,] Board { get => _board; set => _board = value; }


    public GameBoard()
    {
        Board = new char[_rows,_cols];
    }
    public void StartBoard()
    {
        for (int i = 0; i < Board.GetLength(0); i++)
        {
            for (int j = 0; j < Board.GetLength(1); j++)
            {
                Board[i, j] = '#';
            }         
        }
    }
    public void DrawBoard()
    {
        for(int i=0; i < Board.GetLength(0); i++)
        {
            Console.Write("   ");
            for(int j=0; j < Board.GetLength(1); j++)
            {
                Console.Write($" {Board[i,j]} ");
            }
            Console.WriteLine();
        }
        Console.Write("   ");
        for (int i = 1; i <= Board.GetLength(1); i++)
        {
            Console.Write("---");
        }
        Console.WriteLine();
        Console.Write("   ");
        for (int i = 1; i <= Board.GetLength(1); i++)
        {
            Console.Write($" {i} ");
        }
        Console.WriteLine("\n");
    }


    
}

class Program
{
    static void Main(string[] args)
    {
        //Controller Class is executed here
        //Game Starts and is only ended by the user
        GameController.Play();
        Console.ReadLine();
        
    }
}