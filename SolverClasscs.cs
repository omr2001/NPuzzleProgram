using System;
using System.Collections.Generic;
using static N_Puzzle.PriorityQueue;
namespace N_Puzzle
{
    class SolverClass
    {
        // Fields Area
        private int MinimumMoves = 0;
        int width;
        private bool Solvable = false;
        private FindNode FNode;
        private MinPriorityQueue<FindNode> nodes = new MinPriorityQueue<FindNode>(10);
        // Methods Area
        // Processing The Nodes towards The Solution
        public SolverClass(Board InitialBoard)
        {
            this.width = InitialBoard.width;
            List<Board> m;
            if (InitialBoard == null)
            {
                Console.WriteLine("The Initial Board is Null");
            }
            FNode = new FindNode(InitialBoard);

            nodes.Enqueue(FNode);
            int i = 0;
            while (!Solvable)
            {

                i++;
                FindNode min;
                min = nodes.Dequeue();
                if (min.getBoard().IdealBoard())
                {
                    Solvable = true;
                    printPath(min.getBoard());
                    MinimumMoves = min.GetMoves();
                    break;
                }
                m = new List<Board>(min.CurrentNode.NextState());
                addNodes(m);

            }
            
        }
        // Returning Number of Moves
        public int NumofMoves()
        {
            return MinimumMoves;
        }
        // Add Nodes related to Path
        public void addNodes(List<Board> children)
        {

            FindNode container;
            for (int i = 0; i < children.Count; i++)
            {
                container = new FindNode(children[i]);
                nodes.Enqueue(container);
            }

        }
        public void printPath(Board root)
        {
            if (root.myparent == null)
                return;
            printPath(root.myparent);
            printmat(root.board);
            Console.WriteLine();
            Console.WriteLine();

        }
        public void printmat(int[] board)
        {
            for (int i = 0; i < board.Length; i++)
            {
                Console.Write(board[i]+" ");
                if ((i+1) % width == 0)
                { Console.WriteLine(); }
            }

        }

        // Solution Displayer Function

    }
}
