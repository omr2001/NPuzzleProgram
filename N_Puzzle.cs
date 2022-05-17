using System;
namespace N_Puzzle
{
    class N_Puzzle
    {
        public static int NumberOfMOvements(int size, int[,] puzzle)
        {
            //Assigning The Puzzle to Board Class
            Board initial = new Board(puzzle);
            //Checking Solvability of The Puzzle
            if (!initial.IsSolvable())
            {
                return -1;
            }
            else
            {
             // Calculating Execution Time using Stop Watch 
                var watch = new System.Diagnostics.Stopwatch();
                watch.Start();
                //Solving Puzzle
                SolverClass solver = new SolverClass(initial);
                watch.Stop();
                //Returning Number of Moves and Displaying Solution
                Console.WriteLine("\n");
                Console.WriteLine(solver.NumofMoves());
                Console.WriteLine($"Execution Time: {watch.ElapsedMilliseconds} ms");
                return solver.NumofMoves();

            }


        }
    }
}
