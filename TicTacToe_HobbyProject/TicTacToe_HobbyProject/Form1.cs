using System;
using System.Drawing;
using System.Windows.Forms;

namespace TicTacToe
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private bool playerTurn = true;                  // Switches player turns to color code player one and two. True means Red player goes first.
        private char[,] gameArray = new char[3, 3];    // 2D array to map out buttons into a grid formation.
        private int ticker, win, i, j = 0;             // "Ticker" confirms search results, "win" determins which color won at the end, "i" + "j" are used in for loops.
        private int max = 3;                          // Sets max iterations in for loops.
        private int maxRound = 0;                    // Controls max amount of rounds to initiate Draw game ending condition.

        // BUTTON SECTION
        private void button1_Click(object sender, EventArgs e) // Button 1 contains all the explanations which are repeated for any other button.
                                                               // With the exception of the hard coded 2D array index.
        {
            if (playerTurn == true)
            {

                button1.BackColor = Color.Red;  // Changes color of Button to Red.
                gameArray[0, 0] = 'r';         // Allocates this Button as Red.
                playerTurn = false;           // Switches over to next player for their turn.
                formColor();                 // Changes Form color based on who's turn it is now.
            }
            else
            {

                button1.BackColor = Color.Yellow; // Changes color of Button to Yellow.
                gameArray[0, 0] = 'y';           // Allocates this Button as Yellow.
                playerTurn = true;              // Switches over to next player for their turn.
                formColor();                   // Changes Form color based on who's turn it is now.
            }

            verSearch();                        // Conducts a Vertical Search that repeats 3 times.
            horSearch();                       // Conducts a Horizontal Search that repeats 3 times.
            diaSearch();                      // Conducts a Diaginal Search that does not repeat.

        }
        private void formColor()                // Controls the color of the Form background.
        {
            if (playerTurn == true)
            {
                Form1.ActiveForm.BackColor = Color.DarkSalmon; // If it is Red's turn the form will turn Red(ish).
            }
            else
            {
                Form1.ActiveForm.BackColor = Color.LemonChiffon; // If it is Yellow's turn the form will turn Yellow(ish).
            }
        }
        // SEARCH SECTION
        private void verSearch()
        {
            // Vertical Search
            for (i = 0; i < max; i++)                       // The [i] loop is slower than the [j] loop so it is used to iterate through the right array index. [j,i].                         
            {                                              // This allows each vertical line of the grid to be searched and counted.
                for (j = 0; j < max; j++)
                {
                    if (gameArray[j, i] == 'r')
                    {
                        ticker++;                           // If the index contains an 'r' the ticker goes up.
                    }
                    else if (gameArray[j, i] == 'y')
                    {
                        ticker--;                           // If the index contains a 'y' the ticker goes down.
                    }

                }
                tickerCheck();                              // After the ticks have been counted on a single line the tickerCheck method will trigger.
                                                            // This is inside the larger for loop in order to check each line on the grid and reset the ticker after each search.
            }

        }
        public void horSearch()
        {
            //Horizontal Check
            for (i = 0; i < max; i++)                       // The [i] loop is slower than the [j] loop so it is used to iterate through the left array index. [i,j]
            {                                              // This allows each horizontal line of the grid to be searched and counted.
                for (j = 0; j < max; j++)
                {
                    if (gameArray[i, j] == 'r')
                    {
                        ticker++;                         // If the index contains an 'r' the ticker goes up.
                    }
                    else if (gameArray[i, j] == 'y')
                    {
                        ticker--;                        // If the index contains a 'y' the ticker goes down.
                    }

                }
                tickerCheck();                           // After the ticks have been counted on a single line the tickerCheck method will trigger.
                                                         // This is inside the larger for loop in order to check each line on the grid and reset the ticker after each search.
            }
        }
        public void diaSearch() // The Diagonal search is made from two for loops that are seperated. This allows for the different directions of the lines.
        {
            // Diagonal Check top left to bottom right.
            for (i = 0; i < max; i++)                   // Only single for loop required since each index are matching.
            {
                if (gameArray[i, i] == 'r')
                {
                    ticker++;                           // If the index contains an 'r' the ticker goes up.
                }
                else if (gameArray[i, i] == 'y')
                {
                    ticker--;                           // If the index contains a 'y' the ticker goes down.
                }

            }

            tickerCheck(); // TickerCheck resets the values after each line is searched.

            // Diagonal Check from bottom left to top right.

            j = 2;                                      // [I] index increases as [J] index Decreases around a single for loop.
            for (i = 0; i < max; i++)
            {
                if (gameArray[j, i] == 'r')
                {
                    ticker++;                           // If the index contains an 'r' the ticker goes up.
                }
                else if (gameArray[j, i] == 'y')
                {
                    ticker--;                           // If the index contains a 'y' the ticker goes down.
                }
                j--;                                    // Decreasing [J] and Increasing [I] in this manner allows the indexes to be [2,0] > [1,1] > [0,2].
            }
            tickerCheck();

            // The end of the final Search means that the turn is complete. A check is done to make sure there are still empty squares to continue the game.
            maxRound++;

            if (maxRound == 9) // Max Rounds hard coded to 9 due to number of possible moves before all Squares are "occupied".
            {
                MessageBox.Show("GAME IS A DRAW!!!!"); // Seperate if statement to check if all the boxes are full. This allows a draw to be declared.
                gameReset();

            }

        }
        private void tickerCheck()  // After each line is searched the number of ticks is counted. 
        {                          // If the line contains 3 reds the number of ticks will be 3.
            if (ticker == 3)      // If the line contains 3 yellows the number of ticks will be -3.
            {                    // Any other number will be disregarded and the ticker will be reset.
                win = 1;
                gameWon();
                ticker = 0;
            }
            else if (ticker == -3)
            {
                win = -1;
                gameWon();
                ticker = 0;
            }
            else
            {
                ticker = 0;         // Ticker reset after each search task is complete.  
            }


        }
        private void gameWon()      // Once the correct number of tickers have been counted a winner is declared.
        {
            if (win == -1)
            {
                MessageBox.Show("YELLOW HAS WON!!!!!");
            }
            else if (win == 1)
            {
                MessageBox.Show("RED HAS WON!!!!!");
            }

            gameReset();
        }
        private void gameReset()
        {
            button1.BackColor = Color.White;        // All colors of Buttons are reset back to White as default.
            button2.BackColor = Color.White;
            button3.BackColor = Color.White;
            button4.BackColor = Color.White;
            button5.BackColor = Color.White;
            button6.BackColor = Color.White;
            button7.BackColor = Color.White;
            button8.BackColor = Color.White;
            button9.BackColor = Color.White;

            for (int i = 0; i < max; i++)
            {
                for (int j = 0; j < max; j++)
                {
                    gameArray[i, j] = ' ';          // All the buttons will have the 'r' and 'y' removed for the next game.
                }
            }

            playerTurn = true;                      // Red Player will always go first on the new game.
            maxRound = 0;
            Form1.ActiveForm.BackColor = Color.Red; // Color of Form will change to red to represent this.
        }
        private void button10_Click(object sender, EventArgs e) // Reset button allows user to reset game at any stage.
        {
            gameReset();
        }


        // Button explanation is above. No comments below this point.
        private void button2_Click(object sender, EventArgs e)
        {
            if (playerTurn == true)
            {

                button2.BackColor = Color.Red;
                gameArray[0, 1] = 'r';
                playerTurn = false;
                formColor();
            }
            else
            {

                button2.BackColor = Color.Yellow;
                gameArray[0, 1] = 'y';
                playerTurn = true;
                formColor();
            }
            verSearch();
            horSearch();
            diaSearch();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (playerTurn == true)
            {

                button3.BackColor = Color.Red;
                gameArray[0, 2] = 'r';
                playerTurn = false;
                formColor();
            }
            else
            {

                button3.BackColor = Color.Yellow;
                gameArray[0, 2] = 'y';
                playerTurn = true;
                formColor();
            }
            verSearch();
            horSearch();
            diaSearch();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (playerTurn == true)
            {

                button4.BackColor = Color.Red;
                gameArray[1, 0] = 'r';
                playerTurn = false;
                formColor();
            }
            else
            {

                button4.BackColor = Color.Yellow;
                gameArray[1, 0] = 'y';
                playerTurn = true;
                formColor();
            }
            verSearch();
            horSearch();
            diaSearch();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (playerTurn == true)
            {

                button5.BackColor = Color.Red;
                gameArray[1, 1] = 'r';
                playerTurn = false;
                formColor();
            }
            else
            {

                button5.BackColor = Color.Yellow;
                gameArray[1, 1] = 'y';
                playerTurn = true;
                formColor();
            }
            verSearch();
            horSearch();
            diaSearch();

        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (playerTurn == true)
            {

                button6.BackColor = Color.Red;
                gameArray[1, 2] = 'r';
                playerTurn = false;
                formColor();
            }
            else
            {

                button6.BackColor = Color.Yellow;
                gameArray[1, 2] = 'y';
                playerTurn = true;
                formColor();
            }
            verSearch();
            horSearch();
            diaSearch();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (playerTurn == true)
            {

                button7.BackColor = Color.Red;
                gameArray[2, 0] = 'r';
                playerTurn = false;
                formColor();
            }
            else
            {

                button7.BackColor = Color.Yellow;
                gameArray[2, 0] = 'y';
                playerTurn = true;
                formColor();
            }
            verSearch();
            horSearch();
            diaSearch();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (playerTurn == true)
            {

                button8.BackColor = Color.Red;
                gameArray[2, 1] = 'r';
                playerTurn = false;
                formColor();
            }
            else
            {

                button8.BackColor = Color.Yellow;
                gameArray[2, 1] = 'y';
                playerTurn = true;
                formColor();
            }
            verSearch();
            horSearch();
            diaSearch();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (playerTurn == true)
            {

                button9.BackColor = Color.Red;
                gameArray[2, 2] = 'r';
                playerTurn = false;
                formColor();
            }
            else
            {

                button9.BackColor = Color.Yellow;
                gameArray[2, 2] = 'y';
                playerTurn = true;
                formColor();
            }
            verSearch();
            horSearch();
            diaSearch();
        }

    }
}
