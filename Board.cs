using System;
using System.Collections.Generic;
namespace N_Puzzle
{
    class Board
    {
        //Fields Area
        public int[] board;
        private int offset = 1;
        public int width;
        private List<Board> mychildrens = new List<Board>();
        private int mychildren_count = 0;
        private int empty_cell_row = 0;
        private int empty_cell_col = 0;
        private int index_empty = 0;
        private int index_empty_of_my_parent = -1;
        private int G = 0;
        private int H = 1000;
        public int MDistance = 100000;
        public Board myparent = null;
        //Methods Area
        //Checking The Place of The Blank Area
        public Board(int[,] blocks)
        {
            if (blocks == null)
            {
                Console.WriteLine("Not Solvable");
            }
            if (blocks.GetLength(1) != blocks.GetLength(0))
            {
                Console.WriteLine("Not Solvable");
            }
            width = blocks.GetLength(1);
            board = new int[width * width];
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < blocks.GetLength(0); j++)
                {
                    if (blocks[i, j] == 0)
                    {
                        empty_cell_row = i;
                        empty_cell_col = j;
                        index_empty = Array2DtoArray1D(i, j);
                    }
                    board[Array2DtoArray1D(i, j)] = blocks[i, j];
                }
            }
        }//this will be called once only 
        public Board()
        {

        }
        // Converting 2D Array to 1D Array by Returning Indices
        private int Array2DtoArray1D(int row, int col)
        {
            return (col % width) + (width * row);
        }
        //Converting 1D Array to 2D Array
        private int[] Array2DFrom1D(int i)
        {
            int[] Arr2D = new int[2];
            Arr2D[1] = i % width;
            Arr2D[0] = i / width;
            return Arr2D;
        }
        // Swapping 2 Values in an Array
        private void Exchangein1DArray(int[] arr, int i, int j)
        {
            int temp = 0;
            temp = arr[i];
            arr[i] = arr[j];
            arr[j] = temp;

        }
        // Checking Boundries of The Grid
        private bool CheckBoundary(int row, int col)
        {
            if (row < 0 || row >= width || col < 0 || col >= width)
                return false;
            return true;
        }
        // Returning Dimensions
        public int Dimension()
        {
            return width;
        }
        // Calculating Hamming Distance
        public int Hamming_Distance()
        {
            int Sum = 0;
            for (int i = 0; i < board.Length; i++)
            {
                if (board[i] != 0 && board[i] != i + offset)
                {
                    Sum += 1;
                }
            }
            return Sum;
        }
        //Calculating Manhatten Distance
        public void Manhattan_Distance()
        {
            int cost = 0;
            int[] puzzleOneD = board;
            for (int i = 0; i < puzzleOneD.Length; i++)
            {
                if (puzzleOneD[i] == 0)
                    continue;
                int x1 = (puzzleOneD[i] - 1) % width, x2 = i % width;
                int y1 = (puzzleOneD[i] - 1) / width, y2 = i / width;
                cost += Math.Abs(x1 - x2) + Math.Abs(y1 - y2);
            }
            this.MDistance = cost;
        }
        // Goal Board
        public bool IdealBoard()
        {
            for (int i = 0; i < board.Length - offset; i++)
                if (board[i] != i + 1)
                    return false;
            return true;
        }
        // Checking Equality of Two Objects
        public bool Equality(Object y)
        {
            if (y == this)
            {
                return true;
            }
            if (y == null)
            {
                return false;
            }
            if (y.GetType() != this.GetType())
            {
                return false;
            }
            Board B2 = (Board)y;
            if (B2.Dimension() != this.Dimension())
            {
                return false;
            }
            for (int i = 0; i < board.Length; i++)
            {
                if (this.board[i] != B2.board[i])
                    return false;
            }
            return true;
        }
        // Transferring Contents of 1D Array to 2D Array
        private int[,] Copy1DArrayTo2D(int[] arr, int width)
        {
            int[,] arr2D = new int[width, width];
            for (int i = 0; i < arr.Length; i++)
            {
                int x = Array2DFrom1D(i)[0];
                int y = Array2DFrom1D(i)[1];
                arr2D[y, x] = arr[i];
            }
            return arr2D;
        }
        // Transferring Contents of 1D Array to 1D Array
        private int[] Copy1DArrayTo1D(int[] a)
        {
            int[] b = new int[a.Length];
            if (a.Length != b.Length)
                return null;
            for (int i = 0; i < a.Length; i++)
            {
                b[i] = a[i];
            }
            return b;
        }
        // Checking the Possible Moves
        public List<Board> NextState()
        {

            int Up = 0, Down = 0, Left = 0, Right = 0;
            int[,] NextState2D = new int[width, width];
            int[] NextState1D = new int[board.Length];

            for (int i = 0; i < board.Length; i++)
            {
                NextState1D[i] = board[i];
                if (NextState1D[i] == 0) ///////added
                {
                    empty_cell_row = Array2DFrom1D(i)[0];
                    empty_cell_col = Array2DFrom1D(i)[1];
                    index_empty = i;
                }
            }
            int new_index_empty;
            if (CheckBoundary(empty_cell_row - 1, empty_cell_col))
            {
                Up = Array2DtoArray1D(empty_cell_row - 1, empty_cell_col);
                if (this.index_empty_of_my_parent != Up)
                {
                    Exchangein1DArray(NextState1D, index_empty, Up);
                    new_index_empty = Array2DtoArray1D(empty_cell_row - 1, empty_cell_col);
                    Copy1Dinchildren(NextState1D, new_index_empty);
                    Exchangein1DArray(NextState1D, index_empty, Up);
                }
            }
            if (CheckBoundary(empty_cell_row, empty_cell_col + 1))
            {
                Right = Array2DtoArray1D(empty_cell_row, empty_cell_col + 1);
                if (this.index_empty_of_my_parent != Right)
                {
                    Exchangein1DArray(NextState1D, this.index_empty, Right);
                    new_index_empty = Array2DtoArray1D(empty_cell_row, empty_cell_col + 1);
                    Copy1Dinchildren(NextState1D, new_index_empty);
                    Exchangein1DArray(NextState1D, this.index_empty, Right);
                }

            }
            if (CheckBoundary(empty_cell_row + 1, empty_cell_col))
            {

                Down = Array2DtoArray1D(empty_cell_row + 1, empty_cell_col);
                if (this.index_empty_of_my_parent != Down)
                {
                    Exchangein1DArray(NextState1D, this.index_empty, Down);
                    new_index_empty = Array2DtoArray1D(empty_cell_row + 1, empty_cell_col);
                    Copy1Dinchildren(NextState1D, new_index_empty);
                    Exchangein1DArray(NextState1D, this.index_empty, Down);
                }

            }
            if (CheckBoundary(empty_cell_row, empty_cell_col - 1))
            {
                Left = Array2DtoArray1D(empty_cell_row, empty_cell_col - 1);
                if (this.index_empty_of_my_parent != Left)
                {
                    Exchangein1DArray(NextState1D, this.index_empty, Left);
                    new_index_empty = Array2DtoArray1D(empty_cell_row, empty_cell_col - 1);
                    Copy1Dinchildren(NextState1D, new_index_empty);
                    Exchangein1DArray(NextState1D, this.index_empty, Left);
                }
            }
            return mychildrens;
        }
        // Assigning Children Values
        private void Copy1Dinchildren(int[] arr, int emptyindex)
        {
            mychildrens.Add(new Board());
            mychildrens[mychildren_count].board = new int[width * width];
            mychildrens[mychildren_count].G = G + 1;
            mychildrens[mychildren_count].width = width;
            mychildrens[mychildren_count].index_empty = emptyindex;
            mychildrens[mychildren_count].index_empty_of_my_parent = this.index_empty;

            for (int i = 0; i < arr.Length; i++)
            {
                mychildrens[mychildren_count].board[i] = arr[i];
            }
            mychildrens[mychildren_count].Manhattan_Distance();
            mychildrens[mychildren_count].H = mychildrens[mychildren_count].Hamming_Distance();
            mychildrens[mychildren_count].myparent = this;
            mychildren_count = mychildren_count + 1;
        }
        // Returning Number of Moves
        public int Get_NUmber_ofmoves()
        {
            return this.G;
        }
        // Checking Solvability of Puzzle
        public bool IsSolvable()
        {
            int[] b = board;
            int parity = 0;
            int gridwidth = (int)Math.Sqrt(b.Length);
            int row = 0;
            int blankrow = 0;
            for (int i = 0; i < b.Length; i++)
            {
                if (i % gridwidth == 0)
                {
                    row++;
                }
                if (b[i] == 0)
                {
                    blankrow = row;
                    continue;
                }
                for (int j = i + 1; j < b.Length; j++)
                {
                    if (b[i] > b[j] && b[j] != 0)
                    {
                        parity++;
                    }
                }
            }
            if (gridwidth % 2 == 0)
            {
                if (blankrow % 2 == 0)
                {
                    return parity % 2 == 0;
                }
                else
                {
                    return parity % 2 != 0;
                }
            }
            else
            {
                return parity % 2 == 0;
            }
        
        }
        


    }
}


