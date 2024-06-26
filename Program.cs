﻿using System;
using System.Threading;
using System.Runtime.InteropServices;
using System.Numerics;

static class GameController
{
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

    private static char CheckDiagonalStrike(GameBoard game)
    {
        for(int i=0; i< game.Board.GetLength(0)-3; i++)
        {
            for (int j = 0; j < game.Board.GetLength(1) - 3; j++)
            {
                if (game.Board[i, j] == 'X' && game.Board[i + 1, j + 1] == 'X' && game.Board[i + 2, j + 2] == 'X' && game.Board[i + 3, j + 3] == 'X')
                    return 'X';

                else if (game.Board[i, j] == 'O' && game.Board[i + 1, j + 1] == 'O' && game.Board[i + 2, j + 2] == 'O' && game.Board[i + 3, j + 3] == 'O')
                    return 'O';
            }
        }
        for (int i = 0; i < game.Board.GetLength(0) - 3; i++)
        {
            for (int j = game.Board.GetLength(1)-1; j > game.Board.GetLength(1) - 4; j--)
            {
                if (game.Board[i, j] == 'X' && game.Board[i + 1, j - 1] == 'X' && game.Board[i + 2, j - 2] == 'X' && game.Board[i + 3, j - 3] == 'X')
                    return 'X';

                else if (game.Board[i, j] == 'O' && game.Board[i + 1, j - 1] == 'O' && game.Board[i + 2, j - 2] == 'O' && game.Board[i + 3, j - 3] == 'O')
                    return 'O';
            }
        }
        return '#';
    }
    private static char CheckVerticalStrike(GameBoard game)
    {
        for (int i = 0; i < game.Board.GetLength(1); i++)
        {
            for (int j = 0; j < game.Board.GetLength(0)-3; j++)
            {
                if (game.Board[j, i] == 'X' && game.Board[j+1, i] == 'X' && game.Board[j+2, i] == 'X' && game.Board[j+3, i] == 'X')
                    return 'X';

                else if (game.Board[j, i] == 'O' && game.Board[j + 1, i] == 'O' && game.Board[j + 2, i] == 'O' && game.Board[j + 3, i] == 'O')
                    return 'O';
            }
        }
        return '#';
    }
    private static char CheckHorizontalStrike(GameBoard game)
    {
        for(int i=0; i < game.Board.GetLength(0); i++)
        {
            for(int j=0; j < game.Board.GetLength(1)-3; j++)
            {
                if (game.Board[i,j] == 'X' && game.Board[i, j+1] == 'X' && game.Board[i, j + 2] == 'X' && game.Board[i, j + 3] == 'X')
                    return 'X';
               

                else if (game.Board[i, j] == 'O' && game.Board[i, j + 1] == 'O' && game.Board[i, j + 2] == 'O' && game.Board[i, j + 3] == 'O')
                    return 'O';
            }
        }
        return '#';
    }
    private static bool CheckTie(GameBoard game)
    {
        for (int i = 0; i < game.Board.GetLength(0); i++)
        {
            for (int j = 0; j < game.Board.GetLength(1); j++)
            {
                if (game.Board[i, j] == '#') return false;
            }
        }
        return true;
    }

    private static bool CheckWinner(GameBoard board, IPlayer player1, IPlayer player2)
    {
        _userInterface.ResetUI();
        _board.DrawBoard();
        if (CheckHorizontalStrike(board) == 'X')
        {
            Console.WriteLine($"Thw winner is: {player1.Name}");
            return true;
        }
        else if (CheckHorizontalStrike(board) == 'O')
        {
            Console.WriteLine($"Thw winner is: {player2.Name}");
            return true;
        }
        else if (CheckVerticalStrike(board) == 'X')
        {
            Console.WriteLine($"Thw winner is: {player1.Name}");
            return true;
        }
        else if (CheckVerticalStrike(board) == 'O')
        {
            Console.WriteLine($"Thw winner is: {player2.Name}");
            return true;
        }

        else if (CheckDiagonalStrike(board) == 'X')
        {
            Console.WriteLine($"Thw winner is: {player1.Name}");
            return true;
        }
        else if (CheckDiagonalStrike(board) == 'O')
        {
            Console.WriteLine($"Thw winner is: {player2.Name}");
            return true;
        }

        else if (CheckTie(board))
        {
            Console.WriteLine("It is a tie...");
            return true;
        }
            
        return false;
    }
    private static bool ValidateNewGame(string input)
    {
        if(input != "Y" && input != "N")
        {
            return true;
        }
        return false;
    }
    private static bool ResetGame(ref string reset)
    {
        Exception eObj;
        do
        {
            eObj = null;

            try
            {
                Console.WriteLine("New Game? YES (Y) -OR- NO (N)");
                reset = Console.ReadLine();
                if (ValidateNewGame(reset))
                    throw (new Exception("Invalid Command"));

            }
            catch (Exception e)
            {
                eObj = e;
            }
        } while (eObj != null);

        if (reset == "Y")
        {
            Human.Existing = false;
            return true;
        }
           
        return false;

    }


    public static void Play()
    {
        //Score, restart and exit commands pending...
        string reset = "Y";
        do
        {
             
            int gameMode = _userInterface.Intro();
            string[] names = _userInterface.GetPlayersName(gameMode);
            switch (gameMode)
            {
                case 1:
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
            _userInterface.ResetUI();
            _board.DrawBoard();

            do
            {
                _userInterface.DisplayTurn(_p1);
                _p1.MakeMove(_board.Board);
                _userInterface.ResetUI();
                _board.DrawBoard();
                if (CheckWinner(_board, _p1, _p2))
                    break;
                _userInterface.DisplayTurn(_p2);
                _p2.MakeMove(_board.Board);

            } while (!CheckWinner(_board, _p1, _p2));
                

        } while (ResetGame(ref reset));

        Console.WriteLine("Thank you for playing, press any key to exit...");

    
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

            try
            {
                GameMode = int.Parse(Console.ReadLine());
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
        Exception eObj;
        string[] names = new string[gameMode];
        do
        {
            eObj = null;
            try
            {             
                for (int i = 1; i <= gameMode; i++)
                {
                    Console.WriteLine($"Please Enter Player {i} Name:");
                    names[i - 1] = Console.ReadLine();
                    if (names[i - 1] == "") 
                        throw (new Exception("Invalid Name"));                   
                }               
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                eObj = e;
            }
        } while (eObj != null);

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

    public static bool Existing = false;

    //We still do not know if the game is gonna be played: Human vs Human || Human vs Robot
    public void MakeMove(char[,] board);

}
class Human : IPlayer
{
    //Human Player Elements defined Here:
 
    public string Name { get; set; }
    public char Token { get; set; }
    public int Score { get; set; }

    public static bool Existing = false;
    
    public Human(string name)
    {
        if (Existing)
        {
            Name = name;
            Token = 'O';
            Score = 0;
        }
        else
        {
            Name = name;
            Token = 'X';
            Score = 0;
            Existing = true;
        }
    }
    private static bool ValidateMove(int move)
    {
        if (move < 1 || move > 7) 
            return true;
        return false;

    }
    //Make Move
    public void MakeMove(char[,] board)
    {
        Exception eObj;
        do
        {
            eObj = null;
            Console.Write("Make a move: ");
            try
            {
                int move = int.Parse(Console.ReadLine()) - 1;
                if (ValidateMove(move+1)) throw (new Exception("Invalid Input"));
                for (int i = 0; i < board.GetLength(0); i++) //height
                {
                    if (board[0,move] != '#')
                        throw (new Exception("Invalid Input"));
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
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                eObj = e;
            }
        } while (eObj != null);
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