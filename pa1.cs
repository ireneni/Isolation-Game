// I had to use different unicode display strings since the half blocks did not load properly on my laptop.
// I also have platforms as " P " because it was more differentiable from the one full block i have for open tile.

//oct 11th addendum: i read campuswire and i realized my default platforms from off the box would not work for a 4x4 board, so i modified my code to reflect. thanks and happy thanksgiving!!
using System;
using static System.Console;

namespace Bme121
{
    static class Program
    {
        
     // Collect user input but allow just <Enter> for a default value.
        static string[ ] letters = { "a","b","c","d","e","f","g","h","i","j","k","l",
        "m","n","o","p","q","r","s","t","u","v","w","x","y","z"};
        
        //declare static variables to be used across all methods
        static string playerAName;
        static string playerBName;
        static int numRows;
        static int numCols;
        static string playerAPos;
        static string playerBPos;
        static int playerARow;
        static int playerACol;
        static int playerBRow;
        static int playerBCol;
        static int[,] board = new int[26, 26]; //i declared the 2d array for tile state as 26x26 to satisfy maximum board size, but only necessary values are accessed depending on user input
        
        static void Main( )
        {
        //asking for user input to initialize game
            
            bool gameWin = false; 
            int playerTurn = 0;
            
            initGame();
            DrawGameBoard(); // draws the initial game board

            while (! gameWin)
            {   
                PlayerMove(playerTurn);
                // code for displaying the states of the tiles at corresponding coord; might be helpful for marking so i kept it in :)
                //for (int i = 0; i<numRows; i++)
                //{
                    //for (int j=0; j<numCols; j++)
                    //{
                        //Write("{0}, {1}: {2} ", letters[i], letters[j], board[i,j]);
                    //}
                //}
                WriteLine();
                DrawGameBoard(); // draws the game board
                playerTurn += 1;
            }
            
        }
        
        static void initGame() //initialization method that takes user input for game specifications
        {
            WriteLine("Press enter for default.");
            Write( "Player A enter your name [default Player A]: " );
            playerAName = ReadLine( );
            if( playerAName.Length == 0 ) playerAName = "Player A";
            WriteLine( "name: {0}", playerAName );
            //Collect user input from second player
            Write( "Player B enter your name [default Player B]: " );
            playerBName = ReadLine( );
            if( playerBName.Length == 0 ) playerBName = "Player B";
            WriteLine( "name: {0}", playerBName );
            //prompt for number of rows
            Write( "Enter your desired number of rows [default 6, minimum 4 and max 26]: " );
            string strNumRows = ReadLine( );
            if( strNumRows.Length == 0 )
            { 
                numRows = 6;
            }
            else
            {
                numRows = Convert.ToInt32(strNumRows);
                if (numRows <= 4)
                {
                    WriteLine("Your board needs to be a minimum of 4 x 4. We've made your board 4 rows wide.");
                    numRows = 4;
                }
                else if (numRows > 26)
                {
                WriteLine("Your board needs to be a maximum of 26x26. We've made your board 26 rows wide.");
                    numRows = 26;
                }
            }
            Write( "Enter your desired number of columns [default 8, minimum 4 and max 26]: " );
            string strNumCols = ReadLine( );
            if( strNumCols.Length == 0 )
            { 
                numCols = 8;
            }
            else
            {
                numCols = Convert.ToInt32(strNumCols);
                if (numCols <= 4)
                {
                    WriteLine("Your board needs to be a minimum of 4 x 4. We've made your board 4 columns wide.");
                    numCols = 4;
                }
                else if (numCols > 26)
                {
                WriteLine("Your board needs to be a maximum of 26x26. We've made your board 26 columns wide.");
                    numCols = 26;
                }
            }
            WriteLine( "number of rows is {0} and number of columns is {1}", numRows, numCols );
            //prompt for player A position
            Write( "Player A input your starting position [default ca]: " );
            playerAPos = ReadLine( );
            if( playerAPos.Length == 0 ) playerAPos = letters[(numRows/2 - 1)] + letters[0]; //use integer division to find starting platforms
            //prompt for player B position
            Write( "Player B input your starting position [default dh]: " );
            playerBPos = ReadLine( );
            if( playerBPos.Length == 0 ) playerBPos = letters[(numRows/2)] + letters[numCols-1]; //use integer division to find starting platforms
            
            playerARow = Array.IndexOf(letters, playerAPos.Substring(0,1));
            playerACol = Array.IndexOf(letters,playerAPos.Substring(1,1));
            playerBRow = Array.IndexOf(letters, playerBPos.Substring(0,1));
            playerBCol = Array.IndexOf(letters,playerBPos.Substring(1,1));
            
            board = new int[numRows, numCols];
            board[playerARow, playerACol] = 2; //init player a position
            board[playerBRow, playerBCol] = 3; //init player b position
        }
    
        static void DrawGameBoard()
        {
            const string h  = "\u2500"; // horizontal line
            const string v  = "\u2502"; // vertical line
            const string tl = "\u250c"; // top left corner
            const string tr = "\u2510"; // top right corner
            const string bl = "\u2514"; // bottom left corner
            const string br = "\u2518"; // bottom right corner
            const string vr = "\u251c"; // vertical join from right
            const string vl = "\u2524"; // vertical join from left
            const string hb = "\u252c"; // horizontal join from below
            const string ha = "\u2534"; // horizontal join from above
            const string hv = "\u253c"; // horizontal vertical cross
            const string sp = " ";      // space
            const string pa = "A";      // pawn A
            const string pb = "B";      // pawn B
            const string fb = "\u2588"; // full block
            //pawn and special tile Display strings
            const string playerAStr = sp+pa+sp;
            const string playerBStr = sp+pb+sp;
            //const string platformStr = sp+bb; 
            const string platformStr = sp+"P"+sp;
            const string openTile = sp+fb+sp; //unicode block did not display properly on my laptop so ive modified my strings to work on my powershell
            const string removedTile = sp+sp+sp;
            
            // determine if platforms are necessary
            if (board[Array.IndexOf(letters, playerAPos.Substring(0,1)), Array.IndexOf(letters,playerAPos.Substring(1,1))] != 2)
            {
                board[Array.IndexOf(letters,playerAPos.Substring(0,1)),  Array.IndexOf(letters, playerAPos.Substring(1,1))] = 4;

            }

            if (board[Array.IndexOf(letters, playerBPos.Substring(0,1)), Array.IndexOf(letters,playerBPos.Substring(1,1))] != 3)
            {
                board[Array.IndexOf(letters, playerBPos.Substring(0,1)), Array.IndexOf(letters,playerBPos.Substring(1,1))] = 4;
            }
            
            // Draw the top board boundary
            //Write top letter headings
            Write( "   " );
            for( int r = 0; r < numCols; r ++ )
            {
                Write( "  {0} ", letters[ r ] );
            }
            WriteLine("");
            Write("   " );
            //draw top boundaries
            for( int c = 0; c < numCols; c ++ )
            {
                if( c == 0 ) Write( tl );
                Write( "{0}{0}{0}", h );
                if( c == numCols - 1 ) Write( "{0}", tr ); 
                else                                Write( "{0}", hb );
            }
            WriteLine("   " );
            
            // Draw the board rows.
            
            //Draw vertical letter at start of each row
            for( int r = 0; r < numRows; r ++ )
            {
                Write( " {0} ", letters[ r ] );
                
                // Draw the row contents.
                for( int c = 0; c < numCols; c ++ )
                {
                    if( c == 0 ) Write( v );
                    if( board[ r, c ] == 0) Write( "{0}{1}", openTile, v );  //if this is a playable tile
                    else if( board[ r, c ] == 1) Write( "{0}{1}", removedTile, v ); //draws removed tiles
                    else if( board[ r, c ] == 2) Write( "{0}{1}", playerAStr, v ); //draws player B pawn
                    else if( board[ r, c ] == 3) Write( "{0}{1}", playerBStr, v ); //draws player B pawn
                    else if( board[ r, c ] == 4) Write( "{0}{1}", platformStr, v ); // draws platform tiles
                    else                Write( "{0}{1}", "   ", v );
                }
                WriteLine( );
                
                                
          
                // Draw the boundary after the row.
                if( r != numRows - 1 ) 
                { 
                    Write( "   " );
                    for( int c = 0; c < numCols; c ++ )
                    {
                        if( c == 0 ) Write( vr );
                        Write( "{0}{0}{0}", h );
                        if( c == numCols - 1 ) Write( "{0}", vl ); 
                        else                                Write( "{0}", hv );
                    }
                    WriteLine( );
                }
                else //draws last row
                {
                    Write( "   " );
                    for( int c = 0; c < numCols; c ++ )
                    {
                        if( c == 0 ) Write( bl );
                        Write( "{0}{0}{0}", h );
                        if( c == numCols - 1 ) Write( "{0}", br ); 
                        else                                Write( "{0}", ha );
                    }
                    WriteLine( );
                }
            }
        }
        static void PlayerMove(int playerTurn) //should take argument for which p
        {
            bool moveComplete = false;
            bool isValid = false;
            int pawnRow, pawnCol, playerDisplayVal; //playerDisplayVal is the integer value 3 or 4 assigned to player A and B respectively in the board array
            string playerName;
            if (playerTurn%2 ==0)
            {
                pawnRow = playerARow;
                pawnCol = playerACol;
                playerDisplayVal = 2;
                playerName = playerAName;
            }
            else
            {
                pawnRow = playerBRow;
                pawnCol = playerBCol;
                playerDisplayVal = 3;
                playerName = playerBName;
            }
            WriteLine("It is player {0}'s turn: ", playerName);
            int moveRow, moveCol, removeRow, removeCol;
            
            while (! moveComplete)
            {
                while (! isValid)
                {
                    WriteLine("Enter your move in the form abcd: ");
                    isValid = true;
                }
                
                string move =  ReadLine();
                moveRow = Array.IndexOf(letters, move.Substring(0,1));
                moveCol = Array.IndexOf(letters, move.Substring(1,1));
                removeRow = Array.IndexOf(letters, move.Substring(2,1));
                removeCol = Array.IndexOf(letters, move.Substring(3,1));
                
                if ((moveRow > numRows-1) 
                 || (removeRow > numRows-1) 
                 || (moveCol > numCols-1) 
                 || (removeCol > numCols-1)) 
                {
                    WriteLine ("Your coordinates are outside the coordinates of the game board.");
                    isValid = false;
                }
                else
                {
                    
                    if ((board[moveRow, moveCol] != 0 && board[moveRow, moveCol] != 4)  //check if move was on an open tile
                     || (board[removeRow, removeCol] != 0))
                    {
                        WriteLine ("Your attempted move or tile removal was not on a playable tile.");
                        isValid = false;
                    }
                    else
                    {
                        if (((moveRow == pawnRow+1) || (moveRow == pawnRow-1) || (moveRow == pawnRow))  //check if move is to adjacent tile
                         && ((moveCol == pawnCol+1) || (moveCol == pawnCol-1) || (moveCol == pawnCol)))
                        {
                            isValid = true;
                        }
                        else
                        {
                            WriteLine ("You can only move to an adjacent tile.");
                            isValid = false;
                        }
                    }
                }
                
                if (isValid)
                {
                    WriteLine("Your move was valid. Good Job.");
                    board[moveRow, moveCol] = playerDisplayVal;
                    board[pawnRow, pawnCol] = 0;
                    board[removeRow, removeCol] = 1;
                    if (playerDisplayVal == 2)
                    {
                        playerARow = moveRow;
                        playerACol = moveCol;
                    }
                    else
                    {
                        playerBRow = moveRow;
                        playerBCol = moveCol;
                    }
                    moveComplete = true;
                }
            }
        
        // gamewin checker
        }
    }   
}
